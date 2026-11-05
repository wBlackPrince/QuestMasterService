# Диагностика

## Приложение недоступно на localhost:5113

Убедитесь, что запускался именно `QuestMasterService.Web`, а не class library. Проверьте адрес в консоли `dotnet run`; его можно переопределить стандартными ASP.NET Core настройками URLs.

## Нет OpenAPI/Scalar

`/openapi/v1.json` и `/scalar/v1` намеренно не маппятся при `ASPNETCORE_ENVIRONMENT=Production`. Установите Development или используйте профиль из `launchSettings.json`.

## Build останавливается из-за warning

Это ожидается из-за централизованных `TreatWarningsAsErrors`, `CodeAnalysisTreatWarningsAsErrors` и подключённых analyzers. Исправляйте источник предупреждения; не отключайте правило без согласованного решения.

## Ошибка доступа к `obj` при build

Сборка создаёт intermediate files рядом с каждым `.csproj`. В изолированной среде документации запись была разрешена только в подкаталог `Attempts`, поэтому build solution завершился `Access denied` при создании файлов в `Contracts/obj` и `Domain/obj`. Это ограничение окружения, не установленная ошибка исходного кода. В обычной рабочей копии убедитесь, что каталог репозитория доступен на запись, затем выполните restore/build.

## Ожидание PostgreSQL не приводит к подключению

Наличие Npgsql и EF Core package references не создаёт базу и не настраивает подключение. До появления `DbContext`, connection string и миграций сервис к PostgreSQL не обращается.
