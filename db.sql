-- --------------------------------------------------------
-- Hôte :                        mysql-robebou.alwaysdata.net
-- Version du serveur:           10.5.16-MariaDB - MariaDB Server
-- SE du serveur:                Linux
-- HeidiSQL Version:             11.0.0.5919
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;


-- Listage de la structure de la base pour robebou_m1_application_project
CREATE DATABASE IF NOT EXISTS `robebou_m1_application_project` /*!40100 DEFAULT CHARACTER SET utf8mb4 */;
USE `robebou_m1_application_project`;

-- Listage de la structure de la table robebou_m1_application_project. Clerk
CREATE TABLE IF NOT EXISTS `Clerk` (
  `idClerk` int(11) NOT NULL AUTO_INCREMENT,
  `fname` varchar(50) NOT NULL DEFAULT '',
  `lname` varchar(50) NOT NULL DEFAULT '',
  PRIMARY KEY (`idClerk`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4;

-- Listage des données de la table robebou_m1_application_project.Clerk : ~2 rows (environ)
/*!40000 ALTER TABLE `Clerk` DISABLE KEYS */;
INSERT INTO `Clerk` (`idClerk`, `fname`, `lname`) VALUES
	(1, 'Julienne', 'Goddu'),
	(2, 'Pascal', 'Carlon');
/*!40000 ALTER TABLE `Clerk` ENABLE KEYS */;

-- Listage de la structure de la table robebou_m1_application_project. Customer
CREATE TABLE IF NOT EXISTS `Customer` (
  `customerID` int(11) NOT NULL AUTO_INCREMENT,
  `FirstName` varchar(50) DEFAULT NULL,
  `LastName` varchar(50) DEFAULT NULL,
  `PhoneNumber` varchar(50) DEFAULT NULL,
  `Town` varchar(50) DEFAULT NULL,
  `Street` varchar(50) DEFAULT NULL,
  `PostalCode` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`customerID`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4;

-- Listage des données de la table robebou_m1_application_project.Customer : ~2 rows (environ)
/*!40000 ALTER TABLE `Customer` DISABLE KEYS */;
INSERT INTO `Customer` (`customerID`, `FirstName`, `LastName`, `PhoneNumber`, `Town`, `Street`, `PostalCode`) VALUES
	(1, 'sanchez', 'pablo', '0680218866', 'montrouge', 'avenue henir ginoux', '92120'),
	(2, 'theret', 'guillaume', '0642', 'paris', 'rue jean jaures', '75012');
/*!40000 ALTER TABLE `Customer` ENABLE KEYS */;

-- Listage de la structure de la table robebou_m1_application_project. CustomerOrder
CREATE TABLE IF NOT EXISTS `CustomerOrder` (
  `customerOrderID` int(11) NOT NULL AUTO_INCREMENT,
  `customerID` int(11) DEFAULT NULL,
  `status` varchar(50) DEFAULT NULL,
  `orderDate` datetime DEFAULT NULL,
  `idClerk` int(11) NOT NULL,
  `idDeliverer` int(11) NOT NULL,
  `price` int(11) DEFAULT NULL,
  PRIMARY KEY (`customerOrderID`),
  KEY `customerID` (`customerID`),
  CONSTRAINT `CustomerOrder_ibfk_1` FOREIGN KEY (`customerID`) REFERENCES `Customer` (`customerID`)
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=utf8mb4;

-- Listage des données de la table robebou_m1_application_project.CustomerOrder : ~4 rows (environ)
/*!40000 ALTER TABLE `CustomerOrder` DISABLE KEYS */;
INSERT INTO `CustomerOrder` (`customerOrderID`, `customerID`, `status`, `orderDate`, `idClerk`, `idDeliverer`, `price`) VALUES
	(1, 1, 'ordered', '2022-10-12 20:22:02', 1, 2, NULL),
	(2, 2, 'ordered', '2022-10-12 11:59:56', 2, 3, NULL),
	(3, 1, 'ordered', '2022-10-21 20:12:19', 2, 2, NULL),
	(11, 2, 'ordered', '2022-10-21 18:41:13', 0, 0, 37);
/*!40000 ALTER TABLE `CustomerOrder` ENABLE KEYS */;

-- Listage de la structure de la table robebou_m1_application_project. Deliverer
CREATE TABLE IF NOT EXISTS `Deliverer` (
  `idDeliverer` int(11) NOT NULL AUTO_INCREMENT,
  `fname` varchar(50) NOT NULL DEFAULT '',
  `lname` varchar(50) NOT NULL DEFAULT '',
  PRIMARY KEY (`idDeliverer`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4;

-- Listage des données de la table robebou_m1_application_project.Deliverer : ~3 rows (environ)
/*!40000 ALTER TABLE `Deliverer` DISABLE KEYS */;
INSERT INTO `Deliverer` (`idDeliverer`, `fname`, `lname`) VALUES
	(1, 'Didier', 'Massé'),
	(2, 'Mason', 'Lambert'),
	(3, 'Anne', 'Patel');
/*!40000 ALTER TABLE `Deliverer` ENABLE KEYS */;

-- Listage de la structure de la table robebou_m1_application_project. Drink
CREATE TABLE IF NOT EXISTS `Drink` (
  `drinkID` int(11) NOT NULL,
  `name` varchar(50) DEFAULT NULL,
  `description` varchar(50) DEFAULT NULL,
  `price` int(11) NOT NULL DEFAULT 0,
  PRIMARY KEY (`drinkID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Listage des données de la table robebou_m1_application_project.Drink : ~5 rows (environ)
/*!40000 ALTER TABLE `Drink` DISABLE KEYS */;
INSERT INTO `Drink` (`drinkID`, `name`, `description`, `price`) VALUES
	(1, 'Coca-Cola', NULL, 3),
	(2, 'Fanta', NULL, 3),
	(3, 'Ice Tea', NULL, 3),
	(4, 'Red Bull', NULL, 3),
	(5, 'Orange Juice', NULL, 2);
/*!40000 ALTER TABLE `Drink` ENABLE KEYS */;

-- Listage de la structure de la table robebou_m1_application_project. DrinkOrder
CREATE TABLE IF NOT EXISTS `DrinkOrder` (
  `drinkID` int(11) NOT NULL,
  `customerOrderID` int(11) NOT NULL,
  `quantity` int(11) DEFAULT NULL,
  PRIMARY KEY (`drinkID`,`customerOrderID`),
  KEY `customerOrderID` (`customerOrderID`),
  CONSTRAINT `DrinkOrder_ibfk_1` FOREIGN KEY (`drinkID`) REFERENCES `Drink` (`drinkID`),
  CONSTRAINT `DrinkOrder_ibfk_2` FOREIGN KEY (`customerOrderID`) REFERENCES `CustomerOrder` (`customerOrderID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Listage des données de la table robebou_m1_application_project.DrinkOrder : ~3 rows (environ)
/*!40000 ALTER TABLE `DrinkOrder` DISABLE KEYS */;
INSERT INTO `DrinkOrder` (`drinkID`, `customerOrderID`, `quantity`) VALUES
	(1, 1, 1),
	(2, 1, 2),
	(3, 2, 4);
/*!40000 ALTER TABLE `DrinkOrder` ENABLE KEYS */;

-- Listage de la structure de la table robebou_m1_application_project. Pizza
CREATE TABLE IF NOT EXISTS `Pizza` (
  `pizzaID` int(11) NOT NULL,
  `name` varchar(50) DEFAULT NULL,
  `description` varchar(50) DEFAULT NULL,
  `price` int(11) NOT NULL DEFAULT 0,
  PRIMARY KEY (`pizzaID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Listage des données de la table robebou_m1_application_project.Pizza : ~7 rows (environ)
/*!40000 ALTER TABLE `Pizza` DISABLE KEYS */;
INSERT INTO `Pizza` (`pizzaID`, `name`, `description`, `price`) VALUES
	(0, 'Savoyarde', NULL, 15),
	(1, 'Margerita', NULL, 8),
	(2, 'Calzone', NULL, 12),
	(3, 'Reine', NULL, 10),
	(4, 'Pepperoni', NULL, 8),
	(5, 'Merguez', NULL, 8),
	(6, 'Vegetarienne', NULL, 9);
/*!40000 ALTER TABLE `Pizza` ENABLE KEYS */;

-- Listage de la structure de la table robebou_m1_application_project. PizzaOrder
CREATE TABLE IF NOT EXISTS `PizzaOrder` (
  `pizzaID` int(11) NOT NULL,
  `customerOrderID` int(11) NOT NULL,
  `quantity` int(11) NOT NULL,
  `size` char(50) DEFAULT NULL,
  PRIMARY KEY (`pizzaID`,`customerOrderID`,`size`),
  KEY `customerOrderID` (`customerOrderID`),
  CONSTRAINT `PizzaOrder_ibfk_1` FOREIGN KEY (`pizzaID`) REFERENCES `Pizza` (`pizzaID`),
  CONSTRAINT `PizzaOrder_ibfk_2` FOREIGN KEY (`customerOrderID`) REFERENCES `CustomerOrder` (`customerOrderID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Listage des données de la table robebou_m1_application_project.PizzaOrder : ~4 rows (environ)
/*!40000 ALTER TABLE `PizzaOrder` DISABLE KEYS */;
INSERT INTO `PizzaOrder` (`pizzaID`, `customerOrderID`, `quantity`, `size`) VALUES
	(1, 1, 2, NULL),
	(2, 1, 1, NULL),
	(4, 2, 4, NULL),
	(6, 1, 1, NULL);
/*!40000 ALTER TABLE `PizzaOrder` ENABLE KEYS */;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
