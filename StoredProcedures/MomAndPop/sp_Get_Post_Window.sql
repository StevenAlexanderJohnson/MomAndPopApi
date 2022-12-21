CREATE DEFINER=`root`@`%` PROCEDURE `sp_Get_Post_Window`(page_offset int)
BEGIN
	SELECT p.* FROM POST p 
    ORDER BY p.id
    LIMIT 10 OFFSET page_offset;
END