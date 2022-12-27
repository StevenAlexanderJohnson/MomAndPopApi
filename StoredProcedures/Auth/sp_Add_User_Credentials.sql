CREATE DEFINER=`root`@`%` PROCEDURE `sp_Add_User_Credentials`(usernameInput varchar(20), passwordInput varchar(64), refreshToken varchar(64))
BEGIN
	IF EXISTS(SELECT 1 FROM USER_CREDENTIALS uc WHERE uc.username = usernameInput) THEN
		SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'User credentials already exists.';
    END IF;
	INSERT INTO USER_CREDENTIALS(username, password_hash) VALUES (usernameInput, passwordInput);
    INSERT INTO PERSISTENT_TOKENS(username, refresh_token, expires) VALUES (usernameInput, refreshToken, NOW() + INTERVAL 30 MINUTE);
END