# Тестирование и качество

Тестовые проекты и тестовые файлы в репозитории отсутствуют, поэтому команда `dotnet test` не имеет подтверждённого тестового набора.

Проверка компиляции solution:

```powershell
dotnet build backend/QuestMasterService/QuestMasterService.slnx
```

Для всех проектов включены nullable reference types, .NET analyzers, Roslynator, Meziantou.Analyzer, AsyncFixer и SonarAnalyzer.CSharp. `TreatWarningsAsErrors` и `CodeAnalysisTreatWarningsAsErrors` включены, поэтому warning может остановить build.

Минимальная smoke-проверка запущенного API описана в [setup.md](setup.md). При развитии проекта приоритетны unit-тесты value objects и aggregate operations, затем integration tests HTTP/persistence; это рекомендация, а не уже принятая реализация.
