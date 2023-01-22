CREATE DEFINER=`root`@`%` PROCEDURE `sp_Get_Post_By_User`(Username varchar(20), page_offset int)
BEGIN
    SELECT p.*, u.username 
    FROM MomAndPop.POST p 
	JOIN MomAndPop.USER u on u.id = p.user_id
	WHERE u.username = Username
    ORDER BY p.id
    LIMIT 10 OFFSET page_offset;
END