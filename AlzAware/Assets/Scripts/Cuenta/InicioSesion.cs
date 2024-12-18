using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class InicioSesion : MonoBehaviour
{
    [SerializeField] private SQLiteManager databaseManager;
    [SerializeField] private TMP_InputField nombreUsuario;
    [SerializeField] private TMP_InputField contrasena;
    [SerializeField] private TMP_Text mensajeError;
    [SerializeField] private float tiempoEsperaParaEscena = 3f; // Tiempo de espera antes de cambiar de escena
    [SerializeField] private string siguienteEscena; // Nombre de la escena de Login

    [SerializeField] private GameObject panelBienvenida; // Panel de bienvenida
    [SerializeField] private TMP_Text nombreDeUsuarioBienvenida; // Texto específico dentro del panel

    public void OnLoginButtonClicked()
    {
        if (string.IsNullOrEmpty(nombreUsuario.text) || string.IsNullOrEmpty(contrasena.text))
        {
            mensajeError.text = "Usuario y contraseña son obligatorios.";
            return;
        }

        // Verificar credenciales
        try
        {
            bool loginExitoso = databaseManager.CheckLogin(nombreUsuario.text, contrasena.text);
            if (loginExitoso)
            {
                StartCoroutine(MostrarBienvenidaYCambiarEscena());
            }
            else
            {
                mensajeError.text = "Usuario o contraseña incorrectos.";
            }
        }
        catch (System.Exception ex)
        {
            mensajeError.text = "Error al iniciar sesión: " + ex.Message;
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
        yield return new WaitForSeconds(3f); // Esperar 3 segundos
        SceneManager.LoadScene(siguienteEscena);
    }
}
