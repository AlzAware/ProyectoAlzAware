using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivarPopUp : MonoBehaviour
{
    // Referencia al popup
    public GameObject popup;

    // Start is called before the first frame update
    void Start()
    {
        // Asegúrate de que el popup esté inactivo al inicio
        popup.SetActive(false);
    }

    // Método que se llama cuando el ratón entra en el área del objeto
    public void OnPointerEnter()
    {
        popup.SetActive(true);
    }

    // Método que se llama cuando el ratón sale del área del objeto
    public void OnPointerExit()
    {
        popup.SetActive(false);
    }
}


