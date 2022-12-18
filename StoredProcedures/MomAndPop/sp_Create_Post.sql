CREATE DEFINER=`root`@`%` PROCEDURE `sp_Create_Post`(Image BLOB, Title VARCHAR(50), `Description` VARCHAR(250), UserID BIGINT)
BEGIN
	INSERT INTO POST (attachment, title, `description`, user_id)
    VALUES (Image is not null, Title, `Description`, UserID);
    IF Image is not null THEN
		INSERT INTO POST_IMAGES (post_id, image)
        VALUES(LAST_INSERT_ID(), Image);
    END IF;
END