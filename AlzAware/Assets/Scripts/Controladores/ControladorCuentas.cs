using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorCuentas : MonoBehaviour
{
    // Método utilizado para saltar a la escena de Creación de Cuenta
    public void IrACrearCuenta()
    {
        SceneManager.LoadScene("CrearCuenta");
    }

    // Método utilizado para saltar a la escena de Iniciar Sesión
    public void IrALogin()
    {
        SceneManager.LoadScene("Login");
    }

    // Método utilizado para saltar a la escena del Menú Principal
    public void IrAMenuPrincipal()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }
}
