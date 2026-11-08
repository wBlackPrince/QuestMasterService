# QuestMaster

## Database seeding

The seeder applies pending migrations and adds demo questions of all three types, their options/matches, attempts, and answers. It adds 3,000 questions by default and does not delete existing data.

```powershell
.\scripts\seed-database.ps1
.\scripts\seed-database.ps1 -QuestionCount 10000 -BatchSize 500
```

QuestMaster — находящийся в начальной стадии backend-проект для работы с тестами, вопросами и попытками прохождения. На момент актуализации в репозитории присутствует один ASP.NET Core Web API и его доменная модель; frontend, развёртывание и прикладные API ещё не реализованы.

Подробная техническая документация находится в [docs/README.md](docs/README.md).

## Быстрый запуск

Требуется .NET SDK 10.0.401 или совместимый SDK для `net10.0`.

```powershell
dotnet restore backend/QuestMasterService/QuestMasterService.slnx
dotnet run --project backend/QuestMasterService/src/QuestMasterService.Web
```

В профиле разработки приложение слушает `http://localhost:5113`. Доступны `GET /`, `GET /health`, `GET /api/Test`; OpenAPI и Scalar включаются только вне Production.

> Состояние API и инфраструктуры отражает текущую реализацию, а не целевую архитектуру.
Hello
