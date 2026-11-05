# Архитектурный обзор

## Назначение

QuestMaster моделирует предметную область тестирования: пользователя, тест, вопросы трёх типов, варианты/пары сопоставления, попытку и ответы. Реализована именно доменная модель; пользовательские сценарии её сохранения и предоставления через бизнес-API отсутствуют.

## Фактический состав

Единственная solution — `backend/QuestMasterService/QuestMasterService.slnx`. Она содержит один deployable Web API `QuestMasterService.Web` и четыре class library. Поэтому микросервисная архитектура в текущем коде не подтверждается: это один сервис, разложенный на проекты.

| Проект | Роль по коду |
| --- | --- |
| `QuestMasterService.Domain` | Сущности, value objects, enums, `AppError`; зависимость только от CSharpFunctionalExtensions. |
| `QuestMasterService.Contracts` | Пустая библиотека-контракт. |
| `QuestMasterService.Core` | Пустая application-библиотека; ссылается на Domain и Contracts. |
| `QuestMasterService.Infrastructure.Postgres` | Пустая библиотека инфраструктуры; содержит ссылки на EF Core, Npgsql и Dapper. |
| `QuestMasterService.Web` | ASP.NET Core host, диагностический контроллер, OpenAPI/Scalar и health check. |

## Подтверждённые принципы

- Разделение на Domain/Core/Infrastructure/Web похоже на заготовку Clean Architecture.
- Доменные инварианты инкапсулированы в фабриках и value objects; ошибки возвращаются через `Result<T, AppError>`/`UnitResult<AppError>` из CSharpFunctionalExtensions.
- `Nullable` включён, предупреждения и анализаторы трактуются как ошибки на уровне `Directory.Build.props`.
- Версии NuGet централизованы в `backend/Directory.Packages.props`.

DDD можно считать применённым частично: имеются rich domain model и границы агрегатов, но отсутствуют репозитории, application use cases, persistence mapping и интеграционные контракты. Bounded Contexts явно не определены.

См. также [контекст системы](system-context.md), [слои сервиса](microservices.md) и [архитектурные наблюдения](../decisions/architecture-decisions.md).
