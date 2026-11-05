# Хранение данных

В проекте `QuestMasterService.Infrastructure.Postgres` добавлен `QuestMasterDbContext` и EF Core-конфигурации моделей попыток, ответов, вопросов, пар и вариантов. В `Program.cs` контекст подключён через Npgsql.

Для запуска требуется connection string с именем `Postgres` (например, environment variable `ConnectionStrings__Postgres`). В tracked `appsettings*.json` значения нет. Миграции, repositories и Dapper queries пока отсутствуют.

Не следует добавлять connection string в tracked `appsettings*.json`. Когда persistence появится, документацию нужно дополнить перечнем миграций, моделью данных, переменными окружения, жизненным циклом миграций и health-check configuration.
