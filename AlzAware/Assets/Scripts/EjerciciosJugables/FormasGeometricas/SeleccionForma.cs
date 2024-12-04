using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SeleccionForma : MonoBehaviour
{
    // Imagen correcta
    public Sprite correctImage;
    // Imagen incorrecta
    public Sprite incorrectImage;
    // Indica si el objeto es correcto o no
    public bool isCorrectShape;

    private Image shapeImage;

    // Esta variable se usará para verificar la selección de objetos correctos
    public static int correctObjectsCount = 0;
    public static int totalCorrectObjects = 0;

    // Referencia al script del temporizador
    private Contador contador;

    void Start()
    {
        // Obtener el componente Image del objeto
        shapeImage = GetComponent<Image>();
        // Contar cuántos objetos correctos hay al inicio
        if (isCorrectShape)
        {
            totalCorrectObjects++;
        }
        if (contador == null)
        {
            contador = FindObjectOfType<Contador>();
        }
    }

    // Método que se llama cuando se hace clic en el objeto
    public void OnObjectClick()
    {
        if (isCorrectShape)
        {
            // Cambiar a la imagen correcta
            shapeImage.sprite = correctImage;
            correctObjectsCount++;

            // Verificar si todos los objetos correctos han sido seleccionados
            if (correctObjectsCount >= totalCorrectObjects)
            {
                // Cambiar a la otra escena
                SceneManager.LoadScene("EstadisticasEjercicioSeleccionado");
            }
        }
        else
        {
            // Cambiar a la imagen incorrecta
            shapeImage.sprite = incorrectImage;

            if (contador != null)
            {
                contador.RestartTimer();
            }
        }
    }
}
