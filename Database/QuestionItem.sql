CREATE TABLE [dbo].[QuestionItem]
(
	[Id] INT NOT NULL IDENTITY, 
	[AnswerId] INT NULL , 
    [QuestionId] INT NOT NULL, 
    [QuestionListId] INT NOT NULL, 
	
    CONSTRAINT [FK_QuestionItem_Answer] FOREIGN KEY ([AnswerId]) REFERENCES [Answer]([Id]), 
    CONSTRAINT [PK_QuestionItem] PRIMARY KEY ([Id]), 
    CONSTRAINT [FK_QuestionItem_Question] FOREIGN KEY ([QuestionId]) REFERENCES [Question]([Id]) ON DELETE CASCADE,
	CONSTRAINT [FK_QuestionItem_QuestionList] FOREIGN KEY ([QuestionListId]) REFERENCES [QuestionList]([Id]) ON DELETE CASCADE 
    
)

GO

CREATE UNIQUE INDEX [IX_QuestionItem_AnswerId] ON [dbo].[QuestionItem] ([AnswerId])
