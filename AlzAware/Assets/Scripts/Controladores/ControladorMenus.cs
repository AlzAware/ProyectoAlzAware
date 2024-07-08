using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorMenuPrincipal : MonoBehaviour
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

    // M�todo utilizado para saltar a la escena de los Ejercicios Diarios
    public void IrAEjerciciosDiarios()
    {
        SceneManager.LoadScene("EjerciciosDiarios");
    }

    // M�todo utilizado para saltar a la escena Ejercicios
    public void IrAEjercicios()
    {
        SceneManager.LoadScene("Ejercicios");

    }

    // M�todo utilizado para saltar a la escena de las Estad�sticas Globales
    public void IrAEstadisticas()
    {
        SceneManager.LoadScene("EstadisticasGlobales");
        
    }

    // M�todo utilizado para saltar a la escena de Ajustes
    public void IrAAjustes()
    {
        SceneManager.LoadScene("Ajustes");
    }

    // M�todo utilizado para saltar a la escena de Actualizar a Pro
    public void IrAActualizarAPro()
    {
        SceneManager.LoadScene("ActualizarAPro");
    }

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

    // M�todo utilizado para saltar a la escena del Ejercicio Seleccionado
    public void IrAEjercicioSeleccionado()
    {
        SceneManager.LoadScene("EjercicioSeleccionado");
    }

    // M�todo utilizado para saltar a la escena de las Estad�sticas del Ejercicio Seleccionado
    public void IrAEstadisticasEjercicioSeleccionado()
    {
        SceneManager.LoadScene("EstadisticasEjercicioSeleccionado");
    }

    // M�todo utilizado para saltar a la escena de Ajustes de Cuenta
    public void IrAAjustesCuenta()
    {
        SceneManager.LoadScene("Ajustes_Cuenta");
    }

    // M�todo utilizado para saltar a la escena de Ajustes Generales
    public void IrAAjustesGeneral()
    {
        SceneManager.LoadScene("Ajustes_General");
    }

    // M�todo utilizado para saltar a la escena de Quitar Anuncios
    public void IrAAjustesQuitarAnuncios()
    {
        SceneManager.LoadScene("Ajustes_QuitarAnuncios");
    }

    // M�todo utilizado para saltar a la escena de Ajustes de Sonido
    public void IrAAjustesSonido()
    {
        SceneManager.LoadScene("Ajustes_Sonido");
    }
}
