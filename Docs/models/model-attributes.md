# Атрибуты моделей

```csharp
using System.ComponentModel.DataAnnotations;
```

## 📋 Валидация

| Атрибут | Для чего | Пример |
|---|---|---|
| `[Required]` | Поле обязательно | `[Required] public string Name` |
| `[StringLength(max)]` | Ограничение длины строки | `[StringLength(50, MinimumLength = 3)]` |
| `[Range(min, max)]` | Число в диапазоне | `[Range(1, 1000)]` |
| `[RegularExpression(pattern)]` | Regex-проверка | `[RegularExpression(@"^[A-Z].*")]` |
| `[EmailAddress]` | Формат email | `[EmailAddress] public string Email` |
| `[Phone]` | Формат телефона | `[Phone] public string PhoneNumber` |
| `[Url]` | Формат URL | `[Url] public string Website` |
| `[Compare(other)]` | Совпадение с другим полем | `[Compare("Password")]` |
| `[MinLength(n)]` | Минимальная длина | `[MinLength(5)]` |
| `[MaxLength(n)]` | Максимальная длина | `[MaxLength(100)]` |

## 🏷️ Метаданные

| Атрибут | Для чего |
|---|---|
| `[Display(Name)]` | Читаемое имя поля |
| `[DataType(type)]` | Семантика (Date, Currency, EmailAddress) |
| `[DisplayFormat]` | Формат отображения |

## 🗄️ EF Core

| Атрибут | Для чего |
|---|---|
| `[Key]` | Первичный ключ |
| `[ForeignKey("NavProperty")]` | Внешний ключ |
| `[Column(TypeName = "decimal(18,2)")]` | Тип колонки в БД |
| `[NotMapped]` | Исключить из маппинга |

## 🔗 Привязка данных

| Атрибут | Откуда брать |
|---|---|
| `[FromBody]` | Тело запроса (JSON) |
| `[FromQuery]` | Query (`?id=5`) |
| `[FromRoute]` | URL-путь (`/5`) |
| `[FromHeader]` | HTTP-заголовки |
| `[FromForm]` | Форма |

## 💡 Замечание

Для value-типов (`int`, `decimal`, `DateTime`, `bool`) `[Required]` не нужен — они не могут быть `null`. Актуален только для `string` и nullable-типов (`int?`).