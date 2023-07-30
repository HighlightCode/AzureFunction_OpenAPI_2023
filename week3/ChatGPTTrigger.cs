using System.Net;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;

namespace week3
{
    public class ChatGPTTrigger
    {
        private readonly ILogger _logger;

        public ChatGPTTrigger(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<ChatGPTTrigger>();
        }

        [Function("ChatGPTTrigger")]
        [OpenApiOperation(operationId: nameof(ChatGPTTrigger.Run), tags: new[] { "name" })]
        [OpenApiRequestBody(contentType: "text/plain", bodyType: typeof(string), Required = true, Description = "The request body")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "Response 200: OK response")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "text/plain", bodyType: typeof(string), Description = "Response 400: Error, Bad Request")]

        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function,  "post", Route = "completions")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var prompt = req.ReadAsString();

            // HTTP Client 생성 
            var httpClient = new HttpClient();

            // Client Header Setting
            var apiKey = Environment.GetEnvironmentVariable("AOAI_APIKEY");
            Console.WriteLine(apiKey);
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

            //var organizationKey = Environment.GetEnvironmentVariable(" AOAI_OrganizationKey");
            //httpClient.DefaultRequestHeaders.Add("OpenAI-Organization", organizationKey);

            // Body 생성
            var requestBody = JsonSerializer.Serialize(new
            {
                messages = new[]
                {
                    new { role = "system", content = "You are a helpful assistant. You are very good at summarizing the given text into 2-3 bullet points." },
                    new { role = "user", content = prompt }
                },
                model = "gpt-3.5-turbo",
                max_tokens = 800,
                temperature = 0.7f,
            });
            var endpoint = Environment.GetEnvironmentVariable("AOAI_ENDPOINT");
            Console.WriteLine(endpoint);
            var content = new StringContent(requestBody, Encoding.UTF8, "application/json");

            // Http 전송
            var response = await httpClient.PostAsync(endpoint, content);

            // 응답 바디에서 content 필드만 추출
            var responseBody = await response.Content.ReadAsStringAsync();
            dynamic? responseJson = JsonSerializer.Deserialize<dynamic>(responseBody);
            string message = responseJson.choices[0].message.content;

            // openAI 서비스 호출 결과를 HTTP 응답으로 반환
            var httpResponse = req.CreateResponse(HttpStatusCode.OK);
            httpResponse.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            httpResponse.WriteString(message);

            return httpResponse;

        }
    }
}
