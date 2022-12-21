CREATE DEFINER=`root`@`%` PROCEDURE `sp_Expire_Refresh_Token`(refreshToken VARCHAR(64), out success bit)
BEGIN
	UPDATE PERSISTENT_TOKENS SET expires = NOW() where refresh_token = refreshToken;
    
END