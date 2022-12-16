CREATE DEFINER=`root`@`%` PROCEDURE `sp_Delete_User_Credentials`(usernameInput varchar(20), passwordInput varchar(64))
BEGIN
	DELETE FROM USER_CREDENTIALS uc WHERE uc.username = usernameInput AND uc.password_hash = passwordInput;
END