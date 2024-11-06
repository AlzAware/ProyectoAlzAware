using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorEjercicios : MonoBehaviour
{
    
    public int escenasSopaLetras;
    public int escenasBuscaObjetos;
    public int escenasRefranes;
    public int escenasFormasGeometricas;

    public void CargarSopaDeLetrasAleatoria()
    {
        // Seleccionar un índice aleatorio
         int aleatorioEscena = UnityEngine.Random.Range(1, escenasSopaLetras+1);

        switch (aleatorioEscena)
        {
            case 1:
                SceneManager.LoadScene("SopaLetras");
                break;
            case 2:
                SceneManager.LoadScene("SopaLetras1");
                break;
            case 3:
                SceneManager.LoadScene("SopaLetras2");
                break;
            case 4:
                SceneManager.LoadScene("SopaLetras3");
                break;
            case 5:
                SceneManager.LoadScene("SopaLetras4");
                break;
            case 6:
                SceneManager.LoadScene("SopaLetras5");
                break;
            case 7:
                SceneManager.LoadScene("SopaLetras6");
                break;
            case 8:
                SceneManager.LoadScene("SopaLetras7");
                break;
            case 9:
                SceneManager.LoadScene("SopaLetras8");
                break;
            case 10:
                SceneManager.LoadScene("SopaLetras9");
                break;
            default:
                Debug.LogError("No hay escenas en la lista para cargar.");
                break;

        }
       
    }

    public void CargarBuscaObjetosAleatorio()
    {
        // Seleccionar un numero aleatorio
        int aleatorioEscena = UnityEngine.Random.Range(1, escenasBuscaObjetos+1);

        // Segun el numero seleccionado entrara en una escena u otra
        switch (aleatorioEscena)
        {
            case 1:
                SceneManager.LoadScene("BuscaObjetos1");
                break;
            case 2:
                SceneManager.LoadScene("BuscaObjetos2");
                break;
            case 3:
                SceneManager.LoadScene("BuscaObjetos3");
                break;
            case 4:
                SceneManager.LoadScene("BuscaObjetos4");
                break;
            default:
                Debug.LogError("No hay escenas en la lista para cargar.");
                break;

        }

    }

    public void CargarFormasGeometricasAleatorio()
    {
        // Seleccionar un numero aleatorio
        int aleatorioEscena = UnityEngine.Random.Range(1, escenasFormasGeometricas + 1);

        // Segun el numero seleccionado entrara en una escena u otra
        switch (aleatorioEscena)
        {
            case 1:
                SceneManager.LoadScene("FormaGeometrica");
                break;
            case 2:
                SceneManager.LoadScene("FormaGeometrica1");
                break;
            case 3:
                SceneManager.LoadScene("FormaGeometrica2");
                break;
            case 4:
                SceneManager.LoadScene("FormaGeometrica3");
                break;
            case 5:
                SceneManager.LoadScene("FormaGeometrica4");
                break;
            default:
                Debug.LogError("No hay escenas en la lista para cargar.");
                break;

        }

    }

    public void CargarRefranesAleatorio()
    {
        // Seleccionar un índice aleatorio
        int aleatorioEscena = UnityEngine.Random.Range(1, escenasRefranes + 1);

        switch (aleatorioEscena)
        {
            case 1:
                SceneManager.LoadScene("Refran1");
                break;
            case 2:
                SceneManager.LoadScene("Refran2");
                break;
            case 3:
                SceneManager.LoadScene("Refran3");
                break;
            case 4:
                SceneManager.LoadScene("Refran4");
                break;
            case 5:
                SceneManager.LoadScene("Refran5");
                break;
            case 6:
                SceneManager.LoadScene("Refran6");
                break;
            case 7:
                SceneManager.LoadScene("Refran7");
                break;
            
            default:
                Debug.LogError("No hay escenas en la lista para cargar.");
                break;

        }
    }

    public void CargarJuegoParejas()
    {
        SceneManager.LoadScene("JuegoParejas");
    }

    public void CargarPuzzle()
    {
        SceneManager.LoadScene("Puzzle");
    }

    public void VolverAlMenu()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }
}
