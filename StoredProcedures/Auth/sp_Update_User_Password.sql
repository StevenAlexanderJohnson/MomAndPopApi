CREATE PROCEDURE `sp_Update_User_Password` (usernameInput varchar(20), newPasswordInput varchar(64))
BEGIN
	UPDATE USER_CREDENTIALS uc SET uc.password_hash = newPasswordInput WHERE uc.username = usernameInput;
END
