using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorCuentas : MonoBehaviour
{
    // M�todo utilizado para saltar a la escena de Creaci�n de Cuenta
    public void IrACrearCuenta()
    {
        SceneManager.LoadScene("CrearCuenta");
    }

    // M�todo utilizado para saltar a la escena de Iniciar Sesi�n
    public void IrALogin()
    {
        SceneManager.LoadScene("Login");
    }

    // M�todo utilizado para saltar a la escena del Men� Principal
    public void IrAMenuPrincipal()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }
}
