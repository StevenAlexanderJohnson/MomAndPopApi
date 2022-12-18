CREATE DEFINER=`root`@`%` PROCEDURE `sp_Check_Refresh_Token`(refreshToken VARCHAR(64), OUT foundUser VARCHAR(64))
BEGIN
	SELECT username into foundUser FROM PERSISTENT_TOKENS pt WHERE pt.refresh_token = refreshToken AND pt.expires > current_timestamp();
END