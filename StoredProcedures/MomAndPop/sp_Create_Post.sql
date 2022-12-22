CREATE DEFINER=`root`@`%` PROCEDURE `sp_Create_Post`(Image MEDIUMBLOB, Image_Type varchar(20), Title VARCHAR(50), `Description` VARCHAR(250), UserID BIGINT)
BEGIN
	INSERT INTO POST (attachment, title, `description`, user_id)
    VALUES (Image is not null, Title, `Description`, UserID);
    IF Image is not null THEN
		INSERT INTO POST_IMAGES (post_id, image, image_type)
        VALUES(LAST_INSERT_ID(), Image, Image_Type);
    END IF;
END