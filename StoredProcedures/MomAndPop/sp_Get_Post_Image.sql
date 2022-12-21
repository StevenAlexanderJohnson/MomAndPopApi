CREATE DEFINER=`root`@`%` PROCEDURE `sp_Get_Post_Image`(postId int)
BEGIN
	SELECT pi.image, pi.image_type
    FROM POST_IMAGES pi 
    INNER JOIN POST p on pi.post_id = p.id 
    WHERE p.id = postId;
END