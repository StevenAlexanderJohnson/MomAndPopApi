DELIMITER &&
use MomAndPop;
&&
DROP PROCEDURE IF EXISTS sp_Delete_User;
&&
CREATE PROCEDURE sp_Delete_User(
    IN ID BIGINT
)
BEGIN
    DELETE FROM User
    WHERE id = ID;
END
&&