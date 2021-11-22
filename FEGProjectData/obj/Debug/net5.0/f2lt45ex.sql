IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Exams] (
    [ExamId] int NOT NULL IDENTITY,
    [Name] nvarchar(256) NOT NULL,
    CONSTRAINT [PK_Exams] PRIMARY KEY ([ExamId])
);
GO

CREATE TABLE [Groups] (
    [GroupId] int NOT NULL IDENTITY,
    [Name] nvarchar(256) NOT NULL,
    CONSTRAINT [PK_Groups] PRIMARY KEY ([GroupId])
);
GO

CREATE TABLE [Questions] (
    [QuestionId] int NOT NULL IDENTITY,
    [Text] nvarchar(256) NOT NULL,
    [Type] int NOT NULL,
    CONSTRAINT [PK_Questions] PRIMARY KEY ([QuestionId])
);
GO

CREATE TABLE [AssignedExams] (
    [AssignedExamId] int NOT NULL IDENTITY,
    [Deadline] datetime2 NOT NULL,
    [Password] int NOT NULL,
    [ExamId] int NOT NULL,
    CONSTRAINT [PK_AssignedExams] PRIMARY KEY ([AssignedExamId]),
    CONSTRAINT [FK_AssignedExams_Exams_ExamId] FOREIGN KEY ([ExamId]) REFERENCES [Exams] ([ExamId]) ON DELETE CASCADE
);
GO

CREATE TABLE [Students] (
    [StudentId] int NOT NULL IDENTITY,
    [FirstName] nvarchar(100) NOT NULL,
    [LastName] nvarchar(100) NOT NULL,
    [Adderss] nvarchar(100) NOT NULL,
    [EmailAddress] nvarchar(100) NULL,
    [GroupId] int NOT NULL,
    CONSTRAINT [PK_Students] PRIMARY KEY ([StudentId]),
    CONSTRAINT [FK_Students_Groups_GroupId] FOREIGN KEY ([GroupId]) REFERENCES [Groups] ([GroupId]) ON DELETE CASCADE
);
GO

CREATE TABLE [ExamQuestion] (
    [ExamsExamId] int NOT NULL,
    [QuestionsQuestionId] int NOT NULL,
    CONSTRAINT [PK_ExamQuestion] PRIMARY KEY ([ExamsExamId], [QuestionsQuestionId]),
    CONSTRAINT [FK_ExamQuestion_Exams_ExamsExamId] FOREIGN KEY ([ExamsExamId]) REFERENCES [Exams] ([ExamId]) ON DELETE CASCADE,
    CONSTRAINT [FK_ExamQuestion_Questions_QuestionsQuestionId] FOREIGN KEY ([QuestionsQuestionId]) REFERENCES [Questions] ([QuestionId]) ON DELETE CASCADE
);
GO

CREATE TABLE [QuestionOptions] (
    [QuestionOptionId] int NOT NULL IDENTITY,
    [Option] nvarchar(1) NOT NULL,
    [Text] nvarchar(256) NOT NULL,
    [QuestionId] int NOT NULL,
    CONSTRAINT [PK_QuestionOptions] PRIMARY KEY ([QuestionOptionId]),
    CONSTRAINT [FK_QuestionOptions_Questions_QuestionId] FOREIGN KEY ([QuestionId]) REFERENCES [Questions] ([QuestionId]) ON DELETE CASCADE
);
GO

CREATE TABLE [AssignedExamGroup] (
    [AssignedExamsAssignedExamId] int NOT NULL,
    [GroupsGroupId] int NOT NULL,
    CONSTRAINT [PK_AssignedExamGroup] PRIMARY KEY ([AssignedExamsAssignedExamId], [GroupsGroupId]),
    CONSTRAINT [FK_AssignedExamGroup_AssignedExams_AssignedExamsAssignedExamId] FOREIGN KEY ([AssignedExamsAssignedExamId]) REFERENCES [AssignedExams] ([AssignedExamId]) ON DELETE CASCADE,
    CONSTRAINT [FK_AssignedExamGroup_Groups_GroupsGroupId] FOREIGN KEY ([GroupsGroupId]) REFERENCES [Groups] ([GroupId]) ON DELETE CASCADE
);
GO

CREATE TABLE [StudentAssignedExams] (
    [StudentAssignedExamId] int NOT NULL IDENTITY,
    [DeliveryTime] datetime2 NOT NULL,
    [Result] float NOT NULL,
    [StudentId] int NOT NULL,
    [AssignedExamId] int NOT NULL,
    CONSTRAINT [PK_StudentAssignedExams] PRIMARY KEY ([StudentAssignedExamId]),
    CONSTRAINT [FK_StudentAssignedExams_AssignedExams_AssignedExamId] FOREIGN KEY ([AssignedExamId]) REFERENCES [AssignedExams] ([AssignedExamId]) ON DELETE CASCADE,
    CONSTRAINT [FK_StudentAssignedExams_Students_StudentId] FOREIGN KEY ([StudentId]) REFERENCES [Students] ([StudentId]) ON DELETE CASCADE
);
GO

CREATE TABLE [StudentAnswers] (
    [StudentAnswerId] int NOT NULL IDENTITY,
    [Answer] nvarchar(2000) NOT NULL,
    [StudentAssignedExamId] int NOT NULL,
    [QuestionId] int NOT NULL,
    CONSTRAINT [PK_StudentAnswers] PRIMARY KEY ([StudentAnswerId]),
    CONSTRAINT [FK_StudentAnswers_Questions_QuestionId] FOREIGN KEY ([QuestionId]) REFERENCES [Questions] ([QuestionId]) ON DELETE CASCADE,
    CONSTRAINT [FK_StudentAnswers_StudentAssignedExams_StudentAssignedExamId] FOREIGN KEY ([StudentAssignedExamId]) REFERENCES [StudentAssignedExams] ([StudentAssignedExamId]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_AssignedExamGroup_GroupsGroupId] ON [AssignedExamGroup] ([GroupsGroupId]);
GO

CREATE INDEX [IX_AssignedExams_ExamId] ON [AssignedExams] ([ExamId]);
GO

CREATE INDEX [IX_ExamQuestion_QuestionsQuestionId] ON [ExamQuestion] ([QuestionsQuestionId]);
GO

CREATE INDEX [IX_QuestionOptions_QuestionId] ON [QuestionOptions] ([QuestionId]);
GO

CREATE INDEX [IX_StudentAnswers_QuestionId] ON [StudentAnswers] ([QuestionId]);
GO

CREATE INDEX [IX_StudentAnswers_StudentAssignedExamId] ON [StudentAnswers] ([StudentAssignedExamId]);
GO

CREATE INDEX [IX_StudentAssignedExams_AssignedExamId] ON [StudentAssignedExams] ([AssignedExamId]);
GO

CREATE INDEX [IX_StudentAssignedExams_StudentId] ON [StudentAssignedExams] ([StudentId]);
GO

CREATE INDEX [IX_Students_GroupId] ON [Students] ([GroupId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20211112084153_InitialCreate', N'5.0.12');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[QuestionOptions]') AND [c].[name] = N'Option');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [QuestionOptions] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [QuestionOptions] ALTER COLUMN [Option] int NOT NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20211112133637_v2', N'5.0.12');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20211115075545_v3', N'5.0.12');
GO

COMMIT;
GO

