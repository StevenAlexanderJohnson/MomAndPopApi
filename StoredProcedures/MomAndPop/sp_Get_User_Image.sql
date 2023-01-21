CREATE DEFINER=`root`@`%` PROCEDURE `sp_Get_User_Image`(userId bigint)
BEGIN
	SELECT image, image_type FROM USER_IMAGES ui WHERE ui.user_id = userId;
END