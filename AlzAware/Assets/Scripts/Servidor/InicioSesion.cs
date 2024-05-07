using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class InicioSesion : MonoBehaviour
{
    // Declaraci�n de variables
    [SerializeField] GameObject panelBienvenida;
    [SerializeField] GameObject panelLogin;
    [SerializeField] TMP_Text usuario;
    [SerializeField] TMP_InputField nombreUsuario;
    [SerializeField] TMP_InputField contrasena;
    [SerializeField] TMP_Text mensajesError;
    [SerializeField] GameObject imagenLogin;
    [SerializeField] Button botonLogIn;
    [SerializeField] string url;

    // M�todo que se llama cuando pulsamos el bot�n de LogIn
    public void OnLoginButtonClicked()
    {
        // Cuando pulsamos el bot�n de LogIn, se muestra la imagen de carga mientras est� comprobando la informaci�n y desactivamos el bot�n para no pulsarlo otra vez
        botonLogIn.interactable = false;
        imagenLogin.SetActive(true);

        // Iniciamos la Corrutina para iniciar sesi�n
        StartCoroutine(Login());
    }

    // Rutina a la que llamaremos cuando queramos iniciar sesi�n
    IEnumerator Login()
    {
        WWWForm form = new WWWForm();

        // A�adimos al WWWForm el usuario y contrase�a que le hemos pasado mediante los InputFields
        form.AddField("usuario", nombreUsuario.text);
        form.AddField("contrasena", contrasena.text);

        using (WWW w = new WWW(url, form))
        {
            yield return w;

            if (w.error != null)
            {
                // La conexi�n ha fallado
                mensajesError.text = "Error de conexi�n: " + w.error;
            }
            else
            {
                // La conexi�n se ha realizado correctamente
                if (w.text == "success")
                {
                    // El usuario existe en la base de datos y la contrase�a es correcta
                    // Desactivamos el panel de Login, y pasamos al panel de bienvenida, mostrando el nombre del usuario que ha hecho LogIn
                    panelLogin.SetActive(false);
                    panelBienvenida.SetActive(true);
                    usuario.text = nombreUsuario.text;

                    // Iniciamos la Corrutina para pasar a la escena del Men� Principal
                    StartCoroutine(CambiarEscenaDespuesDeEspera());
                }
                else if (w.text == "error")
                {
                    // El usuario o la contrase�a no coinciden en la base de datos
                    mensajesError.text = "Usuario o contrase�a inv�lidos";
                }
                else
                {
                    // Otro tipo de error inesperado
                    mensajesError.text = "Error desconocido";
                }
            }
        }

        // Volvemos a activar el bot�n de Login y desactivamos la imagen de carga
        botonLogIn.interactable = true;
        imagenLogin.SetActive(false);
    }

    // Rutina que utilizamos para saltar a siguiente escena
    IEnumerator CambiarEscenaDespuesDeEspera()
    {
        // Esperamos 3 segundos antes de ir a la escena del Men� Principal
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("MenuPrincipal");
    }
}
