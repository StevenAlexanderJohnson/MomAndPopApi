CREATE DEFINER=`root`@`%` PROCEDURE `sp_Get_Post_By_Id`(postId bigint)
BEGIN
	SELECT p.*, pi.image FROM POST p 
    LEFT JOIN POST_IMAGES pi on pi.post_id = p.id
    WHERE p.id = postId;
END