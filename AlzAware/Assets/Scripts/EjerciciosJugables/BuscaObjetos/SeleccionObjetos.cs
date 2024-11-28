using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SeleccionObjetos : MonoBehaviour
{
    // Imagen correcta
    public Sprite correctImage;
    // Imagen incorrecta
    public Sprite incorrectImage;
    // Indica si el objeto es correcto o no
    public bool isCorrectObject;

    private Image objectImage;

    // Esta variable se usar� para verificar la selecci�n de objetos correctos
    public static int correctObjectsCount = 0;
    public static int totalCorrectObjects = 0;

    // Referencia al script del temporizador
    public Contador contador;

    void Start()
    {
        // Obtener el componente Image del objeto
        objectImage = GetComponent<Image>();
        // Contar cu�ntos objetos correctos hay al inicio
        if (isCorrectObject)
        {
            totalCorrectObjects++;
        }
        if (contador == null)
        {
            contador = FindObjectOfType<Contador>();
        }
    }

    // M�todo que se llama cuando se hace clic en el objeto
    public void OnObjectClick()
    {
        if (isCorrectObject)
        {
            // Cambiar a la imagen correcta
            objectImage.sprite = correctImage;
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
            objectImage.sprite = incorrectImage;

            // Reiniciar el temporizador seg�n el ejercicio actual
            if (contador != null)
            {
                contador.RestartTimer();
            }
        }
    }
}
