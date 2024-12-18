using System;
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

    // Contadores para verificar la selección
    public static int correctObjectsCount = 0;
    public static int totalCorrectObjects = 0;

    private Contador contador;
    private SQLiteManager dbManager;

    void Start()
    {
        // Obtener el componente Image del objeto
        objectImage = GetComponent<Image>();

        // Contar cuántos objetos correctos hay al inicio
        if (isCorrectObject)
        {
            totalCorrectObjects++;
        }

        dbManager = FindObjectOfType<SQLiteManager>();
        contador = FindObjectOfType<Contador>();
    }

    // Método que se llama cuando se hace clic en el objeto
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
                TerminarEjercicio();
            }
        }
        else
        {
            // Cambiar a la imagen incorrecta
            objectImage.sprite = incorrectImage;

            // Sumar tiempo y mostrar el temporizador en rojo
            if (contador != null)
            {
                contador.AddTime(10f); // Suma 10 segundos
            }
        }
    }

    // Método que se llama cuando se completan todos los objetos correctos
    private void TerminarEjercicio()
    {
        Debug.Log("Juego completado, guardando estadísticas...");

        // Detener el temporizador
        contador.DetenerTemporizador();

        // Obtener valores
        int idUsuario = ObtenerIdUsuario(); // ID del usuario activo
        int idEjercicio = 2; // ID del ejercicio (Busca Objetos)
        int puntuacion = CalcularPuntuacion(contador.ObtenerTiempoActual());
        string fecha = DateTime.Now.ToString("dd/MM/yyyy");

        // Guardar estadísticas en la base de datos
        dbManager.InsertEstadistica(idUsuario, idEjercicio, puntuacion, fecha);

        // Cargar escena
        SceneManager.LoadScene("EstadisticasEj2");
    }

    private int ObtenerIdUsuario()
    {
        return PlayerPrefs.GetInt("UsuarioActivoID", 0); // Obtiene el ID del usuario activo
    }

    private int CalcularPuntuacion(int tiempoSegundos)
    {
        // Tiempo mínimo y máximo en segundos
        int tiempoMinimo = 45; // 0:45
        int tiempoMaximo = 180; // 3:00

        // Si el tiempo es menor o igual al mínimo, puntuación máxima
        if (tiempoSegundos <= tiempoMinimo)
        {
            return 100;
        }
        // Si el tiempo es mayor o igual al máximo, puntuación mínima
        if (tiempoSegundos >= tiempoMaximo)
        {
            return 10;
        }

        // Cálculo proporcional de la puntuación
        float puntuacion = 100 - ((float)(tiempoSegundos - tiempoMinimo) / (tiempoMaximo - tiempoMinimo) * 90);
        return Mathf.RoundToInt(puntuacion);
    }
}
