DELIMITER %%
CREATE DATABASE `Auth`;
%%

CREATE TABLE `PERSISTENT_TOKENS` (
   `username` varchar(20) NOT NULL,
   `refresh_token` varchar(64) NOT NULL,
   `expires` datetime NOT NULL,
   PRIMARY KEY (`username`),
   CONSTRAINT `PERSISTENT_TOKENS_ibfk_1` FOREIGN KEY (`username`) REFERENCES `USER_CREDENTIALS` (`username`) ON DELETE CASCADE
 ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

%%

CREATE TABLE `USER_CREDENTIALS` (
   `username` varchar(20) NOT NULL,
   `password_hash` varchar(64) DEFAULT NULL,
   PRIMARY KEY (`username`),
   KEY `username` (`username`)
 ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
%%