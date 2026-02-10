CREATE PROCEDURE [dbo].[spPostInsert]
    @UserId INT,
    @Title NVARCHAR(150),
    @Body NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO dbo.Posts (UserId, Title, Body, DateCreated)
    VALUES (@UserId, @Title, @Body, GETDATE());
END
