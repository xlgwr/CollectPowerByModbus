/*
Navicat MySQL Data Transfer

Source Server         : mysql
Source Server Version : 50626
Source Host           : localhost:3306
Source Database       : epmcs

Target Server Type    : MYSQL
Target Server Version : 50626
File Encoding         : 65001

Date: 2015-08-07 09:12:37
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for keyvalparams
-- ----------------------------
DROP TABLE IF EXISTS `keyvalparams`;
CREATE TABLE `keyvalparams` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `K` varchar(32) NOT NULL,
  `V` varchar(400) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for meterparams
-- ----------------------------
DROP TABLE IF EXISTS `meterparams`;
CREATE TABLE `meterparams` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `CustomerId` longtext,
  `DeviceId` longtext,
  `DeviceCd` longtext,
  `DeviceName` longtext,
  `FDeviceId` longtext,
  `DemandValue` int(11) NOT NULL,
  `Level1` int(11) NOT NULL,
  `Level2` int(11) NOT NULL,
  `Level3` int(11) NOT NULL,
  `Level4` int(11) NOT NULL,
  `Port` longtext,
  `DeviceAdd` longtext,
  `Message` longtext,
  `ComputationRule` longtext,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for uploaddatas
-- ----------------------------
DROP TABLE IF EXISTS `uploaddatas`;
CREATE TABLE `uploaddatas` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `CustomerId` varchar(25) NOT NULL,
  `DeviceId` varchar(25) NOT NULL,
  `DeviceCd` varchar(25) NOT NULL,
  `Groupstamp` longtext NOT NULL,
  `PowerDate` datetime NOT NULL,
  `PowerValue` double NOT NULL,
  `ValueLevel` int(11) NOT NULL,
  `MeterValue` double NOT NULL,
  `DiffMeterValuePre` double NOT NULL,
  `PrePowerDate` datetime NOT NULL,
  `A1` double NOT NULL,
  `A2` double NOT NULL,
  `A3` double NOT NULL,
  `V1` double NOT NULL,
  `V2` double NOT NULL,
  `V3` double NOT NULL,
  `Pf` double NOT NULL,
  `Uploaded` int(11) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for __migrationhistory
-- ----------------------------
DROP TABLE IF EXISTS `__migrationhistory`;
CREATE TABLE `__migrationhistory` (
  `MigrationId` varchar(150) NOT NULL,
  `ContextKey` varchar(300) NOT NULL,
  `Model` longblob NOT NULL,
  `ProductVersion` varchar(32) NOT NULL,
  PRIMARY KEY (`MigrationId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
