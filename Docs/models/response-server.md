# ResponseServer

Универсальная обёртка для ответов API.

## Поля

| Поле | Тип | Описание |
|---|---|---|
| `IsSuccess` | `bool` | Успех операции (по умолчанию `true`) |
| `HttpStatus` | `HttpStatusCode` | HTTP-код (200, 400, 404...) |
| `ErrorMessages` | `List<string>` | Ошибки, пустой при успехе |
| `Result` | `object` | Данные ответа |

## Пример
```csharp
using System.Net;

namespace Api.Model
{
    public class ResponseServer
    {

        public ResponseServer()
        {
            this.IsSuccess = true;
            this.ErrorMessages = new();
        }
        public bool IsSuccess {get; set;}
        public HttpStatusCode HttpStatus  {get; set;}
        public List<string> ErrorMessages {get; set;}
        public object Result {get; set;}
    }
}
```

```csharp
var response = new ResponseServer
{
    HttpStatus = HttpStatusCode.OK,
    Result = await DbContext.Products.ToListAsync()
};
return Ok(response);
```