CREATE DEFINER=`root`@`%` PROCEDURE `sp_Authorize_User`(usernameInput varchar(20), passwordHashInput varchar(64))
BEGIN
	SELECT EXISTS(
		SELECT 1
        FROM USER_CREDENTIALS uc
        WHERE LOWER(uc.username) = LOWER(usernameInput)
			AND uc.password_hash = passwordHashInput
	) AS result;
END