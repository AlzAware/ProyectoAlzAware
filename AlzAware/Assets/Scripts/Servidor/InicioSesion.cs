using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class InicioSesion : MonoBehaviour
{
    // Declaración de variables
    [SerializeField] GameObject panelBienvenida;
    [SerializeField] GameObject panelLogin;
    [SerializeField] TMP_Text usuario;
    [SerializeField] TMP_InputField nombreUsuario;
    [SerializeField] TMP_InputField contrasena;
    [SerializeField] TMP_Text mensajesError;
    [SerializeField] GameObject imagenLogin;
    [SerializeField] Button botonLogIn;
    [SerializeField] string url;

    // Método que se llama cuando pulsamos el botón de LogIn
    public void OnLoginButtonClicked()
    {
        // Cuando pulsamos el botón de LogIn, se muestra la imagen de carga mientras está comprobando la información y desactivamos el botón para no pulsarlo otra vez
        botonLogIn.interactable = false;
        imagenLogin.SetActive(true);

        // Iniciamos la Corrutina para iniciar sesión
        StartCoroutine(Login());
    }

    // Rutina a la que llamaremos cuando queramos iniciar sesión
    IEnumerator Login()
    {
        WWWForm form = new WWWForm();

        // Añadimos al WWWForm el usuario y contraseña que le hemos pasado mediante los InputFields
        form.AddField("usuario", nombreUsuario.text);
        form.AddField("contrasena", contrasena.text);

        using (WWW w = new WWW(url, form))
        {
            yield return w;

            if (w.error != null)
            {
                // La conexión ha fallado
                mensajesError.text = "Error de conexión: " + w.error;
            }
            else
            {
                // La conexión se ha realizado correctamente
                if (w.text == "success")
                {
                    // El usuario existe en la base de datos y la contraseña es correcta
                    // Desactivamos el panel de Login, y pasamos al panel de bienvenida, mostrando el nombre del usuario que ha hecho LogIn
                    panelLogin.SetActive(false);
                    panelBienvenida.SetActive(true);
                    usuario.text = nombreUsuario.text;

                    // Iniciamos la Corrutina para pasar a la escena del Menú Principal
                    StartCoroutine(CambiarEscenaDespuesDeEspera());
                }
                else if (w.text == "error")
                {
                    // El usuario o la contraseña no coinciden en la base de datos
                    mensajesError.text = "Usuario o contraseña inválidos";
                }
                else
                {
                    // Otro tipo de error inesperado
                    mensajesError.text = "Error desconocido";
                }
            }
        }

        // Volvemos a activar el botón de Login y desactivamos la imagen de carga
        botonLogIn.interactable = true;
        imagenLogin.SetActive(false);
    }

    // Rutina que utilizamos para saltar a siguiente escena
    IEnumerator CambiarEscenaDespuesDeEspera()
    {
        // Esperamos 3 segundos antes de ir a la escena del Menú Principal
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("MenuPrincipal");
    }
}
