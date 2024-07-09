using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CargarEscenasAleatorias : MonoBehaviour
{

    // Lista de referencias a las escenas
    public SceneAsset[] sceneAssets;

    // Funci�n que se llama cuando se presiona el bot�n
    public void LoadRandom()
    {
        if (sceneAssets.Length == 0)
        {
            Debug.LogError("No hay escenas en la lista para cargar.");
            return;
        }

        // Seleccionar un �ndice aleatorio
        int randomIndex = Random.Range(0, sceneAssets.Length);

        // Obtener el nombre de la escena seleccionada aleatoriamente
        string sceneName = sceneAssets[randomIndex].name;

        // Cargar la escena seleccionada aleatoriamente
        SceneManager.LoadScene(sceneName);
    }
}
