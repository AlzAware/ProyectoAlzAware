using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ControladorAjustes : MonoBehaviour
{
    [Header("Paneles y Botones")]
    public GameObject panelCrearCuenta; // Panel de Crear Cuenta
    public GameObject panelCambiarPass; // Panel de Cambiar Contrase�a
    public GameObject panelEliminarCuenta; // Panel de Eliminar Cuenta
    public GameObject botonCrearCuenta; // Bot�n de Crear Cuenta
    public GameObject botonCambiarPass; // Bot�n de Cambiar Contrase�a
    public GameObject botonEliminarCuenta; // Bot�n de Eliminar Cuenta
    public GameObject botonVolver; // Bot�n de Volver

    [Header("Campos de Crear Cuenta")]
    public TMP_InputField inputNombreUsuario; // Campo de texto para nombre de usuario
    public TMP_InputField inputCorreoElectronico; // Campo de texto para correo electr�nico
    public TMP_InputField inputContrasena; // Campo de texto para contrase�a
    public TMP_Text mensajeError; // Texto para mostrar mensajes de error

    [Header("Campos de Cambiar Contrase�a")]
    public TMP_InputField inputNombreUsuarioCambiarPass; // Campo de texto para el nombre de usuario
    public TMP_InputField inputContrasenaCambiarPass; // Campo de texto para la nueva contrase�a
    public TMP_Text mensajeErrorCambiarPass; // Texto para mostrar mensajes de error o �xito

    [Header("Campos de Eliminar Cuenta")]
    public TMP_InputField inputEliminarUsuario; // Campo de texto para el nombre de usuario
    public TMP_InputField inputEliminarContrasena; // Campo de texto para la contrase�a
    public TMP_Text mensajeErrorEliminarCuenta; // Texto para mostrar mensajes de error o �xito

    [Header("SQLiteManager")]
    public SQLiteManager databaseManager;

    [Header("Configuraci�n")]
    public string escenaLogin; // Nombre de la escena de login
    public float tiempoEspera = 2f; // Tiempo de espera tras el registro

    /// <summary>
    /// M�todo para mostrar el panel Crear Cuenta y ocultar los botones.
    /// </summary>
    public void ActivarCrearCuenta()
    {
        panelCrearCuenta.SetActive(true);
        botonCrearCuenta.SetActive(false);
        botonCambiarPass.SetActive(false);
        botonEliminarCuenta.SetActive(false);
        botonVolver.SetActive(false);
    }

    /// <summary>
    /// M�todo para mostrar el panel Cambiar Contrase�a y ocultar los botones.
    /// </summary>
    public void ActivarCambiarPass()
    {
        panelCambiarPass.SetActive(true);
        botonCrearCuenta.SetActive(false);
        botonCambiarPass.SetActive(false);
        botonEliminarCuenta.SetActive(false);
        botonVolver.SetActive(false);
        LimpiarCamposYMensajes();
    }

    /// <summary>
    /// M�todo para mostrar el panel Eliminar Cuenta y ocultar los botones.
    /// </summary>
    public void ActivarEliminarCuenta()
    {
        panelEliminarCuenta.SetActive(true);
        botonCrearCuenta.SetActive(false);
        botonCambiarPass.SetActive(false);
        botonEliminarCuenta.SetActive(false);
        botonVolver.SetActive(false);
        LimpiarCamposYMensajes();
    }

    /// <summary>
    /// M�todo para desactivar todos los paneles y activar todos los botones.
    /// </summary>
    public void VolverDesdePaneles()
    {
        panelCrearCuenta.SetActive(false);
        panelCambiarPass.SetActive(false);
        panelEliminarCuenta.SetActive(false);
        botonCrearCuenta.SetActive(true);
        botonCambiarPass.SetActive(true);
        botonEliminarCuenta.SetActive(true);
        botonVolver.SetActive(true);
    }

    /// <summary>
    /// M�todo llamado al presionar el bot�n "Crear Cuenta".
    /// </summary>
    public void CrearCuenta()
    {
        if (string.IsNullOrEmpty(inputNombreUsuario.text) ||
            string.IsNullOrEmpty(inputCorreoElectronico.text) ||
            string.IsNullOrEmpty(inputContrasena.text))
        {
            MostrarMensajeError(mensajeError, "Todos los campos son obligatorios.");
            return;
        }

        try
        {
            databaseManager.InsertUsuario(inputNombreUsuario.text, inputContrasena.text, inputCorreoElectronico.text);
            StartCoroutine(MostrarBienvenidaYCargarLogin());
        }
        catch (System.Exception ex)
        {
            MostrarMensajeError(mensajeError, $"Error al registrar usuario: {ex.Message}");
        }
    }

    /// <summary>
    /// M�todo llamado al presionar el bot�n "Cambiar Contrase�a".
    /// </summary>
    public void CambiarContrasena()
    {
        if (string.IsNullOrEmpty(inputNombreUsuarioCambiarPass.text) || string.IsNullOrEmpty(inputContrasenaCambiarPass.text))
        {
            MostrarMensajeError(mensajeErrorCambiarPass, "Todos los campos son obligatorios.");
            return;
        }

        try
        {
            databaseManager.CambiarContrasena(inputNombreUsuarioCambiarPass.text, inputContrasenaCambiarPass.text);
            MostrarMensajeError(mensajeErrorCambiarPass, "Contrase�a cambiada con �xito.", true);
            VolverDesdePaneles();
        }
        catch (System.Exception ex)
        {
            MostrarMensajeError(mensajeErrorCambiarPass, $"Error: {ex.Message}");
        }
    }

    /// <summary>
    /// M�todo llamado al presionar el bot�n "Eliminar Cuenta".
    /// </summary>
    public void EliminarCuenta()
    {
        if (string.IsNullOrEmpty(inputEliminarUsuario.text) || string.IsNullOrEmpty(inputEliminarContrasena.text))
        {
            MostrarMensajeError(mensajeErrorEliminarCuenta, "Todos los campos son obligatorios.");
            return;
        }

        bool eliminado = databaseManager.EliminarUsuario(inputEliminarUsuario.text, inputEliminarContrasena.text);
        if (eliminado)
        {
            MostrarMensajeError(mensajeErrorEliminarCuenta, "Usuario eliminado con �xito.", true);
            StartCoroutine(UsuarioEliminadoYCargarLogin());
        }
        else
        {
            MostrarMensajeError(mensajeErrorEliminarCuenta, "Usuario no encontrado o contrase�a incorrecta.");
        }
    }

    /// <summary>
    /// Limpia los campos de texto y los mensajes.
    /// </summary>
    private void LimpiarCamposYMensajes()
    {
        inputNombreUsuarioCambiarPass.text = "";
        inputContrasenaCambiarPass.text = "";
        inputEliminarUsuario.text = "";
        inputEliminarContrasena.text = "";
        mensajeError.text = "";
        mensajeErrorCambiarPass.text = "";
        mensajeErrorEliminarCuenta.text = "";
    }

    /// <summary>
    /// Muestra un mensaje de error o �xito en el panel correspondiente.
    /// </summary>
    private void MostrarMensajeError(TMP_Text mensajeText, string mensaje, bool esExito = false)
    {
        if (mensajeText != null)
        {
            mensajeText.text = mensaje;
            mensajeText.color = esExito ? Color.green : Color.red;
        }
    }

    /// <summary>
    /// Muestra una breve confirmaci�n y carga la escena de login.
    /// </summary>
    private IEnumerator MostrarBienvenidaYCargarLogin()
    {
        if (mensajeError != null)
        {
            mensajeError.text = "Usuario registrado con �xito.";
        }

        yield return new WaitForSeconds(tiempoEspera);

        SceneManager.LoadScene(escenaLogin);
    }

    private IEnumerator UsuarioEliminadoYCargarLogin()
    {
        if (mensajeError != null)
        {
            mensajeError.text = "Usuario eliminado con �xito.";
        }

        yield return new WaitForSeconds(tiempoEspera);

        SceneManager.LoadScene(escenaLogin);
    }

    public void VolverAMenuPrincipal()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }
}
