CREATE PROCEDURE [dbo].[spUsers_Authenticate]
    @UserName NVARCHAR(16),
    @Password NVARCHAR(16)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT Id, UserName, FirstName, LastName, Password
    FROM dbo.Users
    WHERE UserName = @UserName AND Password = @Password;
END
