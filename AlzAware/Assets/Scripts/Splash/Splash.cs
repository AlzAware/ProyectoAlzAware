using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Splash : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Llama a la funci�n para cambiar la escena despu�s de 3 segundos
        StartCoroutine(CambiarEscenaDespuesDeEspera());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator CambiarEscenaDespuesDeEspera()
    {
        // Espera 3 segundos
        yield return new WaitForSeconds(3);

        // Cambia a la escena "MenuPrincipal"
        SceneManager.LoadScene("MenuPrincipal");
    }
}
