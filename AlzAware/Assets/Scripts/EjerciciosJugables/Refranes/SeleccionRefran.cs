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
    private SQLiteManager dbManager;

    void Start()
    {
        // Obtener el componente Image del objeto
        opcionObject = GetComponent<Image>();
        correctObjectsCount = 0;

        dbManager = FindObjectOfType<SQLiteManager>();
        contador = FindObjectOfType<Contador>();

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
                Debug.Log("Juego completado, guardando estadísticas...");

                // Detener el temporizador
                contador.DetenerTemporizador();

                // Obtener valores
                int idUsuario = ObtenerIdUsuario(); // ID del usuario activo
                int idEjercicio = 5; // ID del ejercicio (Busca Objetos)
                int puntuacion = CalcularPuntuacion(contador.ObtenerTiempoActual());
                string fecha = DateTime.Now.ToString("dd/MM/yyyy");

                // Guardar estadísticas en la base de datos
                dbManager.InsertEstadistica(idUsuario, idEjercicio, puntuacion, fecha);

                // Cargar escena
                SceneManager.LoadScene("EstadisticasEj5");
            }

        }
        else
        {
            // Cambiar a la imagen incorrecta
            opcionObject.sprite = incorrectImage;

            //Disminuye temporizador si fallas
            if (contador != null)
            {
                contador.AddTime(10f); // Suma 10 segundos y activa el efecto de color rojo
            }
        }
    }
    private int ObtenerIdUsuario()
    {
        return PlayerPrefs.GetInt("UsuarioActivoID", 0); // Obtiene el ID del usuario activo
    }

    private int CalcularPuntuacion(int tiempoSegundos)
    {
        // Tiempo mínimo y máximo en segundos
        int tiempoMinimo = 15; // 0:15
        int tiempoMaximo = 60; // 1:00

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