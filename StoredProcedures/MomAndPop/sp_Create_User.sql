CREATE DEFINER=`root`@`%` PROCEDURE `sp_Create_User`(
    IN Username varchar(20),
    IN FirstName varchar(20),
    IN LastName varchar(20),
    IN Address1 varchar(80),
    IN Address2 varchar(80),
    IN City varchar(50),
    IN State varchar(50),
    IN Zip varchar(15),
    IN Verified BIT
)
BEGIN
    INSERT INTO USER
           (username
           ,first_name
           ,last_name
           ,address1
           ,address2
           ,city
           ,state
           ,zip
           ,verified)
     VALUES
           (Username
           ,FirstName
           ,LastName
           ,Address1
           ,Address2
           ,City
           ,State
           ,Zip
           ,Verified);
END