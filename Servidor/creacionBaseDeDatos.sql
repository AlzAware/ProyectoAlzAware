-- Crear la base de datos llamada servidor
CREATE DATABASE IF NOT EXISTS servidor;

-- Seleccionar la base de datos servidor
USE servidor;

-- Crear la tabla usuarios con tres columnas
CREATE TABLE IF NOT EXISTS usuarios (
    id INT AUTO_INCREMENT PRIMARY KEY,
    usuario VARCHAR(50),
    contrasena VARCHAR(50)
);

-- Insertar los cuatro elementos en la tabla usuarios
INSERT INTO usuarios (usuario, contrasena) VALUES
('marcos', '123456'),
('laura', '123456'),
('marta', '123456'),
('joseluis', '123456');