-- Crear la base de datos llamada servidor
CREATE DATABASE IF NOT EXISTS servidor;

-- Seleccionar la base de datos servidor
USE servidor;

-- Crear la tabla usuarios con cuatro columnas
CREATE TABLE IF NOT EXISTS usuarios (
    id INT AUTO_INCREMENT PRIMARY KEY,
    usuario VARCHAR(50),
    contrasena VARCHAR(50),
    correoElectronico VARCHAR(255)
);

-- Insertar los cinco elementos en la tabla usuarios con correoElectronico
INSERT INTO usuarios (usuario, contrasena, correoElectronico) VALUES
('marcos', '123456', 'marcos@ifp.es'),
('laura', '123456', 'laura@ifp.es'),
('marta', '123456', 'marta@ifp.es'),
('joseluis', '123456', 'joseluis@ifp.es'),
('paula', '123456', 'paula@ifp.es');
