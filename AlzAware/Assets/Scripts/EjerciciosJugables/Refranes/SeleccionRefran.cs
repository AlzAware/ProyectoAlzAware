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

    // Esta variable se usará para verificar la selección de objetos correctos
    public static int correctObjectsCount = 1;

    private Contador contador;

    void Start()
    {
        // Obtener el componente Image del objeto
        opcionObject = GetComponent<Image>();
        correctObjectsCount = 0;
        if (contador == null)
        {
            contador = FindObjectOfType<Contador>();
        }

    }

    // Método que se llama cuando se hace clic en el objeto
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

            // Reiniciar el temporizador según el ejercicio actual
            if (contador != null)
            {
                contador.RestartTimer();
            }
        }
    }

    //Metodo para pasar a la escena indicada
    public void cambiarEscena()
    {
        SceneManager.LoadScene("EstadisticasEjercicioSeleccionado");
    }
}