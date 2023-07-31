# azure-functions-openapi-extension

azure-functions-openapi-extension 를 위한 개발 환경 세팅 및 준비사항 입니다.
Github [링크](https://github.com/Azure/azure-functions-openapi-extension)

<br/>

## Visual Studio 에서 빌드하기

먼저, visual studio 빌드를 하기 전에, Visual Studio Installer 를 통해 .net5.0 / .net6.0 / .net7.0 / Azure Functions Core Tools 4.0 를 다운받아 설치를 진행해줍니다. 
visual studio 에서, [https://github.com/Azure/azure-functions-openapi-extension](https://github.com/Azure/azure-functions-openapi-extension) 을 링크에서 다운 받은 후, 솔루션 빌드를 실행해줍니다. 

```csharp
dotnet restore && dotnet build
```

그리고 Azure Core Tools 를 설치 해준후 ([링크](https://learn.microsoft.com/en-us/azure/azure-functions/functions-run-local?tabs=windows%2Cportal%2Cv2%2Cbash&pivots=programming-language-csharp)), Azure Func 버전을 확인해줍니다.

```csharp
func --version
```

그 다음 Test 를 위해, OutProc 과 InProc 으로 나뉜 부분으로 갈 수 있는데, 여기서, InProc 폴더로 이동을 진행한 다음 명령어를 통해 Test 를 진행합니다.

```csharp
Func Start
```

그리고 local host 7071 포트를 열어주는 메시지가 나온 뒤, local host 에서의 Swagger UI 가 나오게 됩니다.

![Untitled (1)](https://github.com/HighlightCode/AzureFunction_OpenAPI_2023/assets/51946218/215b8e2f-2224-40c5-8be0-ad9f147f4178)

그 다음 OutOfProc 으로 이동하여, 해당 폴더에서 Func Start 명령어를 통해 out proc 을 실행시켜 주고, 마찬가지로 위와 같은 localhost::7071/api/swagger/ui 링크에서 Swagger UI 를 확인해줍니다.

In Proc 과 Out Proc 이 작동되어지는지 확인을 한 후, Out Proc 에서의 local.settings.json 을 변형시켜, 해당 settings 가 어떻게 바뀌는지 확인을 해봅니다

우선 local.settings.json : "OpenApi__DocTitle” 이  현재 Swagger Petstore 라고 적혀져 있는데, 이 부분을 **"Contribution Academy"** 로 변경한 결과 다음과 같이 

![Untitled (2)](https://github.com/HighlightCode/AzureFunction_OpenAPI_2023/assets/51946218/c242d4eb-6b8d-4bcb-92c9-fc8412b31c5d)

**Contibution Academy** 로 변경되어진 결과를 확인할 수 있습니다. 
Inproc 과 OutProc 은 버전 차이라고 볼 수 있는데, InProc 의 경우, 기존에 작동하던 레거시 버전(초기 MVP 버전)의 코드로 볼 수 있으며, OutProc 은 차세대의 버전 업 버전이라고 생각됩다.

이렇게 개발 환경 세팅은 끝낼 수 있습니다.

여기서 알아둬야 할 점은 Azure Function 을 개발하는 실무에서는 위와 같이 소스코드를 빌드해서 사용하는 환경이 아닌, Package 형태로 다운받아 사용을 한다는 점을 알아둬야 합니다.

<br/>
<br/>

## 패키지 형태의 Azure Functions 사용하기

패키지 형태의 Azure Functions 를 만들려면, 해당 프로세스를 따라온다. 원하는 폴더에서 Func Init 을 통해 Azure Functions 를 위한 프로젝트 세팅을 진행해줍니다.

```csharp
Func init
dotnet
c#
```

![Untitled (3)](https://github.com/HighlightCode/AzureFunction_OpenAPI_2023/assets/51946218/706bdfbb-bef8-4894-b2b0-b0c6747b0c3c)

그 다음 패키지를 빌드 후, Azure Functions 를 실행해줍니다.

```csharp
dotnet restore && dotnet build
Func Start
```

그리고 해당 host에 접속해준다. ([http://localhost:7071/](http://localhost:7071/)) 위와 같이 Swagger UI 에 접속하면 **오류**가 나게 됩니다. 아직은 App을 설정해주지 않았기 때문입니다.

```csharp
Func New
```

![Untitled (4)](https://github.com/HighlightCode/AzureFunction_OpenAPI_2023/assets/51946218/6970912c-7a5a-4394-aab3-6646605dc012)

위와 같이 입력 후 Trigger 를 만들 <이름> 을 설정해줍니다. 여기서는 OcaTrigger 로 설정해 주었습니다.

[http://localhost:7071/api/OcaTrigger](http://localhost:7071/api/OcaTrigger) 로 접속하여 다음 메시지가 제대로 출력이 되는지 확인한다.

![Untitled (5)](https://github.com/HighlightCode/AzureFunction_OpenAPI_2023/assets/51946218/cc36f103-7ca7-46fd-84b6-8bef21b37522)

그리고 Http Trigger 에서 해당 Trigger 에 해당하는 함수를 등록하여(여기서는 Greeting 으로 인자를 보내줌) 다음과 같은 메시지를 확인할 수 있습니다.

![Untitled (6)](https://github.com/HighlightCode/AzureFunction_OpenAPI_2023/assets/51946218/8d1f0c8e-94aa-40c5-9aa8-f91e86fed74e)

<br/>
<br/>

## Dotnet 패키지 관리 및 Webjobs 적용

dotnet 패키지를 관리하고, **Webjobs 를 적용시킨 Azure Functions 를 실행**시킬 수 있는데, 절차는 다음과 같습니다.

[Microsoft.Azure.WebJobs.Extensions.OpenApi 1.5.1](https://www.nuget.org/packages/Microsoft.Azure.WebJobs.Extensions.OpenApi/)

Azure OpenAPI 를 설치를 진행시켜준 후, Azure 에서의 OpenAPI 를 적용시켜봅니다. dotnet 패키지 설치를 위한 다음 명령어를 적용시켜 준 후, 

```csharp
dotnet add package Microsoft.Azure.WebJobs.Extensions.OpenApi --version 1.5.1
```

빌드를 진행 시켜줍니다.

```csharp
dotnet restore && dotnet build
```

그리고 다음과 같은 패키지 충돌이 일어났지만, 당황하지 않고, 

![Untitled (7)](https://github.com/HighlightCode/AzureFunction_OpenAPI_2023/assets/51946218/da403249-9d20-4297-a9dd-73c694cd92d7)

에러를 읽어본 후, 4.1.1 버전을 업그레이드 시켜줍니다.

```csharp
dotnet add package Microsoft.Azure.WebJobs.Extensions.OpenApi --version 1.5.1
```

그리고, 다시 명령어 실행시켜준 후,

```csharp
dotnet restore && dotnet build
```

그리고 Func Start 를 통해 localhost::7071 에 접속하게 된다면, Swagger UI 가 접속이 되어지는 점을 확인할 수 있습니다.

그러면 다음과 같은 UI 가 생성된 점을 확인할 수 있으며, Swagger UI 도 잘 나오는 점을 확인할 수 있습니다.

![Untitled (8)](https://github.com/HighlightCode/AzureFunction_OpenAPI_2023/assets/51946218/32a07b69-7851-4b77-81a5-ddd33a930f18)

![Untitled (9)](https://github.com/HighlightCode/AzureFunction_OpenAPI_2023/assets/51946218/30bfb6ee-bca9-4906-be45-714c964c0fb5)

<br/>
<br/>

## 내가 만드는 새로운 API

새로운 API 를 만들어 주기 위해서 다음 명령어를 통해 새로운 Function Trigger를 추가시켜 줍니다.

```csharp
func new
```
그 후 생성되는 OcaTriggers.cs 파일에서 다음 코드를 추가해준 후(Attribute 추가.)

해당 패키지를 가져오는 코드를 삽입해줍니다

```csharp
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;

// 생략 ..
// [FunctionName("OcaTrigger")] 밑줄에 추가
[OpenApiOperation(operationId: "Run", tags: new[] { "name" }, Summary = "The name of the person to use in the greeting.", Description = "This HTTP triggered function returns a person's name.", Visibility = OpenApiVisibilityType.Important)]
```

그리구 dotnet build → func start 를 통해 반영이 되었는지 확인을 해봅니다.
그 후 **Greeting 메시지가 추가**된 것을 확인할 수 있습니다.

<br/>
<br/>

## 과제 : dotnet(isolated process) 로 결과물을 빌드하여 제출하기

![Untitled (10)](https://github.com/HighlightCode/AzureFunction_OpenAPI_2023/assets/51946218/3ef5efdd-2237-4be1-add3-2d3fb26d971b)

![Untitled (11)](https://github.com/HighlightCode/AzureFunction_OpenAPI_2023/assets/51946218/5ab9b3c4-bd16-4586-8ee2-069e56db380a)
