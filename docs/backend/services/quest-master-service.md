# QuestMasterService: домен и слои

## Domain

### Сущности и связи

| Объект | Роль и связи |
| --- | --- |
| `User` | Идентификатор `Guid`, изменяемый `UserName`. Изолирован от остальных объектов, кроме `Attempt.UserId`. |
| `Test` | Кандидат в aggregate root: title, description, maximum attempts и коллекция `Question`; управляет добавлением/удалением questions. |
| `Question` | Абстрактная база: description, type, points cost, test id, position. Реализации: `TextQuestion`, `MatchQuestion`, `OptionQuestion`. |
| `QuestionOption` | Вариант option question, связан `QuestionId`; сам вопрос хранит только список GUID. |
| `Match` | Левая/правая стороны пары и признак correct, связан `QuestionId`; вопрос хранит GUID пар. |
| `Attempt` | Кандидат в aggregate root: test/user IDs, время начала/окончания, total score и answers. |
| `Answer` | Абстрактная база. Реализации: `TextAnswer`, `MatchAnswer`, `OptionsAnswer`; ссылка на question и attempt. |

Коллекции выдаются как `IReadOnlyList`, а изменение коллекций производится методами сущностей. Идентификаторы генерируются внутри factory methods, кроме ID question, который передаётся вызывающим кодом.

### Value objects и правила

| Value object | Инвариант |
| --- | --- |
| `UserName` | Непустой, до 50 символов. |
| `TestTitle` | Непустой, до 200 символов. |
| `TestDescription`, `QuestionDescription` | Непустые, до 2000 символов. |
| `MaxAttempts` | От 1 до 100. |
| `PositionIndex` | Неотрицательный. |
| `PointsCost`, `PointsAwarded` | От 0 до 1000. |
| `TotalScore` | Неотрицательный. |
| `RightAnswerText`, `UserAnswerText`, `OptionText`, `MatchLeft`, `MatchRight` | Непустые, до 500 символов. |

`AppError` содержит единственное поле `Message`. Все сообщения в текущем коде — английские. Типы enum: `QuestionType`/`AnswerType` (`Text`, `Match`, `Option`) и `AnswerCorrectness` (`NotAnswered`, `Incorrect`, `PartiallyCorrect`, `Correct`).

### Публичные доменные операции

- `User.Create`, `ChangeName`;
- `Test.Create`, `ChangeTitle`, `ChangeDescription`, `ChangeMaxAttempts`, `AddQuestion`, `RemoveQuestion`;
- у всех concrete question: изменение description/points/position; у text — `SetRightAnswer`; у option/match — add/remove ID;
- `QuestionOption`/`Match`: создание и изменение значений и correctness;
- `Attempt.Create`, добавление трёх видов answer, `Finish`;
- у answers: изменение пользовательского значения/selection, awarded points и correctness.

Попытка запрещает добавление ответов после `Finish` и повторное завершение. Дубликаты option ID и match ID запрещены. Автоматическая проверка ответа, контроль лимита `MaxAttempts`, сопоставление answer с реальным test/question и расчёт score не реализованы.

## Остальные проекты

`Contracts`, `Core` и `Infrastructure.Postgres` не содержат `.cs` файлов. При этом Core уже ссылается на FluentValidation и Scrutor, а infrastructure — на EF Core, EF design-time, Npgsql и Dapper. Это подготовка к реализации, а не доступные публичные интерфейсы.

## Web API

В `Web` есть `TestController`; его `TestResponse` объявлен рядом с контроллером, а не в Contracts. Полный контракт — [API endpoints](../../api/endpoints.md).
