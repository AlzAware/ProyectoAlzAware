<?php
// Conexión a la base de datos
$servername = "localhost";
$username = "root";
$password = "";
$database = "servidor";

$conn = new mysqli($servername, $username, $password, $database);

// Verificar la conexión
if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
}

// Obtener datos de Unity
$usuario = $_POST['usuario'];
$contrasena = $_POST['contrasena'];

// Consulta para comprobar si el usuario y la contraseña existen
$sql = "SELECT * FROM usuarios WHERE (usuario='$usuario' OR correoElectronico='$usuario') AND contrasena='$contrasena'";
$result = $conn->query($sql);

if ($result->num_rows > 0) {
    echo "success"; // Usuario y contraseña válidos
} else {
    echo "error"; // Usuario o contraseña inválidos
}

$conn->close();
?>
