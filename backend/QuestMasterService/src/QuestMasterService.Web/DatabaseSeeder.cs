using Microsoft.EntityFrameworkCore;
using QuestMasterService.Domain;
using QuestMasterService.Infrastructure.Postgres;
using System.Security.Cryptography;

namespace QuestMasterService.Web;

public static class DatabaseSeeder
{
    private const int DefaultQuestionCount = 3000;
    private const int DefaultBatchSize = 250;

    public static async Task SeedAsync(
        QuestMasterDbContext db,
        int questionCount = DefaultQuestionCount,
        int batchSize = DefaultBatchSize,
        CancellationToken cancellationToken = default)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(questionCount, 1);
        ArgumentOutOfRangeException.ThrowIfLessThan(batchSize, 1);

        await db.Database.MigrateAsync(cancellationToken);

        var tests = Enumerable.Range(0, 30)
            .Select(index => Test.Create(
                $"Demo test #{index + 1}",
                $"Description for demo test #{index + 1}",
                3).Value)
            .ToArray();

        var testIds = tests.Select(test => test.Id).ToArray();

        db.Tests.AddRange(tests);

        await db.SaveChangesAsync(cancellationToken);
        db.ChangeTracker.Clear();


        var userIds = Enumerable.Range(0, 200).Select(_ => Guid.NewGuid()).ToArray();
        var seededQuestions = new List<SeededQuestion>(questionCount);

        for (var index = 0; index < questionCount; index++)
        {
            var question = CreateQuestion(index, testIds[index % testIds.Length]);
            db.Questions.Add(question.Question);
            db.QuestionOptions.AddRange(question.Options ?? []);
            db.Matches.AddRange(question.Matches ?? []);
            seededQuestions.Add(question);

            if ((index + 1) % batchSize == 0 || index + 1 == questionCount)
            {
                await db.SaveChangesAsync(cancellationToken);
                db.ChangeTracker.Clear();
            }
        }

        var attemptCount = Math.Max(1, questionCount / 5);
        for (var index = 0; index < attemptCount; index++)
        {
            var attempt = Attempt.Create(
                testIds[index % testIds.Length],
                userIds[index % userIds.Length],
                DateTime.UtcNow.AddDays(-RandomNumberGenerator.GetInt32(1, 90))).Value;

            var totalScore = 0m;
            for (var answerIndex = 0; answerIndex < 5; answerIndex++)
            {
                var question = seededQuestions[(index * 5 + answerIndex) % seededQuestions.Count];
                var correctness = RandomNumberGenerator.GetInt32(10) < 7
                    ? AnswerCorrectness.Correct
                    : AnswerCorrectness.Incorrect;
                var points = correctness == AnswerCorrectness.Correct ? 1m : 0m;
                totalScore += points;

                switch (question.Type)
                {
                    case QuestionType.Text:
                        attempt.AddTextAnswer(question.Id, points, correctness,
                            correctness == AnswerCorrectness.Correct ? $"answer-{question.Position}" : "incorrect answer");
                        break;
                    case QuestionType.Option:
                        var optionAnswer = (OptionsAnswer)attempt.AddOptionsAnswer(question.Id, points, correctness).Value;
                        optionAnswer.AddUserOption(correctness == AnswerCorrectness.Correct ? question.RelatedIds[0] : question.RelatedIds[1]);
                        break;
                    case QuestionType.Match:
                        var matchAnswer = (MatchAnswer)attempt.AddMatchAnswer(question.Id, points, correctness).Value;
                        matchAnswer.AddUserMatch(correctness == AnswerCorrectness.Correct ? question.RelatedIds[0] : question.RelatedIds[1]);
                        break;
                }
            }

            attempt.Finish(totalScore);
            db.Attempts.Add(attempt);

            if ((index + 1) % batchSize == 0 || index + 1 == attemptCount)
            {
                await db.SaveChangesAsync(cancellationToken);
                db.ChangeTracker.Clear();
            }
        }

        Console.WriteLine($"Seed completed: {questionCount:N0} questions, {attemptCount:N0} attempts and {attemptCount * 5:N0} answers.");
    }

    private static SeededQuestion CreateQuestion(int index, Guid testId)
    {
        var description = QuestionDescription.Create($"Demo question #{index + 1}").Value;
        var points = PointsCost.Create(1).Value;
        var position = PositionIndex.Create(index).Value;

        return (index % 3) switch
        {
            0 => CreateTextQuestion(index, testId, description, points, position),
            1 => CreateOptionQuestion(index, testId, description, points, position),
            _ => CreateMatchQuestion(index, testId, description, points, position)
        };
    }

    private static SeededQuestion CreateTextQuestion(int index, Guid testId, QuestionDescription description, PointsCost points, PositionIndex position)
    {
        var question = TextQuestion.Create(Guid.NewGuid(), description, points, testId, position, $"answer-{index}").Value;
        return new SeededQuestion(question, QuestionType.Text, index, []);
    }

    private static SeededQuestion CreateOptionQuestion(int index, Guid testId, QuestionDescription description, PointsCost points, PositionIndex position)
    {
        var question = OptionQuestion.Create(Guid.NewGuid(), description, points, testId, position).Value;
        var options = new List<QuestionOption>(4);
        for (var optionIndex = 0; optionIndex < 4; optionIndex++)
        {
            var option = QuestionOption.Create($"Option {optionIndex + 1} for question {index + 1}", optionIndex == 0, question.Id).Value;
            question.AddOption(option.Id);
            options.Add(option);
        }

        return new SeededQuestion(question, QuestionType.Option, index, options.Select(option => option.Id).ToArray(), options, []);
    }

    private static SeededQuestion CreateMatchQuestion(int index, Guid testId, QuestionDescription description, PointsCost points, PositionIndex position)
    {
        var question = MatchQuestion.Create(Guid.NewGuid(), description, points, testId, position).Value;
        var matches = new List<Match>(3);
        for (var matchIndex = 0; matchIndex < 3; matchIndex++)
        {
            var match = Match.Create($"Left {matchIndex + 1} / question {index + 1}", $"Right {matchIndex + 1} / question {index + 1}", matchIndex == 0, question.Id).Value;
            question.AddMatch(match.Id);
            matches.Add(match);
        }

        return new SeededQuestion(question, QuestionType.Match, index, matches.Select(match => match.Id).ToArray(), [], matches);
    }

    private sealed record SeededQuestion(Question Question, QuestionType Type, int Position, Guid[] RelatedIds,
        IReadOnlyCollection<QuestionOption>? Options = null, IReadOnlyCollection<Match>? Matches = null)
    {
        public Guid Id => Question.Id;
    }
}
