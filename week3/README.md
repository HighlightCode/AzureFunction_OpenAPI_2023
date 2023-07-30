# 3주차 Azure Function 

### 1. 목표
- Azure Function App - dotnet(isolated process) + ChatGPT 결합해보기
- 닷넷 버전은 7.0으로 설정하기
- Azure Functions Chat GPT API 를 사용하지 않고, 직접 Chat GPT API 를 호출하여 사용해보기
<br/>

### 2. API를 통한 ChatGPT 질문
OpenAI의 Chat GPT 용 Endpoint 설정("https://api.openai.com/v1/chat/completions") 후, 개인 Private Key(sh 로 시작하는) 를 발급받아 해당 프로젝트를 진행하였습니다.
![Untitled](https://github.com/HighlightCode/AzureFunction_OpenAPI_2023/assets/51946218/8894043d-9a1d-4f32-82c3-f4fc0528ced2)

<br/>

### 3. TODO
처음에 작동이 되다가, Error Message 출력으로 Internal Error(500) 에러가 나오고 있었는데, 이 부분은 Http Req 를 받아왔을 때, OpenAI 서버에서 429 에러를 보내면서 (Too Many Request) Json Parsing 후 생기는 에러였습니다.
Json 의 Body 객체를 핸들링 하는 부분을 조금 더 자연스럽게 할 수 있도록 수정할 예정입니다.
<img src = "https://github.com/HighlightCode/AzureFunction_OpenAPI_2023/assets/51946218/93a62f4a-4f35-44fe-b59f-35594b26a5d4"/>
   
