CREATE PROCEDURE `sp_Update_User_Username` (usernameInput varchar(64), newUsernameInput varchar(64))
BEGIN
	UPDATE USER_CREDENTIALS uc SET uc.username = newUsernameInput WHERE uc.username = usernameInput;
END
