using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorMenuPrincipal : MonoBehaviour
{
    public void IrAMenuPrincipal()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }

    public void IrAEjerciciosDiarios()
    {
        SceneManager.LoadScene("EjerciciosDiarios");
    }

    public void IrAEjercicios()
    {
        SceneManager.LoadScene("Ejercicios");
    }

    public void IrAEstadisticas()
    {
        SceneManager.LoadScene("EstadisticasGlobales");
    }

    public void IrAAjustes()
    {
        SceneManager.LoadScene("Ajustes");
    }

    public void IrAActualizarAPro()
    {
        SceneManager.LoadScene("ActualizarAPro");
    }

    public void IrAEjercicioAleatorio1()
    {
        SceneManager.LoadScene("EjercicioAleatorio_1");
    }

    public void IrAEjercicioAleatorio2()
    {
        SceneManager.LoadScene("EjercicioAleatorio_2");
    }

    public void IrAEjercicioAleatorio3()
    {
        SceneManager.LoadScene("EjercicioAleatorio_3");
    }

    public void IrAEstadisticasDiarias()
    {
        SceneManager.LoadScene("EstadisticasDiarias");
    }

    public void IrAEjercicioSeleccionado()
    {
        SceneManager.LoadScene("EjercicioSeleccionado");
    }

    public void IrAEstadisticasEjercicioSeleccionado()
    {
        SceneManager.LoadScene("EstadisticasEjercicioSeleccionado");
    }

    public void IrAAjustesCuenta()
    {
        SceneManager.LoadScene("Ajustes_Cuenta");
    }

    public void IrAAjustesGeneral()
    {
        SceneManager.LoadScene("Ajustes_General");
    }

    public void IrAAjustesQuitarAnuncios()
    {
        SceneManager.LoadScene("Ajustes_QuitarAnuncios");
    }

    public void IrAAjustesSonido()
    {
        SceneManager.LoadScene("Ajustes_Sonido");
    }
}
