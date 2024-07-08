using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorMenuPrincipal : MonoBehaviour
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

    // Método utilizado para saltar a la escena de los Ejercicios Diarios
    public void IrAEjerciciosDiarios()
    {
        SceneManager.LoadScene("EjerciciosDiarios");
    }

    // Método utilizado para saltar a la escena Ejercicios
    public void IrAEjercicios()
    {
        SceneManager.LoadScene("Ejercicios");

    }

    // Método utilizado para saltar a la escena de las Estadísticas Globales
    public void IrAEstadisticas()
    {
        SceneManager.LoadScene("EstadisticasGlobales");
        
    }

    // Método utilizado para saltar a la escena de Ajustes
    public void IrAAjustes()
    {
        SceneManager.LoadScene("Ajustes");
    }

    // Método utilizado para saltar a la escena de Actualizar a Pro
    public void IrAActualizarAPro()
    {
        SceneManager.LoadScene("ActualizarAPro");
    }

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

    // Método utilizado para saltar a la escena del Ejercicio Seleccionado
    public void IrAEjercicioSeleccionado()
    {
        SceneManager.LoadScene("EjercicioSeleccionado");
    }

    // Método utilizado para saltar a la escena de las Estadísticas del Ejercicio Seleccionado
    public void IrAEstadisticasEjercicioSeleccionado()
    {
        SceneManager.LoadScene("EstadisticasEjercicioSeleccionado");
    }

    // Método utilizado para saltar a la escena de Ajustes de Cuenta
    public void IrAAjustesCuenta()
    {
        SceneManager.LoadScene("Ajustes_Cuenta");
    }

    // Método utilizado para saltar a la escena de Ajustes Generales
    public void IrAAjustesGeneral()
    {
        SceneManager.LoadScene("Ajustes_General");
    }

    // Método utilizado para saltar a la escena de Quitar Anuncios
    public void IrAAjustesQuitarAnuncios()
    {
        SceneManager.LoadScene("Ajustes_QuitarAnuncios");
    }

    // Método utilizado para saltar a la escena de Ajustes de Sonido
    public void IrAAjustesSonido()
    {
        SceneManager.LoadScene("Ajustes_Sonido");
    }
}
