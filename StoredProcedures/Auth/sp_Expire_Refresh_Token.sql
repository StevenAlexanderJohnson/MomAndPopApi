CREATE DEFINER=`root`@`%` PROCEDURE `sp_Expire_Refresh_Token`(refreshToken VARCHAR(64))
BEGIN
	UPDATE PERSISTENT_TOKENS SET expires = NOW() where refresh_token = refreshToken;
END