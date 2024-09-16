using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorSplash : MonoBehaviour
{
    void Start()
    {
        // Llama a la funci�n para cambiar la escena despu�s de 8 segundos
        StartCoroutine(CambiarEscenaDespuesDeEspera());
    }


    IEnumerator CambiarEscenaDespuesDeEspera()
    {
        // Espera 3 segundos
        yield return new WaitForSeconds(8);

        // Cambia a la escena "Login"
        SceneManager.LoadScene("Login");
    }
}
