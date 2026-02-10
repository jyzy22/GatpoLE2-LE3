CREATE PROCEDURE [dbo].[spUserLogin]
    @UserName NVARCHAR(16),
    @Password NVARCHAR(16)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT Id, UserName, FirstName, LastName
    FROM dbo.Users
    WHERE UserName = @UserName
      AND Password = @Password;
END
