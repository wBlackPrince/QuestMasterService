# Конфигурация

## Файлы

| Файл | Содержимое |
| --- | --- |
| `backend/QuestMasterService/src/QuestMasterService.Web/appsettings.json` | Уровни `Logging:LogLevel` и `AllowedHosts: "*"`. |
| `appsettings.Development.json` | Те же уровни logging для разработки. |
| `Properties/launchSettings.json` | HTTP profile, localhost:5113 и `ASPNETCORE_ENVIRONMENT=Development`. |
| `backend/Directory.Build.props` | `net10.0`, nullable, analyzers и предупреждения как ошибки для всех проектов. |
| `backend/Directory.Packages.props` | Центральные версии NuGet packages. |

## Переменные окружения

Явно используется только `ASPNETCORE_ENVIRONMENT` в launch profile. В коде она определяет публикацию OpenAPI и Scalar: они отключены в `Production`.

Connection strings, JWT secrets, API keys и прочие секреты не определены. Если они будут добавлены, передавайте их защищённым внешним способом (например, environment variables или secret store), не помещая реальные значения в исходный код и документацию.

## Конфигурации, которых нет

В репозитории не найдены настройки PostgreSQL/EF Core, auth, CORS, distributed tracing или межсервисных клиентов. Пустой `compose.yml` не содержит container configuration.
