<?php
// Conexión a la base de datos
$servername = "localhost";
$username = "root"; // Cambiar por tu nombre de usuario de MySQL
$password = ""; // Cambiar por tu contraseña de MySQL
$database = "servidor";

$conn = new mysqli($servername, $username, $password, $database);

// Verificar la conexión
if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
}

// Obtener datos de Unity
$usuario = $_POST['usuario'];
$contrasena = $_POST['contrasena'];

// Comprobar si el usuario ya existe en la base de datos
$sql_check = "SELECT * FROM usuarios WHERE usuario='$usuario'";
$result_check = $conn->query($sql_check);

if ($result_check->num_rows > 0) {
    // Si el usuario ya existe, devolvemos un mensaje de error
    echo "exist";
} else {
    // Si el usuario no existe, lo insertamos en la base de datos
    $sql_insert = "INSERT INTO usuarios (usuario, contrasena) VALUES ('$usuario', '$contrasena')";
    if ($conn->query($sql_insert) === TRUE) {
        echo "success"; // Registro exitoso
    } else {
        echo "error"; // Error al registrar
    }
}

$conn->close();
?>
