using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorSplash : MonoBehaviour
{
    private GameObject logo;
    private Animator logoAnimator;

    void Start()
    {
        logo = GameObject.Find("Logo");
        logoAnimator = logo.GetComponent<Animator>();

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
