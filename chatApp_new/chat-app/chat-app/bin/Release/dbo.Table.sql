CREATE TABLE [dbo].[Login]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [username] NCHAR(10) NULL, 
    [password] NCHAR(10) NULL, 
    [ip] NCHAR(10) NULL, 
    [port] NCHAR(10) NULL
)
