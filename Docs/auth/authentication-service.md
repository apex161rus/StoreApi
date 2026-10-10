# AuthenticationServiceExtension

Расширение для настройки JWT-аутентификации в ASP.NET Core.

## Назначение

Класс `AuthenticationServiceExtension` — это **extension method** для `IServiceCollection`,
который настраивает **JWT-аутентификацию** в приложении.

**Что делает:**
- Регистрирует схему аутентификации `JwtBearer`.
- Настраивает параметры проверки токена.
- Указывает секретный ключ для проверки подписи.

**Где вызывается:** в `Program.cs` при настройке сервисов.

## Полный код

```csharp
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Api.Extension
{
    public static class AuthenticationServiceExtension
    {
        public static IServiceCollection AddAuthenticationConfig(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var authsettingsToken = configuration["AuthSettings:Secretkey"];

            services.AddAuthentication(t =>
            {
                t.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                t.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(t =>
            {
                t.RequireHttpsMetadata = false;
                t.SaveToken = true;
                t.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.ASCII.GetBytes(authsettingsToken)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            return services;
        }
    }
}
```

## Разбор по строкам

### `using System.Text;`

**Что:** пространство имён для `Encoding`.
**Зачем:** используется `Encoding.ASCII.GetBytes(...)`.

### `using Microsoft.AspNetCore.Authentication.JwtBearer;`

**Что:** пространство имён для JWT-аутентификации.
**Зачем:** даёт `JwtBearerDefaults`, `AddJwtBearer`.

### `using Microsoft.IdentityModel.Tokens;`

**Что:** пространство имён для токенов.
**Зачем:** даёт `TokenValidationParameters`, `SymmetricSecurityKey`.

### `namespace Api.Extension`

**Что:** пространство имён для extension-классов.
**Зачем:** все расширения в одном месте.

### `public static class AuthenticationServiceExtension`

**Что:** статический класс с extension-методом.
**Зачем:** extension-методы должны быть в статическом классе.

### `public static IServiceCollection AddAuthenticationConfig(...)`

**Что:** extension-метод для `IServiceCollection`.
**Зачем:** чтобы вызывать как `builder.Services.AddAuthenticationConfig(...)`.
**`this IServiceCollection services`** — первый параметр = цель расширения.

### `IConfiguration configuration`

**Что:** конфигурация приложения (`appsettings.json`).
**Зачем:** чтобы прочитать секретный ключ.

### `var authsettingsToken = configuration["AuthSettings:Secretkey"];`

**Что:** читает ключ из конфига.
**`AuthSettings:Secretkey`** — путь в JSON:

```json
{
  "AuthSettings": {
    "Secretkey": "..."
  }
}
```

Двоеточие = вложенность (`AuthSettings` → `Secretkey`).

### `services.AddAuthentication(t => { ... })`

**Что:** регистрирует сервисы аутентификации.
**`t`** — лямбда с настройками `AuthenticationOptions`.

### `t.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;`

**Что:** какая схема используется по умолчанию для аутентификации.
**`JwtBearerDefaults.AuthenticationScheme`** — строка `"Bearer"`.
**Зачем:** когда приходит запрос, ASP.NET Core знает, как проверять токен.

### `t.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;`

**Что:** какая схема используется, если нужно вернуть `401`.
**Зачем:** согласованность — одна схема везде.

### `.AddJwtBearer(t => { ... })`

**Что:** добавляет JWT Bearer аутентификацию.
**`t`** — лямбда с `JwtBearerOptions`.

### `t.RequireHttpsMetadata = false;`

**Что:** разрешает работу без HTTPS.
**Зачем:** для разработки (localhost).
**⚠️ Важно:** в проде — `true` (HTTPS обязателен).

### `t.SaveToken = true;`

**Что:** сохраняет токен в `HttpContext`.
**Зачем:** потом можно достать через `HttpContext.GetTokenAsync("access_token")`.

### `t.TokenValidationParameters = new TokenValidationParameters { ... }`

**Что:** параметры проверки токена.
**Зачем:** что проверять в JWT.

### `ValidateIssuerSigningKey = true;`

**Что:** проверять подпись токена.
**Зачем:** убедиться, что токен не подделан.

### `IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(authsettingsToken))`

**Что:** ключ для проверки подписи.
**`SymmetricSecurityKey`** — симметричный ключ (HMAC).
**`Encoding.ASCII.GetBytes(...)`** — строка → байты.
**⚠️ Важно:** должен совпадать с `JwtTokenGenerator`!

### `ValidateIssuer = false;`

**Что:** не проверять издателя (`iss`).
**Зачем:** для учебного — упрощение.
**В проде** — `true` + `ValidIssuer = "..."`.

### `ValidateAudience = false;`

**Что:** не проверять получателя (`aud`).
**Зачем:** то же — упрощение.

### `return services;`

**Что:** возвращает `IServiceCollection`.
**Зачем:** для цепочки (`builder.Services.A().B().C()`).

## Использование в Program.cs

```csharp
// 1. Регистрация сервисов
builder.Services.AddAuthenticationConfig(builder.Configuration);

var app = builder.Build();

// 2. Подключение middleware
app.UseAuthentication();   // ← кто ты
app.UseAuthorization();    // ← что можно
```

## Порядок middleware

```
UseRouting()
UseCors()
UseAuthentication()      ← должен быть ДО UseAuthorization
UseAuthorization()
MapControllers()
```

## Важные нюансы

1. **Ключ должен совпадать с `JwtTokenGenerator`.**
   Если `ASCII` здесь — и `ASCII` там.
   Если `UTF8` здесь — и `UTF8` там.

2. **`RequireHttpsMetadata = false`** — только для разработки.
   В проде — `true`.

3. **`ValidateIssuer = false`** и **`ValidateAudience = false`** — для учебного.
   В проде — `true` + настройка `ValidIssuer`/`ValidAudience`.

4. **Секрет** — должен быть длинным (≥ 32 символа для SHA256, ≥ 64 для SHA512).

5. **Секрет** — в `user-secrets`, не в `appsettings.json` (если не pet-проект).

## Связанные файлы

- `Api/Service/JwtTokenGenerator.cs` — генерация токена.
- `Api/Program.cs` — вызов расширения.
- `Api/appsettings.json` — секретный ключ.