using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorAjustes : MonoBehaviour
{
    // M�todo utilizado para ir a Ajustes Generales
    public void IrAAjustesGenerales()
    {
        SceneManager.LoadScene("Ajustes_General");
    }

    // M�todo utilizado para ir a Ajustes de Cuenta
    public void IrAAjustesCuenta()
    {
        SceneManager.LoadScene("Ajustes_Cuenta");
    }

    // M�todo utilizado para ir a Ajustes de Sonido
    public void IrAAjustesSonido()
    {
        SceneManager.LoadScene("Ajustes_Sonido");
    }

    // M�todo utilizado para ir a Quitar Anuncios
    public void IrAQuitarAnuncios()
    {
        SceneManager.LoadScene("Ajustes_QuitarAnuncios");
    }

    // M�todo utilizado para volver al Men� Principal
    public void VolverMenuPrincipal()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }

    // M�todo utilizado para volver a Ajustes
    public void VolverAAjustes()
    {
        SceneManager.LoadScene("Ajustes");
    }
}
