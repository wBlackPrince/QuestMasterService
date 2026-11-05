# Настройка разработки

## Требования

- .NET SDK, поддерживающий `net10.0`. В исследованной среде установлен SDK `10.0.401` и ASP.NET Core runtime `10.0.12`.
- Git.

Node.js, Docker, PostgreSQL и другие сервисы для текущего запуска не требуются: frontend и database integration отсутствуют.

## Restore, build, run

Запускайте команды из корня репозитория:

```powershell
dotnet restore backend/QuestMasterService/QuestMasterService.slnx
dotnet build backend/QuestMasterService/QuestMasterService.slnx
```

`QuestMasterService.Web` ожидает PostgreSQL connection string с именем `Postgres`. Перед запуском задайте его через защищённую конфигурацию, например:

```powershell
$env:ConnectionStrings__Postgres = "Host=<host>;Port=5432;Database=<database>;Username=<user>;Password=<password>"
dotnet run --project backend/QuestMasterService/src/QuestMasterService.Web
```

`launchSettings.json` задаёт для профиля `http` адрес `http://localhost:5113` и `ASPNETCORE_ENVIRONMENT=Development`. После запуска проверьте:

```powershell
Invoke-WebRequest http://localhost:5113/health
Invoke-RestMethod http://localhost:5113/api/Test
```

Development OpenAPI UI: `http://localhost:5113/scalar/v1`.

## Миграции EF Core

Из каталога `backend/QuestMasterService` создайте начальную миграцию:

```powershell
dotnet ef migrations add InitialCreate `
    --project .\src\QuestMasterService.Infrastructure.Postgres\QuestMasterService.Infrastructure.Postgres.csproj `
    --startup-project .\src\QuestMasterService.Web\QuestMasterService.Web.csproj `
    --output-dir Persistence\Migrations
```

## Запуск всего проекта

`compose.yml` запускает PostgreSQL без volume. Запустите инфраструктуру из корня репозитория командой `docker compose up -d`, после чего используйте строку подключения к `localhost:5432`. Отдельных микросервисов или frontend-проекта нет.

## Отладка

Откройте `backend/QuestMasterService/QuestMasterService.slnx` в IDE с поддержкой .NET 10 и выберите startup project `QuestMasterService.Web`. Либо запустите `dotnet run` в Debug configuration и присоедините debugger к процессу.
