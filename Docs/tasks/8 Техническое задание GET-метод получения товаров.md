### Техническое задание

Цель: Преобразовать контроллер ProductController для работы с базой данных, используя Entity Framework Core, и вынести контекст данных AppDbContext в базовый класс StoreController.
___
 

### Шаги реализации

1. **Настройка проекта для работы с Entity Framework Core:**
- Убедитесь, что в проект добавлен пакет Microsoft.EntityFrameworkCore и, если используется MS SQL Server, пакет Microsoft.EntityFrameworkCore.SqlServer.
- Настройте строку подключения к базе данных в appsettings.json и добавьте конфигурацию контекста в Startup.cs или Program.cs (в зависимости от версии ASP.NET Core).

2. **Изменение базового класса StoreController:**
- Убедитесь, что StoreController принимает AppDbContext через конструктор и сохраняет его в защищенное поле. Это позволит использовать контекст в производных классах.


```
public class StoreController : ControllerBase
{
    protected readonly AppDbContext dbContext;

    public StoreController(AppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
}
```     

                  
3. **Модификация контроллера ProductController:**
- Убедитесь, что ProductController наследует StoreController и передает AppDbContext в его конструктор.
- Измените метод Get на GetProducts для получения всех продуктов из базы данных.