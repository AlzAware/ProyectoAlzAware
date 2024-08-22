using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorEjerciciosDiarios : MonoBehaviour
{
    // M�todo utilizado para saltar a la escena del Ejercicio Aleatorio 1
    public void IrAEjercicioAleatorio1()
    {
        SceneManager.LoadScene("EjercicioAleatorio_1");
    }

    // M�todo utilizado para saltar a la escena del Ejercicio Aleatorio 2
    public void IrAEjercicioAleatorio2()
    {
        SceneManager.LoadScene("EjercicioAleatorio_2");
    }

    // M�todo utilizado para saltar a la escena del Ejercicio Aleatorio 3
    public void IrAEjercicioAleatorio3()
    {
        SceneManager.LoadScene("EjercicioAleatorio_3");
    }

    // M�todo utilizado para saltar a la escena de las Estad�sticas Diarias
    public void IrAEstadisticasDiarias()
    {
        SceneManager.LoadScene("EstadisticasDiarias");
    }

    // M�todo utilizado para volver al Men� Principal
    public void VolverMenuPrincipal()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }
}
