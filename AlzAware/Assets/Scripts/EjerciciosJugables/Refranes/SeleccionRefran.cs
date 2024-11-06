using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SeleccionRefran : MonoBehaviour
{
    // Imagen correcta
    public Sprite correctImage;
    // Imagen incorrecta
    public Sprite incorrectImage;
    // Indica si el objeto es correcto o no
    public bool isCorrectObject;
    //Imagen de las opciones
    private Image opcionObject;

    // Esta variable se usar� para verificar la selecci�n de objetos correctos
    public static int correctObjectsCount = 1;


    void Start()
    {
        // Obtener el componente Image del objeto
        opcionObject = GetComponent<Image>();
        correctObjectsCount = 0;


    }

    // M�todo que se llama cuando se hace clic en el objeto
    public void OnObjectClick()
    {
        if (isCorrectObject)
        {
            // Cambiar a la imagen correcta
            opcionObject.sprite = correctImage;

            correctObjectsCount++;


            if (correctObjectsCount >= 1)
            {

                // Cambiar a la otra escena pasado 2seg
                Invoke("cambiarEscena", 1.0f);
            }

        }
        else
        {
            // Cambiar a la imagen incorrecta
            opcionObject.sprite = incorrectImage;

        }
    }

    //Metodo para pasar a la escena indicada
    public void cambiarEscena()
    {
        SceneManager.LoadScene("EstadisticasEjercicioSeleccionado");
    }
}