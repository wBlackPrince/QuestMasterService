# API

## Общие сведения

Единственный Web API — `QuestMasterService.Web`. Development launch profile использует base URL `http://localhost:5113`. Для других окружений URL не задан: он определяется стандартной конфигурацией ASP.NET Core/hosting.

OpenAPI генерируется встроенным механизмом ASP.NET Core и доступен только если environment не `Production`:

- `GET /openapi/v1.json` — документ OpenAPI;
- `GET /scalar/v1` — Scalar API reference.

## Безопасность и ошибки

Authentication и authorization middleware/атрибуты в коде отсутствуют: текущие endpoints не требуют учётных данных. Единый error format отсутствует. `AppError` — внутренний доменный тип, который не маппится на HTTP.

Полный перечень существующих HTTP-маршрутов с примерами: [endpoints.md](endpoints.md).
