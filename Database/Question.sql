CREATE TABLE [dbo].[Question]
(
	[Id] INT NOT NULL IDENTITY , 
    [Version] INT NOT NULL , 
    [Description] VARCHAR(MAX) NOT NULL, 
    
    [IsActive] BIT NOT NULL DEFAULT 1, 
    CONSTRAINT [PK_Question]  PRIMARY KEY CLUSTERED ([Id], [Version])
)

