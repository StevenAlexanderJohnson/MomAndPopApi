CREATE DEFINER=`root`@`%` PROCEDURE `sp_Get_Post_By_Id`(postId bigint)
BEGIN
	SELECT p.*, u.username FROM POST p
    JOIN USER u on u.id = p.user_id
    WHERE p.id = postId;
END