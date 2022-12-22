DELIMITER %%

CREATE DATABASE MomAndPop;
%%

USE MomAndPop;
%%

CREATE TABLE `CONTACT` (
   `user_id` bigint DEFAULT NULL,
   `email` varchar(50) DEFAULT NULL,
   `phone_number` varchar(10) DEFAULT NULL,
   `email_verified` bit(1) DEFAULT NULL,
   `phone_verified` bit(1) DEFAULT NULL,
   KEY `user_id` (`user_id`),
   CONSTRAINT `CONTACT_ibfk_1` FOREIGN KEY (`user_id`) REFERENCES `USER` (`id`)
 ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci
%%

CREATE TABLE `POST` (
   `id` bigint NOT NULL AUTO_INCREMENT,
   `attachment` bit(1) NOT NULL,
   `title` varchar(50) NOT NULL,
   `description` varchar(250) NOT NULL,
   `user_id` bigint NOT NULL,
   PRIMARY KEY (`id`),
   KEY `POST_ibfk_1` (`user_id`),
   CONSTRAINT `POST_ibfk_1` FOREIGN KEY (`user_id`) REFERENCES `USER` (`id`) ON DELETE CASCADE
 ) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci
%%

CREATE TABLE `POST_IMAGES` (
   `post_id` bigint NOT NULL,
   `image` mediumblob NOT NULL,
   `image_type` varchar(15) NOT NULL,
   KEY `id_idx` (`post_id`),
   CONSTRAINT `id` FOREIGN KEY (`post_id`) REFERENCES `POST` (`id`) ON DELETE CASCADE
 ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci
%%

CREATE TABLE `STORE` (
   `id` bigint NOT NULL AUTO_INCREMENT,
   `owner_id` bigint NOT NULL,
   `store_name` varchar(50) NOT NULL,
   `description` varchar(250) NOT NULL,
   `address1` varchar(80) NOT NULL,
   `address2` varchar(80) DEFAULT NULL,
   `city` varchar(50) NOT NULL,
   `state` varchar(50) NOT NULL,
   `zip` varchar(15) NOT NULL,
   `verified` bit(1) NOT NULL,
   PRIMARY KEY (`id`),
   KEY `owner_id` (`owner_id`),
   CONSTRAINT `STORE_ibfk_1` FOREIGN KEY (`owner_id`) REFERENCES `USER` (`id`)
 ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci
%%

CREATE TABLE `USER` (
   `id` bigint NOT NULL AUTO_INCREMENT,
   `username` varchar(20) NOT NULL,
   `first_name` varchar(20) NOT NULL,
   `last_name` varchar(20) NOT NULL,
   `address1` varchar(80) NOT NULL,
   `address2` varchar(80) DEFAULT NULL,
   `city` varchar(50) NOT NULL,
   `state` varchar(50) NOT NULL,
   `zip` varchar(15) NOT NULL,
   `verified` bit(1) NOT NULL,
   PRIMARY KEY (`id`),
   UNIQUE KEY `username_UNIQUE` (`username`)
 ) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci
%%

CREATE TABLE `USER_EMAIL` (
   `user_id` bigint NOT NULL,
   `email` varchar(30) NOT NULL,
   `verified` bit(1) NOT NULL,
   KEY `user_id` (`user_id`),
   CONSTRAINT `USER_EMAIL_ibfk_1` FOREIGN KEY (`user_id`) REFERENCES `USER` (`id`) ON DELETE CASCADE
 ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci
%%
