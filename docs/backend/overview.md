# Backend

Backend расположен в `backend/QuestMasterService` и нацелен на .NET 10 (`net10.0`). В solution нет тестового проекта и нет source-кода в Contracts/Core/Infrastructure.Postgres. Следовательно, единственная исполняемая часть — Web host и диагностический controller.

## Runtime

`QuestMasterService.Web/Program.cs`:

- регистрирует native OpenAPI (`AddOpenApi`), controllers и общие health checks;
- маппит `/`, `/health` и MVC controllers;
- в любой среде, кроме `Production`, публикует `/openapi/v1.json` и Scalar UI `/scalar/v1`;
- не регистрирует authentication/authorization, CORS, exception middleware, DI-компоненты Core/Infrastructure, EF Core или logging provider.

Стандартное ASP.NET Core logging конфигурируется уровнями `Information` и `Warning` в `appsettings*.json`.

## Доменная модель

Подробный каталог сущностей, правил и публичных операций — [QuestMasterService](services/quest-master-service.md). Domain опирается на CSharpFunctionalExtensions для явного возврата бизнес-ошибок; преобразование этих ошибок в HTTP response не реализовано.
