CREATE TABLE [dbo].[QuestionItem]
(
	[AnswerId] INT NULL, 
    [QuestionId] INT NOT NULL, 
	[QuestionVersion] INT NOT NULL,
    [QuestionListId] INT NOT NULL, 
	
     
    CONSTRAINT [FK_QuestionItem_Answer] FOREIGN KEY ([AnswerId]) REFERENCES [Answer]([Id]), 
    CONSTRAINT [PK_QuestionItem] PRIMARY KEY ([QuestionId], [QuestionListId], [QuestionVersion]), 
    CONSTRAINT [FK_QuestionItem_Question] FOREIGN KEY ([QuestionId], [QuestionVersion]) REFERENCES [Question]([Id], [Version]),
	CONSTRAINT [FK_QuestionItem_QuestionList] FOREIGN KEY ([QuestionListId]) REFERENCES [QuestionList]([Id]) ON DELETE CASCADE 
)
