/*
Navicat MySQL Data Transfer

Source Server         : mysql
Source Server Version : 50626
Source Host           : 127.0.0.1:3306
Source Database       : epmcs

Target Server Type    : MYSQL
Target Server Version : 50626
File Encoding         : 65001

Date: 2015-09-28 10:32:14
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
  `CustomerId` varchar(75) NOT NULL,
  `DeviceId` varchar(75) NOT NULL,
  `DeviceCd` varchar(75) NOT NULL,
  `DeviceName` varchar(600) DEFAULT NULL,
  `FDeviceId` varchar(75) DEFAULT NULL,
  `DemandValue` int(11) NOT NULL,
  `Level1` int(11) NOT NULL,
  `Level2` int(11) NOT NULL,
  `Level3` int(11) NOT NULL,
  `Level4` int(11) NOT NULL,
  `Port` varchar(75) DEFAULT NULL,
  `DeviceAdd` varchar(120) DEFAULT NULL,
  `Message` varchar(4096) DEFAULT NULL,
  `ComputationRule` varchar(600) DEFAULT NULL,
  `StartDate` datetime NOT NULL,
  `EndDate` datetime NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=56 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for uploaddatas
-- ----------------------------
DROP TABLE IF EXISTS `uploaddatas`;
CREATE TABLE `uploaddatas` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `CustomerId` varchar(75) NOT NULL,
  `DeviceId` varchar(75) NOT NULL,
  `DeviceCd` varchar(75) NOT NULL,
  `Groupstamp` varchar(32) NOT NULL,
  `PowerDate` datetime NOT NULL,
  `PowerValue` double NOT NULL,
  `ValueLevel` int(11) NOT NULL,
  `MeterValueW` double NOT NULL,
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
) ENGINE=InnoDB AUTO_INCREMENT=64 DEFAULT CHARSET=utf8;

