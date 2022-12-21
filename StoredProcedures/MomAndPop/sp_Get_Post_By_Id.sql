CREATE DEFINER=`root`@`%` PROCEDURE `sp_Get_Post_By_Id`(postId bigint)
BEGIN
	SELECT p.* FROM POST p
    WHERE p.id = postId;
END