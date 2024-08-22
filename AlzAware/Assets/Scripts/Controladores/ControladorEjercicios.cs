using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorEjercicios : MonoBehaviour
{
    // Lista de referencias a las escenas de la Sopa de Letras y Busca Objetos
    public SceneAsset[] escenasSopaDeLetras;
    public SceneAsset[] escenasBuscaObjetos;

    public void CargarSopaDeLetrasAleatoria()
    {
        if (escenasSopaDeLetras.Length == 0)
        {
            Debug.LogError("No hay escenas en la lista para cargar.");
            return;
        }

        // Seleccionar un índice aleatorio
        int aleatorio = Random.Range(0, escenasSopaDeLetras.Length);

        // Obtener el nombre de la escena seleccionada aleatoriamente
        string escena = escenasSopaDeLetras[aleatorio].name;

        // Cargar la escena seleccionada aleatoriamente
        SceneManager.LoadScene(escena);
    }

    public void CargarBuscaObjetosAleatorio()
    {
        if (escenasBuscaObjetos.Length == 0)
        {
            Debug.LogError("No hay escenas en la lista para cargar.");
            return;
        }

        // Seleccionar un índice aleatorio
        int aleatorio = Random.Range(0, escenasBuscaObjetos.Length);

        // Obtener el nombre de la escena seleccionada aleatoriamente
        string escena = escenasBuscaObjetos[aleatorio].name;

        // Cargar la escena seleccionada aleatoriamente
        SceneManager.LoadScene(escena);
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
