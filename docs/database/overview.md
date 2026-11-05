# Хранение данных

Работающая база данных не настроена. В `appsettings.json` и `appsettings.Development.json` нет connection strings; в solution не найдены `DbContext`, EF configurations, migrations, repositories, Dapper queries или вызовы `UseNpgsql`.

Проект `QuestMasterService.Infrastructure.Postgres` ссылается на `Microsoft.EntityFrameworkCore`, `Microsoft.EntityFrameworkCore.Design`, `Npgsql.EntityFrameworkCore.PostgreSQL` и `Dapper`. Это указывает на планируемую PostgreSQL-инфраструктуру, но не подтверждает схему, имя базы, подключения или persistence strategy.

Не следует добавлять connection string в tracked `appsettings*.json`. Когда persistence появится, документацию нужно дополнить перечнем миграций, моделью данных, переменными окружения, жизненным циклом миграций и health-check configuration.
