using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorEjerciciosDiarios : MonoBehaviour
{
    // Método utilizado para saltar a la escena del Ejercicio Aleatorio 1
    public void IrAEjercicioAleatorio1()
    {
        SceneManager.LoadScene("EjercicioAleatorio_1");
    }

    // Método utilizado para saltar a la escena del Ejercicio Aleatorio 2
    public void IrAEjercicioAleatorio2()
    {
        SceneManager.LoadScene("EjercicioAleatorio_2");
    }

    // Método utilizado para saltar a la escena del Ejercicio Aleatorio 3
    public void IrAEjercicioAleatorio3()
    {
        SceneManager.LoadScene("EjercicioAleatorio_3");
    }

    // Método utilizado para saltar a la escena de las Estadísticas Diarias
    public void IrAEstadisticasDiarias()
    {
        SceneManager.LoadScene("EstadisticasDiarias");
    }

    // Método utilizado para volver al Menú Principal
    public void VolverMenuPrincipal()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }
}
