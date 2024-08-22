using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorAjustes : MonoBehaviour
{
    // Método utilizado para ir a Ajustes Generales
    public void IrAAjustesGenerales()
    {
        SceneManager.LoadScene("Ajustes_General");
    }

    // Método utilizado para ir a Ajustes de Cuenta
    public void IrAAjustesCuenta()
    {
        SceneManager.LoadScene("Ajustes_Cuenta");
    }

    // Método utilizado para ir a Ajustes de Sonido
    public void IrAAjustesSonido()
    {
        SceneManager.LoadScene("Ajustes_Sonido");
    }

    // Método utilizado para ir a Quitar Anuncios
    public void IrAQuitarAnuncios()
    {
        SceneManager.LoadScene("Ajustes_QuitarAnuncios");
    }

    // Método utilizado para volver al Menú Principal
    public void VolverMenuPrincipal()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }

    // Método utilizado para volver a Ajustes
    public void VolverAAjustes()
    {
        SceneManager.LoadScene("Ajustes");
    }
}
