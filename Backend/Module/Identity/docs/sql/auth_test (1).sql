-- Fresh schema for MariaDB 11.7+ (UUID_v7). Run against an empty database.

CREATE DATABASE IF NOT EXISTS `ms_identity_test`
    CHARACTER SET utf8mb4
    COLLATE utf8mb4_unicode_ci;

USE `ms_identity_test`;

CREATE TABLE `account`
(
    `account_id`           UUID PRIMARY KEY      DEFAULT (UUID_v7()),
    `account_email`        VARCHAR(255) NOT NULL,
    `account_password`     VARCHAR(255) NOT NULL,
    `account_created_at`   TIMESTAMP             DEFAULT (NOW()),
    `account_updated_at`   TIMESTAMP             DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    `account_is_active`    BOOLEAN      NOT NULL DEFAULT TRUE,

    UNIQUE INDEX `idx_account_email` (`account_email`),
    INDEX `idx_account_is_active_account_id` (`account_is_active`, `account_id`)
) ENGINE = InnoDB;

CREATE TABLE `role`
(
    `role_id`          UUID PRIMARY KEY      DEFAULT (UUID_v7()),
    `role_code`        VARCHAR(50)  NOT NULL,
    `role_name`        VARCHAR(150) NOT NULL,
    `role_description` VARCHAR(300),
    `role_is_active`   BOOLEAN      NOT NULL DEFAULT TRUE,
    `role_created_at`  TIMESTAMP             DEFAULT (NOW()),
    `role_updated_at`  TIMESTAMP             DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,

    UNIQUE INDEX `idx_role_code` (`role_code`),
    UNIQUE INDEX `idx_role_name` (`role_name`)
) ENGINE = InnoDB;

CREATE TABLE `permission`
(
    `permission_id`          UUID PRIMARY KEY      DEFAULT (UUID_v7()),
    `permission_code`        VARCHAR(50)  NOT NULL,
    `permission_name`        VARCHAR(150) NOT NULL,
    `permission_description` VARCHAR(300),
    `permission_is_active`   BOOLEAN      NOT NULL DEFAULT TRUE,
    `permission_created_at`  TIMESTAMP             DEFAULT (NOW()),
    `permission_updated_at`  TIMESTAMP             DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,

    UNIQUE INDEX `idx_permission_code` (`permission_code`),
    UNIQUE INDEX `idx_permission_name` (`permission_name`)
) ENGINE = InnoDB;

CREATE TABLE `account_role`
(
    `account_id`  UUID NOT NULL,
    `role_id`     UUID NOT NULL,
    `assigned_at` TIMESTAMP DEFAULT (NOW()),

    PRIMARY KEY (`account_id`, `role_id`),
    INDEX `idx_account_role_role_id` (`role_id`),

    CONSTRAINT `fk_account_role_account`
        FOREIGN KEY (`account_id`) REFERENCES `account` (`account_id`),
    CONSTRAINT `fk_account_role_role`
        FOREIGN KEY (`role_id`) REFERENCES `role` (`role_id`)
) ENGINE = InnoDB;

CREATE TABLE `role_permission`
(
    `role_id`       UUID NOT NULL,
    `permission_id` UUID NOT NULL,
    `assigned_at`   TIMESTAMP DEFAULT (NOW()),

    PRIMARY KEY (`role_id`, `permission_id`),
    INDEX `idx_role_permission_permission_id` (`permission_id`),

    CONSTRAINT `fk_role_permission_role`
        FOREIGN KEY (`role_id`) REFERENCES `role` (`role_id`),
    CONSTRAINT `fk_role_permission_permission`
        FOREIGN KEY (`permission_id`) REFERENCES `permission` (`permission_id`)
) ENGINE = InnoDB;

CREATE TABLE `account_additional_permission`
(
    `account_id`    UUID NOT NULL,
    `permission_id` UUID NOT NULL,
    `assigned_at`   TIMESTAMP DEFAULT (NOW()),

    PRIMARY KEY (`account_id`, `permission_id`),
    INDEX `idx_account_additional_permission_permission_id` (`permission_id`),

    CONSTRAINT `fk_account_additional_permission_account`
        FOREIGN KEY (`account_id`) REFERENCES `account` (`account_id`),
    CONSTRAINT `fk_account_additional_permission_permission`
        FOREIGN KEY (`permission_id`) REFERENCES `permission` (`permission_id`)
) ENGINE = InnoDB;

CREATE TABLE `user_profile`
(
    `user_profile_id`             INT PRIMARY KEY AUTO_INCREMENT,
    `user_profile_account_id`     UUID NOT NULL,
    `user_profile_first_name`     VARCHAR(30),
    `user_profile_last_name`      VARCHAR(30),
    `user_profile_date_of_birth`  DATE,
    `user_profile_gender`         ENUM ('male', 'female', 'unspecified') NOT NULL DEFAULT 'unspecified',
    `user_profile_phone_number`   VARCHAR(10),
    `user_profile_avatar_url`     VARCHAR(255),
    `user_profile_background_url` VARCHAR(255),
    `user_profile_created_at`     TIMESTAMP DEFAULT (NOW()),
    `user_profile_updated_at`     TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,

    UNIQUE INDEX `idx_user_profile_account_id` (`user_profile_account_id`),
    INDEX `idx_user_profile_phone_number` (`user_profile_phone_number`),
    INDEX `idx_user_profile_date_of_birth` (`user_profile_date_of_birth`),

    CONSTRAINT `fk_user_profile_account`
        FOREIGN KEY (`user_profile_account_id`) REFERENCES `account` (`account_id`)
) ENGINE = InnoDB;

CREATE TABLE `location`
(
    `location_id`        UUID PRIMARY KEY DEFAULT (UUID_v7()),
    `location_address`   VARCHAR(255) NOT NULL,
    `location_created_at` TIMESTAMP DEFAULT (NOW()),
    `location_updated_at` TIMESTAMP DEFAULT CURRENT_TIMESTAMP
                          ON UPDATE CURRENT_TIMESTAMP,

    INDEX `idx_location_address`
        (`location_address`)
) ENGINE = InnoDB;

CREATE TABLE `premise`
(
    `premise_id`          UUID PRIMARY KEY DEFAULT (UUID_v7()),
    `premise_name`        VARCHAR(50) NOT NULL,
    `premise_location_id` UUID NOT NULL,

    `premise_status` ENUM (
        'rented',
        'available',
        'under_maintenance'
    ) NOT NULL DEFAULT 'available',

    `premise_position`     INT,
    `premise_floor`        INT,
    `premise_area`         VARCHAR(10),
    `premise_description`  VARCHAR(100),
    `premise_created_at`   TIMESTAMP DEFAULT (NOW()),
    `premise_updated_at`   TIMESTAMP DEFAULT CURRENT_TIMESTAMP
                           ON UPDATE CURRENT_TIMESTAMP,

    INDEX `idx_premise_location_id`
        (`premise_location_id`),

    INDEX `idx_premise_status`
        (`premise_status`),

    INDEX `idx_premise_floor`
        (`premise_floor`),

    INDEX `idx_premise_location_status`
        (`premise_location_id`, `premise_status`),

    CONSTRAINT `fk_premise_location`
        FOREIGN KEY (`premise_location_id`)
        REFERENCES `location` (`location_id`)
) ENGINE = InnoDB;

CREATE TABLE `business_type`
(
    `business_type_id`          UUID PRIMARY KEY DEFAULT (UUID_v7()),
    `business_type_name`        VARCHAR(50) NOT NULL,
    `business_type_description` VARCHAR(255),
    `business_type_is_active`   BOOLEAN NOT NULL DEFAULT TRUE,
    `business_type_created_at`  TIMESTAMP DEFAULT (NOW()),
    `business_type_updated_at`  TIMESTAMP DEFAULT CURRENT_TIMESTAMP
                                ON UPDATE CURRENT_TIMESTAMP,

    INDEX `idx_business_type_is_active`
        (`business_type_is_active`)
) ENGINE = InnoDB;

CREATE TABLE `premise_business_type`
(
    `premise_id`       UUID NOT NULL,
    `business_type_id` UUID NOT NULL,

    PRIMARY KEY (`premise_id`, `business_type_id`),

    INDEX `idx_premise_business_type_business_type_id`
        (`business_type_id`),

    CONSTRAINT `fk_premise_business_type_premise`
        FOREIGN KEY (`premise_id`)
        REFERENCES `premise` (`premise_id`),

    CONSTRAINT `fk_premise_business_type_business_type`
        FOREIGN KEY (`business_type_id`)
        REFERENCES `business_type` (`business_type_id`)
) ENGINE = InnoDB;

CREATE TABLE `whitelist_product`
(
    `product_id`          UUID PRIMARY KEY DEFAULT (UUID_v7()),
    `product_name`        VARCHAR(100) NOT NULL,
    `product_description` VARCHAR(255),
    `product_created_at`  TIMESTAMP DEFAULT (NOW()),
    `product_updated_at`  TIMESTAMP DEFAULT CURRENT_TIMESTAMP
                          ON UPDATE CURRENT_TIMESTAMP,

    INDEX `idx_whitelist_product_name`
        (`product_name`)
) ENGINE = InnoDB;

CREATE TABLE `product_business_type`
(
    `product_id`       UUID NOT NULL,
    `business_type_id` UUID NOT NULL,

    PRIMARY KEY (`product_id`, `business_type_id`),

    INDEX `idx_product_business_type_business_type_id`
        (`business_type_id`),

    CONSTRAINT `fk_product_business_type_product`
        FOREIGN KEY (`product_id`)
        REFERENCES `whitelist_product` (`product_id`),

    CONSTRAINT `fk_product_business_type_business_type`
        FOREIGN KEY (`business_type_id`)
        REFERENCES `business_type` (`business_type_id`)
) ENGINE = InnoDB;

CREATE TABLE `premise_media`
(
    `premise_media_id`         INT PRIMARY KEY AUTO_INCREMENT,
    `premise_media_image`      VARCHAR(255) NOT NULL,
    `premise_media_premise_id` UUID NOT NULL,

    INDEX `idx_media_premise_id`
        (`premise_media_premise_id`),

    CONSTRAINT `fk_premise_media_premise`
        FOREIGN KEY (`premise_media_premise_id`)
        REFERENCES `premise` (`premise_id`)
) ENGINE = InnoDB;

CREATE TABLE `contract`
(
    `contract_id`                  UUID PRIMARY KEY DEFAULT (UUID_v7()),
    `contract_account_id`          UUID NOT NULL,
    `contract_deposit`             DECIMAL(18,2),
    `contract_rental_price`        DECIMAL(18,2),
    `contract_premise_return_date` TIMESTAMP,

    `contract_status` ENUM (
        'pending_signature',
        'canceled',
        'expired',
        'signed',
        'terminated'
    ) NOT NULL DEFAULT 'pending_signature',

    `contract_termination_date`    TIMESTAMP,
    `contract_created_at`          TIMESTAMP DEFAULT (NOW()),
    `contract_updated_at`          TIMESTAMP DEFAULT CURRENT_TIMESTAMP
                                   ON UPDATE CURRENT_TIMESTAMP,

    INDEX `idx_contract_account_id`
        (`contract_account_id`),

    INDEX `idx_contract_status`
        (`contract_status`),

    INDEX `idx_contract_account_status`
        (`contract_account_id`, `contract_status`),

    CONSTRAINT `fk_contract_account`
        FOREIGN KEY (`contract_account_id`)
        REFERENCES `account` (`account_id`)
) ENGINE = InnoDB;

CREATE TABLE `rented_premise`
(
    `contract_id` UUID NOT NULL,
    `premise_id`  UUID NOT NULL,

    PRIMARY KEY (`contract_id`, `premise_id`),

    INDEX `idx_rented_premise_premise_id`
        (`premise_id`),

    CONSTRAINT `fk_rented_premise_contract`
        FOREIGN KEY (`contract_id`)
        REFERENCES `contract` (`contract_id`),

    CONSTRAINT `fk_rented_premise_premise`
        FOREIGN KEY (`premise_id`)
        REFERENCES `premise` (`premise_id`)
) ENGINE = InnoDB;

CREATE TABLE `regulation`
(
    `regulation_id`          UUID PRIMARY KEY DEFAULT (UUID_v7()),
    `regulation_name`        VARCHAR(255) NOT NULL,
    `regulation_description` VARCHAR(255),
    `regulation_created_at`  TIMESTAMP DEFAULT (NOW()),
    `regulation_updated_at`  TIMESTAMP DEFAULT CURRENT_TIMESTAMP
                             ON UPDATE CURRENT_TIMESTAMP,

    INDEX `idx_regulation_name`
        (`regulation_name`)
) ENGINE = InnoDB;

CREATE TABLE `contract_regulation`
(
    `regulation_id` UUID NOT NULL,
    `contract_id`   UUID NOT NULL,

    PRIMARY KEY (`regulation_id`, `contract_id`),

    INDEX `idx_contract_regulation_contract_id`
        (`contract_id`),

    CONSTRAINT `fk_contract_regulation_regulation`
        FOREIGN KEY (`regulation_id`)
        REFERENCES `regulation` (`regulation_id`),

    CONSTRAINT `fk_contract_regulation_contract`
        FOREIGN KEY (`contract_id`)
        REFERENCES `contract` (`contract_id`)
) ENGINE = InnoDB;

CREATE TABLE `contract_violation`
(
    `violation_id`                  UUID PRIMARY KEY DEFAULT (UUID_v7()),
    `violation_contract_id`          UUID NOT NULL,
    `violation_content`              VARCHAR(150) NOT NULL,
    `violation_compensation_amount`  DECIMAL(18,2),
    `violation_date`                 TIMESTAMP,

    `violation_status` ENUM (
        'pending',
        'resolved',
        'waiting_confirmation'
    ) NOT NULL DEFAULT 'waiting_confirmation',

    INDEX `idx_contract_violation_contract_id`
        (`violation_contract_id`),

    INDEX `idx_contract_violation_status`
        (`violation_status`),

    INDEX `idx_contract_violation_date`
        (`violation_date`),

    CONSTRAINT `fk_contract_violation_contract`
        FOREIGN KEY (`violation_contract_id`)
        REFERENCES `contract` (`contract_id`)
) ENGINE = InnoDB;

CREATE TABLE `monthly_invoice`
(
    `invoice_id`           UUID PRIMARY KEY DEFAULT (UUID_v7()),
    `invoice_contract_id`  UUID NOT NULL,
    `invoice_payment_date` TIMESTAMP,
    `invoice_created_at`   TIMESTAMP DEFAULT (NOW()),
    `invoice_updated_at`   TIMESTAMP DEFAULT CURRENT_TIMESTAMP
                           ON UPDATE CURRENT_TIMESTAMP,

    INDEX `idx_monthly_invoice_contract_id`
        (`invoice_contract_id`),

    INDEX `idx_monthly_invoice_payment_date`
        (`invoice_payment_date`),

    INDEX `idx_monthly_invoice_contract_payment`
        (`invoice_contract_id`, `invoice_payment_date`),

    CONSTRAINT `fk_monthly_invoice_contract`
        FOREIGN KEY (`invoice_contract_id`)
        REFERENCES `contract` (`contract_id`)
) ENGINE = InnoDB;

CREATE TABLE `invoice_detail`
(
    `invoice_detail_id`              INT PRIMARY KEY AUTO_INCREMENT,
    `invoice_detail_invoice_id`      UUID NOT NULL,
    `invoice_detail_premise_id`      UUID NOT NULL,
    `invoice_detail_rental_price`    DECIMAL(18,2),
    `invoice_detail_electricity_fee` DECIMAL(18,2),
    `invoice_detail_water_fee`       DECIMAL(18,2),
    `invoice_detail_garbage_fee`     DECIMAL(18,2),
    `invoice_detail_total_amount`    DECIMAL(18,2),

    INDEX `idx_invoice_detail_invoice_id`
        (`invoice_detail_invoice_id`),

    INDEX `idx_invoice_detail_premise_id`
        (`invoice_detail_premise_id`),

    CONSTRAINT `fk_invoice_detail_invoice`
        FOREIGN KEY (`invoice_detail_invoice_id`)
        REFERENCES `monthly_invoice` (`invoice_id`),

    CONSTRAINT `fk_invoice_detail_premise`
        FOREIGN KEY (`invoice_detail_premise_id`)
        REFERENCES `premise` (`premise_id`)
) ENGINE = InnoDB;

CREATE TABLE `ticket`
(
    `ticket_id`        UUID PRIMARY KEY DEFAULT (UUID_v7()),
    `ticket_account_id` UUID NOT NULL,
    `ticket_content`   VARCHAR(250) NOT NULL,

    `ticket_type` ENUM (
        'review',
        'complaint',
        'feedback'
    ) NOT NULL DEFAULT 'feedback',

    `ticket_status` ENUM (
        'received',
        'viewed',
        'resolved'
    ) NOT NULL DEFAULT 'received',

    `ticket_created_at` TIMESTAMP DEFAULT (NOW()),
    `ticket_updated_at` TIMESTAMP DEFAULT CURRENT_TIMESTAMP
                        ON UPDATE CURRENT_TIMESTAMP,

    INDEX `idx_ticket_account_id`
        (`ticket_account_id`),

    INDEX `idx_ticket_type`
        (`ticket_type`),

    INDEX `idx_ticket_status`
        (`ticket_status`),

    INDEX `idx_ticket_account_status`
        (`ticket_account_id`, `ticket_status`),

    INDEX `idx_ticket_created_at`
        (`ticket_created_at`),

    CONSTRAINT `fk_ticket_account`
        FOREIGN KEY (`ticket_account_id`)
        REFERENCES `account` (`account_id`)
) ENGINE = InnoDB;

CREATE TABLE `ticket_media`
(
    `ticket_media_id`        UUID PRIMARY KEY DEFAULT (UUID_v7()),
    `ticket_media_ticket_id` UUID NOT NULL,
    `ticket_media_image`     VARCHAR(255) NOT NULL,

    INDEX `idx_ticket_media_ticket_id`
        (`ticket_media_ticket_id`),

    CONSTRAINT `fk_ticket_media_ticket`
        FOREIGN KEY (`ticket_media_ticket_id`)
        REFERENCES `ticket` (`ticket_id`)
) ENGINE = InnoDB;

CREATE TABLE `notification`
(
    `notification_id` UUID PRIMARY KEY DEFAULT (UUID_v7()),

    `notification_sender_acount_id` UUID NOT NULL,

    `notification_type` ENUM (
        'violation',
        'ticket',
        'other'
    ) NOT NULL DEFAULT 'other',

    `notification_content` VARCHAR(255) NOT NULL,

    `notification_status` ENUM (
        'unread',
        'read'
    ) NOT NULL DEFAULT 'unread',

    `notification_created_at` TIMESTAMP DEFAULT (NOW()),
    `notification_updated_at` TIMESTAMP DEFAULT CURRENT_TIMESTAMP
                              ON UPDATE CURRENT_TIMESTAMP,

    INDEX `idx_notification_sender_account_id`
        (`notification_sender_acount_id`),

    INDEX `idx_notification_type`
        (`notification_type`),

    INDEX `idx_notification_status`
        (`notification_status`),

    INDEX `idx_notification_created_at`
        (`notification_created_at`),

    CONSTRAINT `fk_notification_sender_account`
        FOREIGN KEY (`notification_sender_acount_id`)
        REFERENCES `account` (`account_id`)
) ENGINE = InnoDB;

CREATE TABLE `notification_recipient`
(
    `notification_id` UUID NOT NULL,
    `account_id`      UUID NOT NULL,

    PRIMARY KEY (`notification_id`, `account_id`),

    INDEX `idx_notification_recipient_account_id`
        (`account_id`),

    CONSTRAINT `fk_notification_recipient_notification`
        FOREIGN KEY (`notification_id`)
        REFERENCES `notification` (`notification_id`),

    CONSTRAINT `fk_notification_recipient_account`
        FOREIGN KEY (`account_id`)
        REFERENCES `account` (`account_id`)
) ENGINE = InnoDB;

DELIMITER //

CREATE TRIGGER `trigger_permission_code_immutable`
    BEFORE UPDATE
    ON `permission`
    FOR EACH ROW
BEGIN
    IF NOT (OLD.`permission_code` <=> NEW.`permission_code`) THEN
        SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'Permission code cannot be changed';
    END IF;
END //

CREATE TRIGGER `trigger_role_code_immutable`
    BEFORE UPDATE
    ON `role`
    FOR EACH ROW
BEGIN
    IF NOT (OLD.`role_code` <=> NEW.`role_code`) THEN
        SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'Role code cannot be changed';
    END IF;
END //

DELIMITER ;
