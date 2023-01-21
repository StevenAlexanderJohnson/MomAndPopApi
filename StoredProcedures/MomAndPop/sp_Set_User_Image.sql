CREATE DEFINER=`root`@`%` PROCEDURE `sp_Set_User_Image`(UserId bigint, Image mediumblob, ImageType varchar(10))
BEGIN
	INSERT INTO USER_IMAGES (user_id, image, image_type) VALUES (UserId, Image, ImageType);
END