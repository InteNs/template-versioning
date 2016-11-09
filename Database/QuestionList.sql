CREATE TABLE [dbo].[QuestionList]
(
	[Id] INT NOT NULL IDENTITY , 
    [Description] VARCHAR(MAX) NOT NULL, 
    [IsTemplate] BIT NOT NULL , 
    CONSTRAINT [PK_QuestionList] PRIMARY KEY ([Id])
)
