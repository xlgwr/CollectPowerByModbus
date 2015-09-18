-- --------------------------------------------------------
-- 主机:                           127.0.0.1
-- 服务器版本:                        5.6.12-log - MySQL Community Server (GPL)
-- 服务器操作系统:                      Win64
-- HeidiSQL 版本:                  9.2.0.4947
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

-- 导出  表 epmcs.keyvalparams 结构
DROP TABLE IF EXISTS `keyvalparams`;
CREATE TABLE IF NOT EXISTS `keyvalparams` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `K` varchar(32) NOT NULL,
  `V` varchar(400) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- 数据导出被取消选择。


-- 导出  表 epmcs.meterparams 结构
DROP TABLE IF EXISTS `meterparams`;
CREATE TABLE IF NOT EXISTS `meterparams` (
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
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- 数据导出被取消选择。


-- 导出  表 epmcs.uploaddatas 结构
DROP TABLE IF EXISTS `uploaddatas`;
CREATE TABLE IF NOT EXISTS `uploaddatas` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `CustomerId` varchar(75) NOT NULL,
  `DeviceId` varchar(75) NOT NULL,
  `DeviceCd` varchar(75) NOT NULL,
  `Groupstamp` varchar(32) NOT NULL,
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
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- 数据导出被取消选择。


-- 导出  表 epmcs.__migrationhistory 结构
DROP TABLE IF EXISTS `__migrationhistory`;
CREATE TABLE IF NOT EXISTS `__migrationhistory` (
  `MigrationId` varchar(150) NOT NULL,
  `ContextKey` varchar(300) NOT NULL,
  `Model` longblob NOT NULL,
  `ProductVersion` varchar(32) NOT NULL,
  PRIMARY KEY (`MigrationId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- 数据导出被取消选择。
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
