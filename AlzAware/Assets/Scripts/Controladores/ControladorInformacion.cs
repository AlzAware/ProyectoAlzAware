using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorInformacion : MonoBehaviour
{
    // M�todo utilizado para volver al Men� Principal
    public void VolverMenuPrincipal()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }
}
