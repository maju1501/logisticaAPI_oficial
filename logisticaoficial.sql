-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Nov 29, 2025 at 03:17 PM
-- Server version: 10.4.32-MariaDB
-- PHP Version: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `logisticaoficial`
--

-- --------------------------------------------------------

--
-- Table structure for table `rotas`
--

CREATE TABLE `rotas` (
  `Id` int(11) NOT NULL,
  `Origem` varchar(255) DEFAULT NULL,
  `Destino` varchar(255) DEFAULT NULL,
  `DistanciaKm` double DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `rotas`
--

INSERT INTO `rotas` (`Id`, `Origem`, `Destino`, `DistanciaKm`) VALUES
(1, 'São Paulo', 'Campinas', 99.5),
(2, 'Campinas', 'Ribeirão Preto', 218.7),
(3, 'Ribeirão Preto', 'Franca', 88.2),
(4, 'São Paulo', 'Sorocaba', 101.3),
(5, 'Sorocaba', 'Itapetininga', 53.8),
(6, 'Bauru', 'Marília', 107.4),
(7, 'Marília', 'Assis', 66.9),
(8, 'Londrina', 'Maringá', 97.1),
(9, 'Curitiba', 'Joinville', 130.6),
(10, 'Curitiba', 'Ponta Grossa', 114.2);

-- --------------------------------------------------------

--
-- Table structure for table `usuarios`
--

CREATE TABLE `usuarios` (
  `Id` int(11) NOT NULL,
  `Nome` varchar(255) DEFAULT NULL,
  `Email` varchar(255) DEFAULT NULL,
  `Senha` varchar(255) DEFAULT NULL,
  `Endereco` varchar(255) DEFAULT NULL,
  `Telefone` varchar(50) DEFAULT NULL,
  `CNH` varchar(50) DEFAULT NULL,
  `Categoria` varchar(50) DEFAULT NULL,
  `Discriminator` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `usuarios`
--

INSERT INTO `usuarios` (`Id`, `Nome`, `Email`, `Senha`, `Endereco`, `Telefone`, `CNH`, `Categoria`, `Discriminator`) VALUES
(1, 'Ana Paula Mendes', 'ana.mendes@example.com', 'senha123', 'Rua das Flores, 120', '(14) 99812-3045', NULL, NULL, 'motorista'),
(2, 'Bruno Henrique Souza', 'bruno.souza@example.com', 'minhasenha', 'Av. Brasil, 455', '(11) 99745-1234', NULL, NULL, 'Cliente'),
(3, 'Carolina Dias', 'carol.dias@example.com', '123456', 'Rua Pedro Álvares, 78', '(19) 99123-9874', NULL, NULL, 'Cliente'),
(4, 'Diego Martins', 'diego.martins@example.com', 'abc123', 'Rua Tiradentes, 999', '(14) 99654-2211', NULL, NULL, 'Cliente'),
(5, 'Eduarda Lima', 'eduarda.lima@example.com', 'senha456', 'Av. Europa, 300', '(15) 99456-7788', NULL, NULL, 'Cliente'),
(6, 'Fernando Tavares', 'fernando.tavares@example.com', 'tavares2025', 'Rua XV de Novembro, 160', '(18) 99321-6547', NULL, NULL, 'Cliente'),
(7, 'Gabriela Rocha', 'gabi.rocha@example.com', 'gabirocha', 'Rua São João, 45', '(17) 99854-2210', NULL, NULL, 'Cliente'),
(8, 'Heitor Carvalho', 'heitor.carvalho@example.com', 'heitor123', 'Av. Central, 785', '(12) 99110-5588', NULL, NULL, 'Cliente'),
(9, 'Isabela Ferreira', 'isa.ferreira@example.com', 'isabela789', 'Rua Dom Pedro, 234', '(16) 99543-8877', NULL, NULL, 'Cliente'),
(10, 'João Vitor Nunes', 'joao.nunes@example.com', 'joaovitor', 'Rua Primavera, 12', '(13) 99223-4411', NULL, NULL, 'Cliente'),
(11, 'dasdasdasdas', 'dasdasdasdas', 'assadasdsad', 'asdasdasd', '1312321', NULL, NULL, 'Cliente'),
(12, 'Ana Paula Mendes', 'ana.mendes@example.com', 'senha123', 'Rua das Flores, 120', '(14) 99812-3045', NULL, NULL, 'Cliente'),
(13, 'Bruno Henrique Souza', 'bruno.souza@example.com', 'minhasenha', 'Av. Brasil, 455', '(11) 99745-1234', NULL, NULL, 'Cliente'),
(14, 'Carolina Dias', 'carol.dias@example.com', '123456', 'Rua Pedro Álvares, 78', '(19) 99123-9874', NULL, NULL, 'Cliente'),
(15, 'Diego Martins', 'diego.martins@example.com', 'abc123', 'Rua Tiradentes, 999', '(14) 99654-2211', NULL, NULL, 'Cliente'),
(16, 'Eduarda Lima', 'eduarda.lima@example.com', 'senha456', 'Av. Europa, 300', '(15) 99456-7788', NULL, NULL, 'Cliente'),
(17, 'Fernando Tavares', 'fernando.tavares@example.com', 'tavares2025', 'Rua XV de Novembro, 160', '(18) 99321-6547', NULL, NULL, 'Cliente'),
(18, 'Gabriela Rocha', 'gabi.rocha@example.com', 'gabirocha', 'Rua São João, 45', '(17) 99854-2210', NULL, NULL, 'Cliente'),
(19, 'Heitor Carvalho', 'heitor.carvalho@example.com', 'heitor123', 'Av. Central, 785', '(12) 99110-5588', NULL, NULL, 'Cliente'),
(20, 'Isabela Ferreira', 'isa.ferreira@example.com', 'isabela789', 'Rua Dom Pedro, 234', '(16) 99543-8877', NULL, NULL, 'Cliente'),
(21, 'João Vitor Nunes', 'joao.nunes@example.com', 'joaovitor', 'Rua Primavera, 12', '(13) 99223-4411', NULL, NULL, 'Cliente'),
(22, 'Marcelo Andrade', 'marcelo.andrade@logistica.com', 'marcelo2025', NULL, NULL, '12345678901', 'C', 'Motorista'),
(23, 'Paulo Ricardo Silva', 'paulo.silva@logistica.com', 'paulo123', NULL, NULL, '23456789012', 'D', 'Motorista'),
(24, 'Roberto Teixeira', 'roberto.teixeira@logistica.com', 'roberto321', NULL, NULL, '34567890123', 'E', 'Motorista'),
(25, 'Ricardo Fernandes', 'ricardo.fernandes@logistica.com', 'rfernandes45', NULL, NULL, '45678901234', 'C', 'Motorista'),
(26, 'André Santos', 'andre.santos@logistica.com', 'andre2025', NULL, NULL, '56789012345', 'D', 'Motorista'),
(27, 'Felipe Moreira', 'felipe.moreira@logistica.com', 'fmoreira77', NULL, NULL, '67890123456', 'E', 'Motorista'),
(28, 'Hugo Carvalho', 'hugo.carvalho@logistica.com', 'hcarvalho88', NULL, NULL, '78901234567', 'C', 'Motorista'),
(29, 'Matheus Vieira', 'matheus.vieira@logistica.com', 'mvieira90', NULL, NULL, '89012345678', 'D', 'Motorista'),
(30, 'João Pedro Ramos', 'joaop.ramos@logistica.com', 'jpramos55', NULL, NULL, '90123456789', 'E', 'Motorista'),
(31, 'Lucas Almeida', 'lucas.almeida@logistica.com', 'lalmeida12', NULL, NULL, '01234567890', 'C', 'Motorista');

-- --------------------------------------------------------

--
-- Table structure for table `veiculos`
--

CREATE TABLE `veiculos` (
  `Id` int(11) NOT NULL,
  `Placa` varchar(20) DEFAULT NULL,
  `Marca` varchar(100) DEFAULT NULL,
  `Capacidade` int(11) DEFAULT NULL,
  `Discriminator` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `veiculos`
--

INSERT INTO `veiculos` (`Id`, `Placa`, `Marca`, `Capacidade`, `Discriminator`) VALUES
(1, 'ABC1A23', 'Mercedes-Benz Accelo 915', 3000, 'Caminhao'),
(2, 'DEF2B34', 'Volkswagen Delivery 9.170', 3500, 'Caminhao'),
(3, 'GHI3C45', 'Iveco Daily 35-150', 1500, 'Van'),
(4, 'JKL4D56', 'Fiat Fiorino', 650, 'Utilitario'),
(5, 'MNO5E67', 'Renault Master', 1200, 'Van'),
(6, 'PQR6F78', 'Hyundai HR', 1800, 'Caminhao'),
(7, 'STU7G89', 'Ford Transit', 1400, 'Van'),
(8, 'VWX8H90', 'Peugeot Boxer', 1300, 'Van'),
(9, 'YZA9J01', 'Chevrolet S10', 800, 'Pickup'),
(10, 'BCD0K12', 'Toyota Hilux', 900, 'Pickup');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `rotas`
--
ALTER TABLE `rotas`
  ADD PRIMARY KEY (`Id`);

--
-- Indexes for table `usuarios`
--
ALTER TABLE `usuarios`
  ADD PRIMARY KEY (`Id`);

--
-- Indexes for table `veiculos`
--
ALTER TABLE `veiculos`
  ADD PRIMARY KEY (`Id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `rotas`
--
ALTER TABLE `rotas`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- AUTO_INCREMENT for table `usuarios`
--
ALTER TABLE `usuarios`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=32;

--
-- AUTO_INCREMENT for table `veiculos`
--
ALTER TABLE `veiculos`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
