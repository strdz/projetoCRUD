-- phpMyAdmin SQL Dump
-- version 5.1.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Tempo de geração: 30-Jan-2022 às 19:28
-- Versão do servidor: 10.4.22-MariaDB
-- versão do PHP: 7.4.26

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Banco de dados: `dbprojeto`
--

-- --------------------------------------------------------

--
-- Estrutura da tabela `produtos`
--

CREATE TABLE `produtos` (
  `ID` int(255) NOT NULL,
  `Codigo` varchar(200) NOT NULL,
  `Descricao` varchar(300) NOT NULL,
  `ID_DEPARTAMENTO` int(50) NOT NULL,
  `PRECO` int(255) NOT NULL,
  `STATUS` varchar(1) NOT NULL,
  `ID_USUARIO` int(11) NOT NULL,
  `ID_USUARIO_ALTERACAO` int(55) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COMMENT='MaximusTech';

--
-- Extraindo dados da tabela `produtos`
--

INSERT INTO `produtos` (`ID`, `Codigo`, `Descricao`, `ID_DEPARTAMENTO`, `PRECO`, `STATUS`, `ID_USUARIO`, `ID_USUARIO_ALTERACAO`) VALUES
(1, '321', 'Produto Primeiro', 4, 7, 'A', 1, 2),
(2, '123', 'Produto Segundo', 5, 2, 'N', 1, 0),
(3, '565', 'Aditivo Flex Motor', 9, 25, 'A', 2, 0),
(4, '3356', 'Teste Insercao', 2, 50, 'A', 1, 0);

-- --------------------------------------------------------

--
-- Estrutura da tabela `usuarios`
--

CREATE TABLE `usuarios` (
  `id` int(11) NOT NULL,
  `Login` varchar(50) NOT NULL,
  `Senha` varchar(130) NOT NULL,
  `Nome` varchar(100) NOT NULL,
  `CPF` int(11) NOT NULL,
  `TP_USUARIO` varchar(1) NOT NULL,
  `EMAIL` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COMMENT='Armazena dados do Usuario';

--
-- Extraindo dados da tabela `usuarios`
--

INSERT INTO `usuarios` (`id`, `Login`, `Senha`, `Nome`, `CPF`, `TP_USUARIO`, `EMAIL`) VALUES
(1, 'marlon.vgs@outlook.com', 'AGpb8Y09y7tFTlU40Nh7IcOhFsPOI5W6GCyEFRPl5MxEg3h4hu06dhm7ywn46WPxnw==', 'Marlon Vinicius', 2147483647, 'A', 'marlon.vgs@outlook.com'),
(2, 'celia', 'AENOGVFcAIgr/2fN6F1+cP3echchRiyP09jWPVdRYWA6T+RYDqGCI1VBGKkm3mQu6g==', 'Celia Goncalves', 0, '', 'celia.goncalves35@hotmail.com');

--
-- Índices para tabelas despejadas
--

--
-- Índices para tabela `produtos`
--
ALTER TABLE `produtos`
  ADD PRIMARY KEY (`ID`);

--
-- Índices para tabela `usuarios`
--
ALTER TABLE `usuarios`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT de tabelas despejadas
--

--
-- AUTO_INCREMENT de tabela `produtos`
--
ALTER TABLE `produtos`
  MODIFY `ID` int(255) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT de tabela `usuarios`
--
ALTER TABLE `usuarios`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
