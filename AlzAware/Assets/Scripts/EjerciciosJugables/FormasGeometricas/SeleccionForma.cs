using System;
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
    private SQLiteManager dbManager;

    void Start()
    {
        // Obtener el componente Image del objeto
        shapeImage = GetComponent<Image>();
        // Contar cuántos objetos correctos hay al inicio
        if (isCorrectShape)
        {
            totalCorrectObjects++;
        }

        dbManager = FindObjectOfType<SQLiteManager>();
        contador = FindObjectOfType<Contador>();
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
                Debug.Log("Juego completado, guardando estadísticas...");

                // Detener el temporizador
                contador.DetenerTemporizador();

                // Obtener valores
                int idUsuario = ObtenerIdUsuario(); // ID del usuario activo
                int idEjercicio = 6; // ID del ejercicio (Busca Objetos)
                int puntuacion = CalcularPuntuacion(contador.ObtenerTiempoActual());
                string fecha = DateTime.Now.ToString("dd/MM/yyyy");

                // Guardar estadísticas en la base de datos
                dbManager.InsertEstadistica(idUsuario, idEjercicio, puntuacion, fecha);

                // Cargar escena
                SceneManager.LoadScene("EstadisticasEj6");
            }
        }
        else
        {
            // Cambiar a la imagen incorrecta
            shapeImage.sprite = incorrectImage;
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
