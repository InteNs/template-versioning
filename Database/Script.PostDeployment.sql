/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

delete from QuestionItem;
delete from Answer;
delete from QuestionList;
delete from Question;

insert into Question (Id, Version, Description, IsActive) VALUES (1, 1, 'Hoeveel autos stonden er?', 1);
insert into Question (Id, Version, Description, IsActive) VALUES (1, 2, 'Hoeveel Autos staan geparkeerd?', 1);
insert into Question (Id, Version, Description, IsActive) VALUES (1, 3, 'Hoeveel autos staan geparkeerd?', 1);
insert into Question (Id, Version, Description, IsActive) VALUES (2, 1, 'Hoeveel autos staan fout geparkeerd?', 1);
insert into Question (Id, Version, Description, IsActive) VALUES (3, 1, 'Is de garage netjes?', 1);

insert into QuestionList (IsTemplate, Description) VALUES (1, 'tel lijst');