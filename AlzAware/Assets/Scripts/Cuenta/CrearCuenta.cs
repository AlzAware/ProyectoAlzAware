using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class CrearCuenta : MonoBehaviour
{
    [SerializeField] private SQLiteManager databaseManager; // Referencia al SQLiteManager
    [SerializeField] private TMP_InputField nombreUsuario; // Campo de texto para el nombre de usuario
    [SerializeField] private TMP_InputField contrasena; // Campo de texto para la contraseña
    [SerializeField] private TMP_InputField correo; // Campo de texto para el correo
    [SerializeField] private TMP_Text mensajeError; // Texto para mostrar errores
    [SerializeField] private float tiempoEsperaParaEscena = 3f; // Tiempo de espera antes de cambiar de escena
    [SerializeField] private string siguienteEscena; // Nombre de la escena de Login

    [SerializeField] private GameObject panelBienvenida; // Panel de bienvenida
    [SerializeField] private TMP_Text nombreDeUsuarioBienvenida; // Texto específico dentro del panel

    public void OnRegisterButtonClicked()
    {
        if (string.IsNullOrEmpty(nombreUsuario.text) || string.IsNullOrEmpty(contrasena.text) || string.IsNullOrEmpty(correo.text))
        {
            mensajeError.text = "Todos los campos son obligatorios.";
            return;
        }

        // Intentar registrar al usuario
        try
        {
            databaseManager.InsertUsuario(nombreUsuario.text, contrasena.text, correo.text);

            // Mostrar panel de bienvenida y actualizar texto
            StartCoroutine(MostrarBienvenidaYCambiarEscena());
        }
        catch (System.Exception ex)
        {
            mensajeError.text = "Error al registrar usuario: " + ex.Message;
        }
    }

    private IEnumerator MostrarBienvenidaYCambiarEscena()
    {
        if (panelBienvenida != null && nombreDeUsuarioBienvenida != null)
        {
            // Activar el panel de bienvenida
            panelBienvenida.SetActive(true);

            // Actualizar el texto del objeto NombreDeUsuarioBienvenida
            nombreDeUsuarioBienvenida.text = nombreUsuario.text;
        }

        // Esperar el tiempo configurado
        yield return new WaitForSeconds(tiempoEsperaParaEscena);

        // Cargar la escena de Login
        SceneManager.LoadScene(siguienteEscena);
    }
}
