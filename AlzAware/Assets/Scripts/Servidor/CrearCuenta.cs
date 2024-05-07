using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class CrearCuenta : MonoBehaviour
{
    [SerializeField] GameObject panelBienvenida;
    [SerializeField] GameObject panelCrearCuenta;
    [SerializeField] TMP_InputField nombreUsuario;
    [SerializeField] TMP_InputField contrasena;
    [SerializeField] TMP_Text mensajesError;
    [SerializeField] Button botonRegistrar;
    [SerializeField] string url;

    public void OnRegisterButtonClicked()
    {
        Debug.Log("Registro button clicked");

        // Desactivamos el botón de registro para evitar múltiples clics
        botonRegistrar.interactable = false;

        // Iniciamos la Corrutina para registrar al usuario
        StartCoroutine(Registro());
    }

    IEnumerator Registro()
    {
        WWWForm form = new WWWForm();

        // Añadimos al WWWForm el usuario y contraseña que se han introducido
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
                    // El usuario se ha creado y desactivamos el panel de creación para mostrar el de bienvenida
                    panelCrearCuenta.SetActive(false);
                    panelBienvenida.SetActive(true);
                }
                else if (w.text == "exist")
                {
                    // El usuario ya existe en la base de datos
                    mensajesError.text = "El usuario ya existe";
                }
                else
                {
                    // Otro tipo de error inesperado
                    mensajesError.text = "Error desconocido";
                }
            }
        }

        // Reactivamos el botón de registro
        botonRegistrar.interactable = true;
    }
}
