CREATE PROCEDURE [dbo].[spUsers_Register]
    @UserName NVARCHAR(16),
    @FirstName NVARCHAR(50),
    @LastName NVARCHAR(50),
    @Password NVARCHAR(16)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO dbo.Users (UserName, FirstName, LastName, Password)
    VALUES (@UserName, @FirstName, @LastName, @Password);
END
