# HTTP endpoints

## `GET /`

Назначение: liveness-ответ корневого minimal API.

- Авторизация: не требуется.
- Параметры: нет.
- Успех: `200 OK`, `text/plain`.

```http
GET http://localhost:5113/
```

Тело ответа:

```text
QuestMasterService is running!
```

## `GET /health`

Назначение: endpoint ASP.NET Core Health Checks. В DI добавлены только общие health checks; EF/database checks не зарегистрированы.

- Авторизация: не требуется.
- Параметры: нет.
- Успех: `200 OK`, тело и content type определяются встроенным middleware.

```http
GET http://localhost:5113/health
```

## `GET /api/Test`

Назначение: тестовый MVC endpoint; не предоставляет тесты предметной области.

- Авторизация: не требуется.
- Route/query/request body: отсутствуют.
- Ответ: `200 OK`, JSON `TestResponse`.

```http
GET http://localhost:5113/api/Test
Accept: application/json
```

```json
{
  "message": "Test controller is working.",
  "timestamp": "2026-11-05T12:34:56.789+00:00"
}
```

`timestamp` формируется как `DateTimeOffset.UtcNow`; значение в примере иллюстративно. Другие status codes, DTO запросов и бизнес endpoints в коде не объявлены.

## Development-only: OpenAPI и Scalar

```http
GET http://localhost:5113/openapi/v1.json
GET http://localhost:5113/scalar/v1
```

Оба маршрута подключаются только при `!app.Environment.IsProduction()`.
