CREATE TABLE [dbo].[Question]
(
	[Id] INT NOT NULL IDENTITY , 
    [Version] INT NOT NULL DEFAULT 1, 
	[Number] INT NOT NULL, 
    [Description] VARCHAR(50) NULL, 
    
    CONSTRAINT [PK_Question] PRIMARY KEY ([Id]),
	CONSTRAINT [UQ_Q_Number_Version] UNIQUE NONCLUSTERED ([Version], [Number]) ON [PRIMARY]
)

