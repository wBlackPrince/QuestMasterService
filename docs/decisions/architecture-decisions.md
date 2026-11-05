# Архитектурные решения и наблюдения

## Подтверждённые решения

1. **Централизованное управление пакетами.** `Directory.Packages.props` включает Central Package Management и pinning transitive dependencies.
2. **Строгая quality gate на build.** Общий `Directory.Build.props` включает analyzers и трактует предупреждения как ошибки.
3. **Result-based domain errors.** Доменные фабрики не выбрасывают исключения для validation errors, а возвращают CSharpFunctionalExtensions results с `AppError`.
4. **OpenAPI вне production.** `Program.cs` публикует OpenAPI и Scalar при `!Production`, снижая exposure production surface.

## Неподтверждённые намерения (не решения)

Имена проектов и package references позволяют предположить Clean Architecture, FluentValidation/Scrutor в application layer и PostgreSQL/EF Core/Dapper в infrastructure. Без исходного кода, регистраций и конфигурации это не реализованные архитектурные решения.

## Риски и потенциальные проблемы

| Наблюдение | Подтверждение | Последствие |
| --- | --- | --- |
| Сервис пока не микросервисная система | Один Web host, нет других deployable services и коммуникаций. | Нельзя документировать сервисные границы и distributed flows как существующие. |
| Слои Core/Contracts/Infrastructure пусты | В них нет `.cs`-файлов. | Нет use cases, DTO, persistence или integration contracts. |
| Text question через `Test.AddQuestion` нельзя создать | Этот overload вызывает `TextQuestion.Create(... без rightAnswer)`, который передаёт пустую строку в `RightAnswerText.Create`; она запрещена. Результат `.Value` используется без проверки. | Вызов `AddQuestion(QuestionType.Text, ...)` может привести к обращению к failed `Result` вместо корректной ошибки. |
| Инварианты связности не проверяются | `Attempt` принимает GUID без проверки Test/User; answers не проверяют принадлежность question тесту/попытке. | После persistence/API нужны application-level и/или aggregate-level проверки. |
| Правила попыток неполны | `MaxAttempts` не применяется; total score передаётся извне; correctness не вычисляется. | Нельзя считать оценивание реализованным. |
| Нет HTTP error handling | Нет exception middleware/problem details и mapping `AppError`. | Будущие endpoints не имеют единого контрактного формата ошибок. |
| Direct references Web → Domain/Contracts/Core/Infrastructure | Указаны в `Web.csproj`. | Для строгой Clean Architecture это более широкая зависимость, чем необходимо; оценивать после появления composition root. |

Эти пункты — технические наблюдения по текущему коду, а не запрос на изменение реализации.
