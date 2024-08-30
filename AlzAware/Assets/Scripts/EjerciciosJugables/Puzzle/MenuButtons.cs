using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{
    // El método Awake se llama al inicio del ciclo de vida del script, antes de que el juego comience a ejecutarse.
    private void Awake()
    {
        // Verifica si el juego no se está ejecutando en el Editor de Unity.
        if(Application.isEditor == false)
        {
            // Si el juego no está en el Editor, desactiva el registro de logs de Unity para evitar que se muestren en la consola.
            Debug.unityLogger.logEnabled = false;
        }
    }

    // Método público para cargar una escena específica.
    // Se puede llamar a este método desde otros scripts o componentes de UI, como botones.
    public void LoadScene(string name)
    {
        // Utiliza el SceneManager para cargar la escena cuyo nombre se pasa como parámetro.
        SceneManager.LoadScene(name);
    }
}
