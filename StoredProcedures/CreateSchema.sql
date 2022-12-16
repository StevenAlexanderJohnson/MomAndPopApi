CREATE DATABASE MomAndPop;

USE MomAndPop;

CREATE TABLE USER (
    id BIGINT AUTO_INCREMENT PRIMARY KEY,
    first_name VARCHAR(20) NOT NULL,
    last_name VARCHAR(20) NOT NULL,
    address1 VARCHAR(80) NOT NULL,
    address2 VARCHAR(80),
    city VARCHAR(50) NOT NULL,
    `state` VARCHAR(50) NOT NULL,
    zip VARCHAR(15) NOT NULL,
    verified BIT NOT NULL -- Verified store owner
);

CREATE TABLE CONTACT (
	user_id BIGINT,
    email varchar(50),
    phone_number varchar(10),
    email_verified bit,
    phone_verified bit,
    FOREIGN KEY (user_id)
		REFERENCES USER(id)
);

CREATE TABLE USER_EMAIL (
    user_id BIGINT NOT NULL,
    email VARCHAR(30) NOT NULL,
    verified BIT NOT NULL, -- Email has been verified as real
    FOREIGN KEY (user_id)
        REFERENCES USER(id)
        ON DELETE CASCADE
);

CREATE TABLE STORE (
    id BIGINT AUTO_INCREMENT,
    owner_id BIGINT NOT NULL,
    store_name VARCHAR(50) NOT NULL,
    description VARCHAR(250) NOT NULL,
    address1 VARCHAR(80) NOT NULL,
    address2 VARCHAR(80),
    city VARCHAR(50) NOT NULL,
    state VARCHAR(50) NOT NULL,
    zip VARCHAR(15) NOT NULL,
    verified BIT NOT NULL, -- Verified real store
    PRIMARY KEY (id),
    FOREIGN KEY (owner_id)
        REFERENCES USER(id)
);

-- POST DO NOT GET DELETED ON USER ACCOUNT DELETION
-- KEPT TO LOOK UP AFTER DELETION
CREATE TABLE POST (
    id BIGINT AUTO_INCREMENT,
    attachment BIT NOT NULL,
    attachment_location VARCHAR(50) NOT NULL,
    description VARCHAR(250) NOT NULL,
    user_id BIGINT NOT NULL,
    PRIMARY KEY (id),
    FOREIGN KEY (user_id)
        REFERENCES USER(id)
);

CREATE TABLE `MomAndPop`.`USER_CREDENTIALS` (
  `username` VARCHAR(50) NOT NULL,
  `password_hash` VARCHAR(32) NOT NULL,
  PRIMARY KEY (`username`),
  INDEX `username` (`username` ASC));