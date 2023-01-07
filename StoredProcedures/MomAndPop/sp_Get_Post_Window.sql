CREATE DEFINER=`root`@`%` PROCEDURE `sp_Get_Post_Window`(page_offset int)
BEGIN
	SELECT p.*, u.username FROM POST p 
    JOIN USER u on u.id = p.user_id
    ORDER BY p.id
    LIMIT 10 OFFSET page_offset;
END