using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorVolver : MonoBehaviour
{
    // M�todo utilizado para volver a Ejercicios
    public void VolverAEjercicios()
    {
        SceneManager.LoadScene("Ejercicios");
    }
}
