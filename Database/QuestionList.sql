CREATE TABLE [dbo].[QuestionList]
(
	[Id] INT NOT NULL IDENTITY , 
    [Version] INT NOT NULL DEFAULT 1,
	[Number] INT NOT NULL DEFAULT 1, 
    [Description] VARCHAR(50) NULL, 
    [IsTemplate] BIT NOT NULL DEFAULT 0, 
    CONSTRAINT [PK_QuestionList] PRIMARY KEY ([Id]),
	CONSTRAINT [UQ_QL_Number_Version] UNIQUE NONCLUSTERED ([Version], [Number]) ON [PRIMARY]
)
