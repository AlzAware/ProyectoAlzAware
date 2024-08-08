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
        // Aseg�rate de que el popup est� inactivo al inicio
        popup.SetActive(false);
    }

    // M�todo que se llama cuando el rat�n entra en el �rea del objeto
    public void OnPointerEnter()
    {
        popup.SetActive(true);
    }

    // M�todo que se llama cuando el rat�n sale del �rea del objeto
    public void OnPointerExit()
    {
        popup.SetActive(false);
    }
}


