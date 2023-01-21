CREATE DEFINER=`root`@`%` PROCEDURE `sp_Select_User_By_Id`(userId bigint)
BEGIN
	SELECT * FROM USER u
    WHERE u.id = userId;
END