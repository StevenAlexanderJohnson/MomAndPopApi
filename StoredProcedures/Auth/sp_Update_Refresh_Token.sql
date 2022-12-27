CREATE DEFINER=`root`@`%` PROCEDURE `sp_Update_Refresh_Token`(usernameInput VARCHAR(64), newRefreshToken VARCHAR(64))
BEGIN
	UPDATE PERSISTENT_TOKENS pt 
    SET pt.refresh_token = newRefreshToken, 
		pt.expires = NOW() + INTERVAL 30 MINUTE
	WHERE pt.username = usernameInput;
END