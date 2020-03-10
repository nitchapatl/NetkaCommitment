-- --------------------------------------------------------
-- Host:                         127.0.0.1
-- Server version:               8.0.19 - MySQL Community Server - GPL
-- Server OS:                    Win64
-- HeidiSQL Version:             10.3.0.5771
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;


-- Dumping database structure for netkacommitment
DROP DATABASE IF EXISTS `netkacommitment`;
CREATE DATABASE IF NOT EXISTS `netkacommitment` /*!40100 DEFAULT CHARACTER SET utf8 */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `netkacommitment`;

-- Dumping structure for table netkacommitment.m_company_lm
DROP TABLE IF EXISTS `m_company_lm`;
CREATE TABLE IF NOT EXISTS `m_company_lm` (
  `COMPANY_LM_ID` int unsigned NOT NULL AUTO_INCREMENT,
  `COMPANY_LM_NAME` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `COMPANY_LM_DESCRIPTION` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `COMPANY_LM_VALUE` tinyint unsigned NOT NULL DEFAULT '0',
  `COMPANY_LM_SEQUENCE` int unsigned NOT NULL,
  `CREATED_DATE` datetime NOT NULL,
  `CREATED_BY` int unsigned NOT NULL,
  `UPDATED_DATE` datetime DEFAULT NULL,
  `UPDATED_BY` int unsigned DEFAULT NULL,
  `IS_DELETED` bit(1) NOT NULL,
  `COMPANY_WIG_ID` int unsigned DEFAULT NULL,
  PRIMARY KEY (`COMPANY_LM_ID`),
  KEY `FK_m_company_lm_m_company_wig` (`COMPANY_WIG_ID`),
  CONSTRAINT `FK_m_company_lm_m_company_wig` FOREIGN KEY (`COMPANY_WIG_ID`) REFERENCES `m_company_wig` (`COMPANY_WIG_ID`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;

-- Dumping data for table netkacommitment.m_company_lm: ~3 rows (approximately)
DELETE FROM `m_company_lm`;
/*!40000 ALTER TABLE `m_company_lm` DISABLE KEYS */;
INSERT INTO `m_company_lm` (`COMPANY_LM_ID`, `COMPANY_LM_NAME`, `COMPANY_LM_DESCRIPTION`, `COMPANY_LM_VALUE`, `COMPANY_LM_SEQUENCE`, `CREATED_DATE`, `CREATED_BY`, `UPDATED_DATE`, `UPDATED_BY`, `IS_DELETED`, `COMPANY_WIG_ID`) VALUES
	(1, 'Increase total revenue from 1xxM Baht to 380M Baht by Dec 2020', NULL, 0, 1, '2020-03-05 14:15:49', 1, '2020-03-05 14:15:50', 1, b'0', 1),
	(2, 'Increase NPA revenue from 0 to 60M Baht by Dec 2020', NULL, 0, 2, '2020-03-05 14:16:20', 1, '2020-03-05 14:16:25', 1, b'0', 1),
	(3, 'Increase Process Compliance Index (PCI) of all projects and all department\'s processes from x% to 95% by Dec 2020', NULL, 0, 3, '2020-03-05 14:16:53', 1, '2020-03-05 14:16:54', 1, b'0', 1);
/*!40000 ALTER TABLE `m_company_lm` ENABLE KEYS */;

-- Dumping structure for table netkacommitment.m_company_wig
DROP TABLE IF EXISTS `m_company_wig`;
CREATE TABLE IF NOT EXISTS `m_company_wig` (
  `COMPANY_WIG_ID` int unsigned NOT NULL AUTO_INCREMENT,
  `COMPANY_WIG_NAME` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `COMPANY_WIG_YEAR` smallint unsigned NOT NULL,
  `COMPANY_WIG_DESCRIPTION` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `COMPANY_WIG_SEQUENCE` int unsigned NOT NULL,
  `CREATED_DATE` datetime NOT NULL,
  `CREATED_BY` int unsigned NOT NULL,
  `UPDATED_DATE` datetime DEFAULT NULL,
  `UPDATED_BY` int unsigned DEFAULT NULL,
  `IS_DELETED` bit(1) NOT NULL,
  PRIMARY KEY (`COMPANY_WIG_ID`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

-- Dumping data for table netkacommitment.m_company_wig: ~0 rows (approximately)
DELETE FROM `m_company_wig`;
/*!40000 ALTER TABLE `m_company_wig` DISABLE KEYS */;
INSERT INTO `m_company_wig` (`COMPANY_WIG_ID`, `COMPANY_WIG_NAME`, `COMPANY_WIG_YEAR`, `COMPANY_WIG_DESCRIPTION`, `COMPANY_WIG_SEQUENCE`, `CREATED_DATE`, `CREATED_BY`, `UPDATED_DATE`, `UPDATED_BY`, `IS_DELETED`) VALUES
	(1, 'Increase Net Income (NI) from xxM Baht to 180M Baht by Dec 2020', 2020, '', 1, '2020-03-05 13:55:26', 1, '2020-03-05 13:55:28', 1, b'0');
/*!40000 ALTER TABLE `m_company_wig` ENABLE KEYS */;

-- Dumping structure for table netkacommitment.m_department
DROP TABLE IF EXISTS `m_department`;
CREATE TABLE IF NOT EXISTS `m_department` (
  `DEPARTMENT_ID` int unsigned NOT NULL AUTO_INCREMENT,
  `DEPARTMENT_NAME` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `DEPARTMENT_DESCRIPTION` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `DEPARTMENT_SEQUENCE` int unsigned NOT NULL,
  `CREATED_DATE` datetime NOT NULL,
  `CREATED_BY` int unsigned NOT NULL,
  `UPDATED_DATE` datetime DEFAULT NULL,
  `UPDATED_BY` int unsigned DEFAULT NULL,
  `IS_DELETED` bit(1) NOT NULL,
  PRIMARY KEY (`DEPARTMENT_ID`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8;

-- Dumping data for table netkacommitment.m_department: ~7 rows (approximately)
DELETE FROM `m_department`;
/*!40000 ALTER TABLE `m_department` DISABLE KEYS */;
INSERT INTO `m_department` (`DEPARTMENT_ID`, `DEPARTMENT_NAME`, `DEPARTMENT_DESCRIPTION`, `DEPARTMENT_SEQUENCE`, `CREATED_DATE`, `CREATED_BY`, `UPDATED_DATE`, `UPDATED_BY`, `IS_DELETED`) VALUES
	(1, 'Accounting', NULL, 1, '2020-03-05 14:20:45', 1, '2020-03-05 14:20:46', 1, b'0'),
	(2, 'HR', NULL, 2, '2020-03-05 14:20:45', 1, '2020-03-05 14:20:46', 1, b'0'),
	(3, 'Marketing', NULL, 3, '2020-03-05 14:20:45', 1, '2020-03-05 14:20:46', 1, b'0'),
	(4, 'PS', NULL, 4, '2020-03-05 14:20:45', 1, '2020-03-05 14:20:46', 1, b'0'),
	(5, 'R&D', NULL, 5, '2020-03-05 14:20:45', 1, '2020-03-05 14:20:46', 1, b'0'),
	(6, 'Sale', NULL, 6, '2020-03-05 14:20:45', 1, '2020-03-05 14:20:46', 1, b'0'),
	(7, 'SP&IT', NULL, 7, '2020-03-05 14:20:45', 1, '2020-03-05 14:20:46', 1, b'0');
/*!40000 ALTER TABLE `m_department` ENABLE KEYS */;

-- Dumping structure for table netkacommitment.m_department_lm
DROP TABLE IF EXISTS `m_department_lm`;
CREATE TABLE IF NOT EXISTS `m_department_lm` (
  `LM_ID` int unsigned NOT NULL AUTO_INCREMENT,
  `LM_NAME` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `LM_DESCRIPTION` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `LM_SEQUENCE` int unsigned NOT NULL,
  `CREATED_DATE` datetime NOT NULL,
  `CREATED_BY` int unsigned NOT NULL,
  `UPDATED_DATE` datetime DEFAULT NULL,
  `UPDATED_BY` int unsigned DEFAULT NULL,
  `IS_DELETED` bit(1) NOT NULL,
  `DEPARTMENT_WIG_ID` int unsigned DEFAULT NULL,
  PRIMARY KEY (`LM_ID`),
  KEY `FK_m_lm_m_wig` (`DEPARTMENT_WIG_ID`),
  CONSTRAINT `FK_m_department_lm_m_department_wig` FOREIGN KEY (`DEPARTMENT_WIG_ID`) REFERENCES `m_department_wig` (`DEPARTMENT_WIG_ID`)
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=utf8;

-- Dumping data for table netkacommitment.m_department_lm: ~11 rows (approximately)
DELETE FROM `m_department_lm`;
/*!40000 ALTER TABLE `m_department_lm` DISABLE KEYS */;
INSERT INTO `m_department_lm` (`LM_ID`, `LM_NAME`, `LM_DESCRIPTION`, `LM_SEQUENCE`, `CREATED_DATE`, `CREATED_BY`, `UPDATED_DATE`, `UPDATED_BY`, `IS_DELETED`, `DEPARTMENT_WIG_ID`) VALUES
	(1, 'Develop feature 4 tasks/week', NULL, 1, '2020-03-05 14:28:25', 1, '2020-03-05 14:28:26', 1, b'0', 1),
	(2, 'Create product development training in LMS 2 topics/month', NULL, 2, '2020-03-05 14:28:25', 1, '2020-03-05 14:28:26', 1, b'0', 1),
	(3, 'Develop feature 4 tasks/week', NULL, 3, '2020-03-05 14:28:25', 1, '2020-03-05 14:28:26', 1, b'0', 2),
	(4, 'Tracking progress to maintain milestone sd <= 20% weekly', NULL, 4, '2020-03-05 14:28:25', 1, '2020-03-05 14:28:26', 1, b'0', 2),
	(5, 'Develop feature 4 tasks/week', NULL, 5, '2020-03-05 14:28:25', 1, '2020-03-05 14:28:26', 1, b'0', 3),
	(6, 'Tracking progress to maintain milestone sd <= 20% weekly', NULL, 6, '2020-03-05 14:28:25', 1, '2020-03-05 14:28:26', 1, b'0', 3),
	(7, 'Plan and deliver works by using agile from 0 to 2 projects', NULL, 7, '2020-03-05 14:28:25', 1, '2020-03-05 14:28:26', 1, b'0', 4),
	(8, 'Conduct R&D process checklist and review every week', NULL, 8, '2020-03-05 14:28:25', 1, '2020-03-05 14:28:26', 1, b'0', 4),
	(9, 'Do SCM comply statement 1 criteria per week', NULL, 9, '2020-03-05 14:28:25', 1, '2020-03-05 14:28:26', 1, b'0', 5),
	(10, 'Write SCM manual 1 criteria per week', NULL, 10, '2020-03-05 14:28:25', 1, '2020-03-05 14:28:26', 1, b'0', 5),
	(11, 'Increase number of activity that drive company goals from 0 to 5 every week', NULL, 11, '2020-03-05 14:28:25', 1, '2020-03-05 14:28:26', 1, b'0', 6);
/*!40000 ALTER TABLE `m_department_lm` ENABLE KEYS */;

-- Dumping structure for table netkacommitment.m_department_wig
DROP TABLE IF EXISTS `m_department_wig`;
CREATE TABLE IF NOT EXISTS `m_department_wig` (
  `DEPARTMENT_WIG_ID` int unsigned NOT NULL AUTO_INCREMENT,
  `DEPARTMENT_WIG_NAME` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `DEPARTMENT_WIG_DESCRIPTION` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `DEPARTMENT_WIG_SEQUENCE` int unsigned NOT NULL,
  `CREATED_DATE` datetime NOT NULL,
  `CREATED_BY` int unsigned NOT NULL,
  `UPDATED_DATE` datetime DEFAULT NULL,
  `UPDATED_BY` int unsigned DEFAULT NULL,
  `IS_DELETED` bit(1) NOT NULL,
  `DEPARTMENT_ID` int unsigned DEFAULT NULL,
  `COMPANY_WIG_ID` int unsigned DEFAULT NULL,
  `COMPANY_LM_ID` int unsigned DEFAULT NULL,
  PRIMARY KEY (`DEPARTMENT_WIG_ID`),
  KEY `FK_m_department_wig_m_department` (`DEPARTMENT_ID`),
  KEY `FK_m_department_wig_m_company_wig` (`COMPANY_WIG_ID`),
  KEY `FK_m_department_wig_m_company_lm` (`COMPANY_LM_ID`),
  CONSTRAINT `FK_m_department_wig_m_company_lm` FOREIGN KEY (`COMPANY_LM_ID`) REFERENCES `m_company_lm` (`COMPANY_LM_ID`),
  CONSTRAINT `FK_m_department_wig_m_company_wig` FOREIGN KEY (`COMPANY_WIG_ID`) REFERENCES `m_company_wig` (`COMPANY_WIG_ID`),
  CONSTRAINT `FK_m_department_wig_m_department` FOREIGN KEY (`DEPARTMENT_ID`) REFERENCES `m_department` (`DEPARTMENT_ID`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8;

-- Dumping data for table netkacommitment.m_department_wig: ~6 rows (approximately)
DELETE FROM `m_department_wig`;
/*!40000 ALTER TABLE `m_department_wig` DISABLE KEYS */;
INSERT INTO `m_department_wig` (`DEPARTMENT_WIG_ID`, `DEPARTMENT_WIG_NAME`, `DEPARTMENT_WIG_DESCRIPTION`, `DEPARTMENT_WIG_SEQUENCE`, `CREATED_DATE`, `CREATED_BY`, `UPDATED_DATE`, `UPDATED_BY`, `IS_DELETED`, `DEPARTMENT_ID`, `COMPANY_WIG_ID`, `COMPANY_LM_ID`) VALUES
	(1, 'Develop NPA from 0 to 10 features by Jun 2020', NULL, 1, '2020-03-05 14:23:48', 1, '2020-03-05 14:23:50', 1, b'0', 5, NULL, NULL),
	(2, 'Develop NSDX SE from 0 to 8 features by Jul 2020', NULL, 2, '2020-03-05 14:23:48', 1, '2020-03-05 14:23:50', 1, b'0', 5, NULL, NULL),
	(3, 'Develop new technologies from 0 to 3 features by Dec 2020', NULL, 3, '2020-03-05 14:23:48', 1, '2020-03-05 14:23:50', 1, b'0', 5, NULL, NULL),
	(4, 'Increase PCI of all R&D projects from xx% to 95% by Dec 2020', NULL, 4, '2020-03-05 14:23:48', 1, '2020-03-05 14:23:50', 1, b'0', 5, NULL, NULL),
	(5, 'Certify PINK from 3 to 4 Process by Aug 2020', NULL, 5, '2020-03-05 14:23:48', 1, '2020-03-05 14:23:50', 1, b'0', 5, NULL, NULL),
	(6, 'Increase number of activity that drive company goals from 0 to 260 by Dec 2020', NULL, 6, '2020-03-05 14:23:48', 1, '2020-03-05 14:23:50', 1, b'0', 5, NULL, NULL);
/*!40000 ALTER TABLE `m_department_wig` ENABLE KEYS */;

-- Dumping structure for table netkacommitment.m_parent_user
DROP TABLE IF EXISTS `m_parent_user`;
CREATE TABLE IF NOT EXISTS `m_parent_user` (
  `PARENT_USER_ID` int unsigned NOT NULL AUTO_INCREMENT,
  `USER_ID` int unsigned NOT NULL,
  `PARENT_ID` int unsigned NOT NULL,
  `CREATED_DATE` datetime NOT NULL,
  `CREATED_BY` int unsigned NOT NULL,
  `UPDATED_DATE` datetime DEFAULT NULL,
  `UPDATED_BY` int unsigned DEFAULT NULL,
  `IS_DELETED` bit(1) NOT NULL,
  PRIMARY KEY (`PARENT_USER_ID`),
  KEY `FK_m_parent_user_m_user` (`USER_ID`),
  KEY `FK_m_parent_user_m_user_2` (`PARENT_ID`),
  CONSTRAINT `FK_m_parent_user_m_user` FOREIGN KEY (`USER_ID`) REFERENCES `m_user` (`USER_ID`),
  CONSTRAINT `FK_m_parent_user_m_user_2` FOREIGN KEY (`PARENT_ID`) REFERENCES `m_user` (`USER_ID`)
) ENGINE=InnoDB AUTO_INCREMENT=33 DEFAULT CHARSET=utf8;

-- Dumping data for table netkacommitment.m_parent_user: ~32 rows (approximately)
DELETE FROM `m_parent_user`;
/*!40000 ALTER TABLE `m_parent_user` DISABLE KEYS */;
INSERT INTO `m_parent_user` (`PARENT_USER_ID`, `USER_ID`, `PARENT_ID`, `CREATED_DATE`, `CREATED_BY`, `UPDATED_DATE`, `UPDATED_BY`, `IS_DELETED`) VALUES
	(1, 15, 13, '2020-03-06 14:36:51', 1, '2020-03-06 14:36:53', 1, b'0'),
	(2, 16, 13, '2020-03-06 14:36:51', 1, '2020-03-06 14:36:53', 1, b'0'),
	(3, 17, 13, '2020-03-06 14:36:51', 1, '2020-03-06 14:36:53', 1, b'0'),
	(4, 24, 13, '2020-03-06 14:36:51', 1, '2020-03-06 14:36:53', 1, b'0'),
	(5, 26, 13, '2020-03-06 14:36:51', 1, '2020-03-06 14:36:53', 1, b'0'),
	(6, 29, 13, '2020-03-06 14:36:51', 1, '2020-03-06 14:36:53', 1, b'0'),
	(7, 30, 13, '2020-03-06 14:36:51', 1, '2020-03-06 14:36:53', 1, b'0'),
	(8, 32, 13, '2020-03-06 14:36:51', 1, '2020-03-06 14:36:53', 1, b'0'),
	(9, 34, 13, '2020-03-06 14:36:51', 1, '2020-03-06 14:36:53', 1, b'0'),
	(10, 42, 13, '2020-03-06 14:36:51', 1, '2020-03-06 14:36:53', 1, b'0'),
	(11, 44, 13, '2020-03-06 14:36:51', 1, '2020-03-06 14:36:53', 1, b'0'),
	(12, 47, 13, '2020-03-06 14:36:51', 1, '2020-03-06 14:36:53', 1, b'0'),
	(13, 52, 13, '2020-03-06 14:36:51', 1, '2020-03-06 14:36:53', 1, b'0'),
	(14, 56, 13, '2020-03-06 14:36:51', 1, '2020-03-06 14:36:53', 1, b'0'),
	(15, 59, 13, '2020-03-06 14:36:51', 1, '2020-03-06 14:36:53', 1, b'0'),
	(16, 61, 13, '2020-03-06 14:36:51', 1, '2020-03-06 14:36:53', 1, b'0'),
	(17, 15, 14, '2020-03-06 14:36:51', 1, '2020-03-06 14:36:53', 1, b'0'),
	(18, 16, 14, '2020-03-06 14:36:51', 1, '2020-03-06 14:36:53', 1, b'0'),
	(19, 17, 14, '2020-03-06 14:36:51', 1, '2020-03-06 14:36:53', 1, b'0'),
	(20, 24, 14, '2020-03-06 14:36:51', 1, '2020-03-06 14:36:53', 1, b'0'),
	(21, 26, 14, '2020-03-06 14:36:51', 1, '2020-03-06 14:36:53', 1, b'0'),
	(22, 29, 14, '2020-03-06 14:36:51', 1, '2020-03-06 14:36:53', 1, b'0'),
	(23, 30, 14, '2020-03-06 14:36:51', 1, '2020-03-06 14:36:53', 1, b'0'),
	(24, 32, 14, '2020-03-06 14:36:51', 1, '2020-03-06 14:36:53', 1, b'0'),
	(25, 34, 14, '2020-03-06 14:36:51', 1, '2020-03-06 14:36:53', 1, b'0'),
	(26, 42, 14, '2020-03-06 14:36:51', 1, '2020-03-06 14:36:53', 1, b'0'),
	(27, 44, 14, '2020-03-06 14:36:51', 1, '2020-03-06 14:36:53', 1, b'0'),
	(28, 47, 14, '2020-03-06 14:36:51', 1, '2020-03-06 14:36:53', 1, b'0'),
	(29, 52, 14, '2020-03-06 14:36:51', 1, '2020-03-06 14:36:53', 1, b'0'),
	(30, 56, 14, '2020-03-06 14:36:51', 1, '2020-03-06 14:36:53', 1, b'0'),
	(31, 59, 14, '2020-03-06 14:36:51', 1, '2020-03-06 14:36:53', 1, b'0'),
	(32, 61, 14, '2020-03-06 14:36:51', 1, '2020-03-06 14:36:53', 1, b'0');
/*!40000 ALTER TABLE `m_parent_user` ENABLE KEYS */;

-- Dumping structure for table netkacommitment.m_user
DROP TABLE IF EXISTS `m_user`;
CREATE TABLE IF NOT EXISTS `m_user` (
  `USER_ID` int unsigned NOT NULL AUTO_INCREMENT,
  `USER_CODE` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `USER_NAME` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `USER_PASSWORD` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL DEFAULT 'netka123',
  `USER_PASSWORD_RESET_TOKEN` varchar(50) DEFAULT NULL,
  `USER_FIRST_NAME` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `USER_LAST_NAME` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `USER_FIRST_NAME_EN` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `USER_LAST_NAME_EN` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `CREATED_DATE` datetime NOT NULL,
  `CREATED_BY` int unsigned NOT NULL,
  `UPDATED_DATE` datetime DEFAULT NULL,
  `UPDATED_BY` int unsigned DEFAULT NULL,
  `IS_DELETED` bit(1) NOT NULL,
  `DEPARTMENT_ID` int unsigned DEFAULT NULL,
  PRIMARY KEY (`USER_ID`),
  KEY `FK_m_user_m_department` (`DEPARTMENT_ID`),
  CONSTRAINT `FK_m_user_m_department` FOREIGN KEY (`DEPARTMENT_ID`) REFERENCES `m_department` (`DEPARTMENT_ID`)
) ENGINE=InnoDB AUTO_INCREMENT=67 DEFAULT CHARSET=utf8;

-- Dumping data for table netkacommitment.m_user: ~56 rows (approximately)
DELETE FROM `m_user`;
/*!40000 ALTER TABLE `m_user` DISABLE KEYS */;
INSERT INTO `m_user` (`USER_ID`, `USER_CODE`, `USER_NAME`, `USER_PASSWORD`, `USER_PASSWORD_RESET_TOKEN`, `USER_FIRST_NAME`, `USER_LAST_NAME`, `USER_FIRST_NAME_EN`, `USER_LAST_NAME_EN`, `CREATED_DATE`, `CREATED_BY`, `UPDATED_DATE`, `UPDATED_BY`, `IS_DELETED`, `DEPARTMENT_ID`) VALUES
	(11, '001020348', 'nipastraporn.j', 'netka123', NULL, 'นิพัสตราภรณ์', 'เจียมโชติพัฒนกุล', 'Nipastraporn', 'Jiamchoatpattanakul', '2020-03-05 14:35:59', 1, '2020-03-05 14:36:01', 1, b'0', NULL),
	(12, '014010648', 'charnchai.j', 'netka123', NULL, 'ชาญชัย', 'เจียมโชติพัฒนกุล', 'Charnchai', 'Jiamchoatpattanakul', '2020-03-05 14:35:59', 1, '2020-03-05 14:36:01', 1, b'0', NULL),
	(13, '020011250', 'wasut.p', 'netka123', NULL, 'วสุต', 'ปริพัฒนานนท์', 'Wasut', 'Paripattananont', '2020-03-05 14:35:59', 1, '2020-03-05 14:36:01', 1, b'0', 5),
	(14, '022110451', 'sukanin.m', 'netka123', NULL, 'สุคณินท์', 'มั่นมาก', 'Sukanin', 'Manmak', '2020-03-05 14:35:59', 1, '2020-03-05 14:36:01', 1, b'0', 5),
	(15, '024020352', 'thammas.p', 'netka123', NULL, 'ธรรมาส', 'โพธิสัตยา', 'Thammas', 'Photisattaya', '2020-03-05 14:35:59', 1, '2020-03-05 14:36:01', 1, b'0', 5),
	(16, '026150652', 'sontaya.t', 'netka123', NULL, 'สนธยา', 'ตั่นเล่ง', 'Sontaya', 'Tunleng', '2020-03-05 14:35:59', 1, '2020-03-05 14:36:01', 1, b'0', 5),
	(17, '027010752', 'jirawat.j', 'netka123', NULL, 'จิรวัฒน์', 'จิระพรกุล', 'Jirawat', 'Jirapornkul', '2020-03-05 14:35:59', 1, '2020-03-05 14:36:01', 1, b'0', 5),
	(18, '028150653', 'amara.s', 'netka123', NULL, 'อมรา', 'สุดยอดสำราญ', 'Amara', 'Sutyodsamran', '2020-03-05 14:35:59', 1, '2020-03-05 14:36:01', 1, b'0', 1),
	(19, '038030955', 'pom.t', 'netka123', NULL, 'ป้อม', 'ธัญญานนท์', 'Pom', 'Tanyanon', '2020-03-05 14:35:59', 1, '2020-03-05 14:36:01', 1, b'0', 7),
	(20, '039011055', 'narisara.l', 'netka123', NULL, 'นริสรา', 'เลี่ยมปรีชา', 'Narisara', 'Liampreecha', '2020-03-05 14:35:59', 1, '2020-03-05 14:36:01', 1, b'0', 4),
	(21, '042090156', 'nuttawoot.n', 'netka123', NULL, 'ณัฐวุฒิ', 'ณบางช้าง', 'Nuttawoot', 'Na-Bangchang', '2020-03-05 14:35:59', 1, '2020-03-05 14:36:01', 1, b'0', 3),
	(22, '044010556', 'wiphattha.p', 'netka123', NULL, 'วีภัทรา', 'พิศภา', 'Wiphattha', 'Phitsapha', '2020-03-05 14:35:59', 1, '2020-03-05 14:36:01', 1, b'0', 2),
	(23, '048011156', 'manaschai.s', 'netka123', NULL, 'มนัสชัย', 'สมัครแก้ว', 'Manaschai', 'Samakkaew', '2020-03-05 14:35:59', 1, '2020-03-05 14:36:01', 1, b'0', 4),
	(24, '068200759', 'kulnipa.b', 'netka123', NULL, 'กุลนิภา', 'บินสเล', 'Kulnipa', 'Binsale', '2020-03-05 14:35:59', 1, '2020-03-05 14:36:01', 1, b'0', 5),
	(25, '069010959', 'nattawee.s', 'netka123', NULL, 'ณัฏฐวี', 'สกุลรัตน์', 'Nattawee', 'Sakulrat', '2020-03-05 14:35:59', 1, '2020-03-05 14:36:01', 1, b'0', 6),
	(26, '070050959', 'parichat.c', 'netka123', NULL, 'ปาริฉัตร', 'เชื้อชาติ', 'Parichat', 'Chueachat', '2020-03-05 14:35:59', 1, '2020-03-05 14:36:01', 1, b'0', 5),
	(27, '071190959', 'artit.t', 'netka123', NULL, 'อาทิตย์', 'เตรณานนท์', 'Artit', 'Trenanont', '2020-03-05 14:35:59', 1, '2020-03-05 14:36:01', 1, b'0', 6),
	(28, '077240260', 'michael.s', 'netka123', NULL, 'Michael', 'Schlosser', 'Michael', 'Schlosser', '2020-03-05 14:35:59', 1, '2020-03-05 14:36:01', 1, b'0', 6),
	(29, '084110560', 'juthathip.c', 'netka123', NULL, 'จุฑาทิพย์', 'เชี่ยวชาญ', 'Juthathip', 'Cheawcharn', '2020-03-05 14:35:59', 1, '2020-03-05 14:36:01', 1, b'0', 5),
	(30, '085030760', 'viritipar.n', 'netka123', NULL, 'วิริฒิพา', 'นวลสม', 'Viritipar', 'Nualsom', '2020-03-05 14:35:59', 1, '2020-03-05 14:36:01', 1, b'0', 5),
	(31, '086150860', 'elizar.b', 'netka123', NULL, 'Elizar', 'Bainto', 'Elizar', 'Bainto', '2020-03-05 14:35:59', 1, '2020-03-05 14:36:01', 1, b'0', 6),
	(32, '091010261', 'arithus.s', 'netka123', NULL, 'อริธัช', 'แสนโสม', 'Arithus', 'Sansom', '2020-03-05 14:35:59', 1, '2020-03-05 14:36:01', 1, b'0', 5),
	(33, '092120361', 'vivach.c', 'netka123', NULL, 'วิวัช', 'ชลไชยะ', 'Vivach', 'Chonchaiya', '2020-03-05 14:35:59', 1, '2020-03-05 14:36:01', 1, b'0', 7),
	(34, '094010661', 'vittaya.j', 'netka123', NULL, 'วิทยา', 'จงอุดมพร', 'Vittaya', 'Jongudomporn', '2020-03-05 14:35:59', 1, '2020-03-05 14:36:01', 1, b'0', 5),
	(35, '095040661', 'charnvithya.s', 'netka123', NULL, 'ชาญวิทย์', 'เศรษฐะทัตต์', 'Charnvithya', 'Sresthadatta', '2020-03-05 14:35:59', 1, '2020-03-05 14:36:01', 1, b'0', 3),
	(36, '096020761', 'jedsada.r', 'netka123', NULL, 'เจษฎา', 'รังษีเทียนไชย', 'Jedsada', 'Rangsethienchai', '2020-03-05 14:35:59', 1, '2020-03-05 14:36:01', 1, b'0', 4),
	(37, '099030961', 'artit.p', 'netka123', NULL, 'อาทิตย์', 'ปักกาโต', 'Artit', 'Pakkato', '2020-03-05 14:35:59', 1, '2020-03-05 14:36:01', 1, b'0', 4),
	(38, '103170961', 'wanwisa.r', 'netka123', NULL, 'วันวิสาข์', 'รังสิมันต์รัตน์', 'Wanwisa', 'Rangsimanrat', '2020-03-05 14:35:59', 1, '2020-03-05 14:36:01', 1, b'0', 6),
	(39, '104011061', 'arucha.n', 'netka123', NULL, 'อรุชา', 'นันทิยะกุล', 'Arucha', 'Nuntiyakul', '2020-03-05 14:35:59', 1, '2020-03-05 14:36:01', 1, b'0', 2),
	(40, '106011161', 'choosak.k', 'netka123', NULL, 'ชูศักดิ์', 'กิ่งไทร', 'Choosak', 'Kingsai', '2020-03-05 14:35:59', 1, '2020-03-05 14:36:01', 1, b'0', 4),
	(41, '107011161', 'kitsanee.s', 'netka123', NULL, 'กฤษณี', 'แซ่ลิ้ม', 'Kitsanee', 'Saelim', '2020-03-05 14:35:59', 1, '2020-03-05 14:36:01', 1, b'0', 4),
	(42, '108031261', 'siriporn.s', 'netka123', NULL, 'ศิริพร', 'ศรีบุญ', 'Siriporn', 'Sriboon', '2020-03-05 14:35:59', 1, '2020-03-05 14:36:01', 1, b'0', 5),
	(43, '110171261', 'chotika.k', 'netka123', NULL, 'โชติกา', 'ขัติยะ', 'Chotika', 'Kattiya', '2020-03-05 14:35:59', 1, '2020-03-05 14:36:01', 1, b'0', 4),
	(44, '111020162', 'noraseth.t', 'netka123', NULL, 'นรเศรษฐ์', 'เทียนแก้ว', 'Noraseth', 'Teankaew', '2020-03-05 14:35:59', 1, '2020-03-05 14:36:01', 1, b'0', 5),
	(45, '112010262', 'teeratuch.s', 'netka123', NULL, 'ธีรธัช', 'เศรษฐโอฬารกิจ', 'Teeratuch', 'Setthaolankit', '2020-03-05 14:35:59', 1, '2020-03-05 14:36:01', 1, b'0', 4),
	(46, '114180262', 'srisakul.p', 'netka123', NULL, 'ศรีสกุล', 'ปรีชามาตร์', 'Srisakul', 'Preechamart', '2020-03-05 14:35:59', 1, '2020-03-05 14:36:01', 1, b'0', 3),
	(47, '115010462', 'nitchapat.l', 'netka123', NULL, 'ณิชชาภัทร', 'หล่อพงศกร', 'Nitchapat', 'Lhophongsakorn', '2020-03-05 14:35:59', 1, '2020-03-05 14:36:01', 1, b'0', 5),
	(48, '116010462', 'wasan.s', 'netka123', NULL, 'วสันต์', 'ศรีเหรา', 'Wasan', 'Srihera', '2020-03-05 14:35:59', 1, '2020-03-05 14:36:01', 1, b'0', 4),
	(49, '202010462', 'thodsapol.p', 'netka123', NULL, 'ทศพล', 'พลเขตต์', 'Thodsapol', 'Polkhet', '2020-03-05 14:35:59', 1, '2020-03-05 14:36:01', 1, b'0', 7),
	(50, '119170462', 'phatrachot.p', 'netka123', NULL, 'ภัทรร์โชติ์', 'ภัทรร์สิริโชติ', 'Phatrachot', 'Phatrasirichot', '2020-03-05 14:35:59', 1, '2020-03-05 14:36:01', 1, b'0', 4),
	(51, '117180462', 'jeerapan.a', 'netka123', NULL, 'จีรพรรณ', 'อนันต์มนตรีโชค', 'Jeerapan', 'Ananmontrichoke', '2020-03-05 14:35:59', 1, '2020-03-05 14:36:01', 1, b'0', 1),
	(52, '200010562', 'kamonvan.p', 'netka123', NULL, 'กมลวรรณ', 'ประจันศรี', 'Kamonvan', 'Prajansri', '2020-03-05 14:35:59', 1, '2020-03-05 14:36:01', 1, b'0', 5),
	(53, '204010762', 'theerasak.s', 'netka123', NULL, 'ธีรศักดิ์', 'สักกทัตติยกุล', 'Theerasak', 'Sakatatiyagul', '2020-03-05 14:35:59', 1, '2020-03-05 14:36:01', 1, b'0', 4),
	(54, '207020962', 'sirichai.p', 'netka123', NULL, 'สิริชัย', 'โพธิเกษม', 'Sirichai', 'Potikasame', '2020-03-05 14:35:59', 1, '2020-03-05 14:36:01', 1, b'0', 2),
	(55, '206020962', 'donrawat.c', 'netka123', NULL, 'ดลวรรธน์', 'ชวาลสันตติ', 'Donrawat', 'Chawansantati', '2020-03-05 14:35:59', 1, '2020-03-05 14:36:01', 1, b'0', 4),
	(56, '208011062', 'kwanchai.t', 'netka123', NULL, 'ขวัญชัย', 'ตระกูลสันติชัย', 'Kwanchai', 'Trakulsantichai', '2020-03-05 14:35:59', 1, '2020-03-05 14:36:01', 1, b'0', 5),
	(57, '210011062', 'jadnipat.t', 'netka123', NULL, 'เจตนิพัทธ์', 'โทมล', 'Jadnipat', 'Thomol', '2020-03-05 14:35:59', 1, '2020-03-05 14:36:01', 1, b'0', 6),
	(58, '209011162', 'thanchanok.l', 'netka123', NULL, 'ธันยชนก', 'เล่าพรหมสุคนธ์', 'Thanchanok', 'Laopromsukon', '2020-03-05 14:35:59', 1, '2020-03-05 14:36:01', 1, b'0', 4),
	(59, '212011162', 'nattaphol.s', 'netka123', NULL, 'ณัฐพล', 'แสงจำปา', 'Nattaphol', 'Sangjumpa', '2020-03-05 14:35:59', 1, '2020-03-05 14:36:01', 1, b'0', 5),
	(60, '215021262', 'chodkorn.m', 'netka123', NULL, 'ชชกร', 'เมธีวิวิธชัย', 'Chodkorn', 'Methiwiwitchai', '2020-03-05 14:35:59', 1, '2020-03-05 14:36:01', 1, b'0', 2),
	(61, '213011262', 'ranchana.k', 'netka123', NULL, 'รัญชนา', 'เกตุแจ้งธรรม', 'Ranchana', 'Katejaengtham', '2020-03-05 14:35:59', 1, '2020-03-05 14:36:01', 1, b'0', 5),
	(62, '214091262', 'thanatchapak.w', 'netka123', NULL, 'ธนัชภัค', 'วัฒนาโสภณ', 'Thanatchapak', 'Wattanasophon', '2020-03-05 14:35:59', 1, '2020-03-05 14:36:01', 1, b'0', 1),
	(63, '217020163', 'thanaporn.k', 'netka123', NULL, 'ธนาพร', 'ก้อนแก้ว', 'Thanaporn', 'Konkaew', '2020-03-05 14:35:59', 1, '2020-03-05 14:36:01', 1, b'0', 3),
	(64, '219270163', 'opart.t', 'netka123', NULL, 'โอภาส', 'ไตรรัตนะ', 'Opart', 'Trairattana', '2020-03-05 14:35:59', 1, '2020-03-05 14:36:01', 1, b'0', 4),
	(65, '218270163', 'navapol.p', 'netka123', NULL, 'นวพล', 'ไพศาลอัศวเสนี', 'Navapol', 'Phaisal-Atsawasenee', '2020-03-05 14:35:59', 1, '2020-03-05 14:36:01', 1, b'0', 4),
	(66, '216030263', 'warisara.y', 'netka123', NULL, 'วริศรา', 'แย้มกลัด', 'Warisara', 'Yamklad', '2020-03-05 14:35:59', 1, '2020-03-05 14:36:01', 1, b'0', 4);
/*!40000 ALTER TABLE `m_user` ENABLE KEYS */;

-- Dumping structure for table netkacommitment.t_access_log
DROP TABLE IF EXISTS `t_access_log`;
CREATE TABLE IF NOT EXISTS `t_access_log` (
  `ACCESS_LOG_ID` int unsigned NOT NULL AUTO_INCREMENT,
  `ACCESS_LOG_DEVICE` varchar(500) NOT NULL,
  `ACCESS_LOG_KEY` varchar(500) NOT NULL,
  `ACCESS_LOG_URL` varchar(500) NOT NULL,
  `ACCESS_LOG_CREATED_DATE` datetime NOT NULL,
  `USER_ID` int unsigned NOT NULL,
  PRIMARY KEY (`ACCESS_LOG_ID`),
  KEY `FK_t_access_log_m_user` (`USER_ID`),
  CONSTRAINT `FK_t_access_log_m_user` FOREIGN KEY (`USER_ID`) REFERENCES `m_user` (`USER_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Dumping data for table netkacommitment.t_access_log: ~0 rows (approximately)
DELETE FROM `t_access_log`;
/*!40000 ALTER TABLE `t_access_log` DISABLE KEYS */;
/*!40000 ALTER TABLE `t_access_log` ENABLE KEYS */;

-- Dumping structure for table netkacommitment.t_access_token
DROP TABLE IF EXISTS `t_access_token`;
CREATE TABLE IF NOT EXISTS `t_access_token` (
  `ACCESS_TOKEN_ID` int unsigned NOT NULL AUTO_INCREMENT,
  `ACCESS_TOKEN_DEVICE` varchar(500) NOT NULL,
  `ACCESS_TOKEN_KEY` varchar(500) NOT NULL,
  `ACCESS_TOKEN_CREATED_DATE` datetime NOT NULL,
  `ACCESS_TOKEN_UPDATED_DATE` datetime DEFAULT NULL,
  `ACCESS_TOKEN_EXPRIED_DATE` datetime NOT NULL,
  `USER_ID` int unsigned DEFAULT NULL,
  PRIMARY KEY (`ACCESS_TOKEN_ID`),
  KEY `FK_t_access_token_m_user` (`USER_ID`),
  CONSTRAINT `FK_t_access_token_m_user` FOREIGN KEY (`USER_ID`) REFERENCES `m_user` (`USER_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Dumping data for table netkacommitment.t_access_token: ~0 rows (approximately)
DELETE FROM `t_access_token`;
/*!40000 ALTER TABLE `t_access_token` DISABLE KEYS */;
/*!40000 ALTER TABLE `t_access_token` ENABLE KEYS */;

-- Dumping structure for table netkacommitment.t_approve
DROP TABLE IF EXISTS `t_approve`;
CREATE TABLE IF NOT EXISTS `t_approve` (
  `APPROVE_ID` int unsigned NOT NULL AUTO_INCREMENT,
  `APPROVE_NO` int unsigned NOT NULL DEFAULT '1',
  `APPROVE_TYPE` varchar(500) NOT NULL DEFAULT 'ยาก',
  `APPROVE_STATUS` varchar(50) NOT NULL DEFAULT 'Watting for approval.',
  `APPROVE_REMARK` varchar(500) DEFAULT NULL,
  `CREATED_DATE` datetime NOT NULL,
  `CREATED_BY` int unsigned NOT NULL,
  `UPDATED_DATE` datetime DEFAULT NULL,
  `UPDATED_BY` int unsigned DEFAULT NULL,
  `IS_DELETED` bit(1) NOT NULL,
  `APPROVE_USER_ID` int unsigned NOT NULL,
  `COMMITMENT_ID` int unsigned NOT NULL,
  PRIMARY KEY (`APPROVE_ID`),
  KEY `FK_t_approve_t_commitment` (`COMMITMENT_ID`),
  KEY `FK_t_approve_m_user` (`APPROVE_USER_ID`),
  CONSTRAINT `FK_t_approve_m_user` FOREIGN KEY (`APPROVE_USER_ID`) REFERENCES `m_user` (`USER_ID`),
  CONSTRAINT `FK_t_approve_t_commitment` FOREIGN KEY (`COMMITMENT_ID`) REFERENCES `t_commitment` (`COMMITMENT_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Dumping data for table netkacommitment.t_approve: ~0 rows (approximately)
DELETE FROM `t_approve`;
/*!40000 ALTER TABLE `t_approve` DISABLE KEYS */;
/*!40000 ALTER TABLE `t_approve` ENABLE KEYS */;

-- Dumping structure for table netkacommitment.t_commitment
DROP TABLE IF EXISTS `t_commitment`;
CREATE TABLE IF NOT EXISTS `t_commitment` (
  `COMMITMENT_ID` int unsigned NOT NULL AUTO_INCREMENT,
  `COMMITMENT_NO` int unsigned NOT NULL DEFAULT '1',
  `COMMITMENT_NAME` varchar(500) NOT NULL,
  `COMMITMENT_DESCRIPTION` varchar(500) DEFAULT NULL,
  `COMMITMENT_REMARK` varchar(500) DEFAULT NULL,
  `COMMITMENT_START_DATE` datetime NOT NULL,
  `COMMITMENT_FINISH_DATE` datetime DEFAULT NULL,
  `COMMITMENT_IS_DELETED` bit(1) NOT NULL DEFAULT b'0',
  `COMMITMENT_STATUS` varchar(50) NOT NULL DEFAULT 'Watting for approval.',
  `CREATED_DATE` datetime NOT NULL,
  `CREATED_BY` int unsigned NOT NULL,
  `UPDATED_DATE` datetime DEFAULT NULL,
  `UPDATED_BY` int unsigned DEFAULT NULL,
  `IS_DELETED` bit(1) NOT NULL,
  `COMMITMENT_LM` int unsigned NOT NULL,
  `USER_ID` int unsigned NOT NULL,
  PRIMARY KEY (`COMMITMENT_ID`),
  KEY `FK_t_commitment_m_department_lm` (`COMMITMENT_LM`),
  KEY `FK_t_commitment_m_user` (`USER_ID`),
  CONSTRAINT `FK_t_commitment_m_department_lm` FOREIGN KEY (`COMMITMENT_LM`) REFERENCES `m_department_lm` (`LM_ID`),
  CONSTRAINT `FK_t_commitment_m_user` FOREIGN KEY (`USER_ID`) REFERENCES `m_user` (`USER_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Dumping data for table netkacommitment.t_commitment: ~0 rows (approximately)
DELETE FROM `t_commitment`;
/*!40000 ALTER TABLE `t_commitment` DISABLE KEYS */;
/*!40000 ALTER TABLE `t_commitment` ENABLE KEYS */;

-- Dumping structure for table netkacommitment.t_firebase_notification
DROP TABLE IF EXISTS `t_firebase_notification`;
CREATE TABLE IF NOT EXISTS `t_firebase_notification` (
  `FIREBASE_ID` int unsigned NOT NULL AUTO_INCREMENT,
  `FIREBASE_MAC_ADDRESS` varchar(200) NOT NULL,
  `FIREBASE_FCM_KEY` varchar(1000) NOT NULL,
  `FIREBASE_PLATFORM` varchar(10) NOT NULL,
  `CREATED_DATE` datetime NOT NULL,
  `CREATED_BY` int unsigned NOT NULL,
  `UPDATED_DATE` datetime DEFAULT NULL,
  `UPDATED_BY` int unsigned DEFAULT NULL,
  `IS_DELETED` bit(1) NOT NULL,
  `USER_ID` int unsigned NOT NULL,
  PRIMARY KEY (`FIREBASE_ID`),
  KEY `fk_t_firebase_m_user` (`USER_ID`),
  CONSTRAINT `fk_t_firebase_m_user` FOREIGN KEY (`USER_ID`) REFERENCES `m_user` (`USER_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Dumping data for table netkacommitment.t_firebase_notification: ~0 rows (approximately)
DELETE FROM `t_firebase_notification`;
/*!40000 ALTER TABLE `t_firebase_notification` DISABLE KEYS */;
/*!40000 ALTER TABLE `t_firebase_notification` ENABLE KEYS */;

-- Dumping structure for table netkacommitment.t_notification_log
DROP TABLE IF EXISTS `t_notification_log`;
CREATE TABLE IF NOT EXISTS `t_notification_log` (
  `NOTIFICATION_ID` int unsigned NOT NULL AUTO_INCREMENT,
  `NOTIFICATION_NAME` varchar(500) NOT NULL,
  `NOTIFICATION_DESCRIPTION` varchar(1000) DEFAULT NULL,
  `NOTIFICATION_FCM_KEY` varchar(1000) NOT NULL,
  `NOTIFICATION_PLATFORM` varchar(10) NOT NULL,
  `NOTIFICATION_IS_SENT` bit(1) NOT NULL,
  `NOTIFICATION_COUNT` int unsigned NOT NULL DEFAULT '1',
  `NOTIFICATION_LIMIT` int unsigned NOT NULL DEFAULT '3',
  `CREATED_DATE` datetime NOT NULL,
  `CREATED_BY` int unsigned NOT NULL,
  `UPDATED_DATE` datetime DEFAULT NULL,
  `UPDATED_BY` int unsigned DEFAULT NULL,
  `IS_DELETED` bit(1) NOT NULL,
  `USER_ID` int unsigned NOT NULL,
  PRIMARY KEY (`NOTIFICATION_ID`),
  KEY `FK_t_notification_m_user` (`USER_ID`),
  CONSTRAINT `FK_t_notification_m_user` FOREIGN KEY (`USER_ID`) REFERENCES `m_user` (`USER_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- Dumping data for table netkacommitment.t_notification_log: ~0 rows (approximately)
DELETE FROM `t_notification_log`;
/*!40000 ALTER TABLE `t_notification_log` DISABLE KEYS */;
/*!40000 ALTER TABLE `t_notification_log` ENABLE KEYS */;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
