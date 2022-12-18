CREATE DEFINER=`root`@`%` PROCEDURE `sp_Authorize_User`(usernameInput varchar(20), passwordHashInput varchar(64), OUT result bit)
BEGIN
	SELECT EXISTS(SELECT * FROM USER_CREDENTIALS uc WHERE uc.username = usernameInput AND uc.password_hash = passwordHashInput) into result;
END