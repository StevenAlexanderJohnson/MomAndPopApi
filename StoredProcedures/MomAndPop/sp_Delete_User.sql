CREATE DEFINER=`root`@`%` PROCEDURE `sp_Delete_User`(
    IN ID BIGINT
)
BEGIN
    DELETE FROM USER u
    WHERE u.id = ID;
END