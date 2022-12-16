CREATE DEFINER=`root`@`%` PROCEDURE `sp_Add_User_Credentials`(usernameInput varchar(20), passwordInput varchar(64))
BEGIN
	INSERT INTO USER_CREDENTIALS(username, password_hash) VALUES (usernameInput, passwordInput);
END