using System.Collections;
using UnityEngine;
using TMPro;

public class Contador : MonoBehaviour
{
    public TMP_Text timerText; // Referencia al texto del temporizador
    private float elapsedTime = 0f; // Tiempo transcurrido
    private bool timerIsRunning = true; // Bandera para controlar el temporizador
    private readonly float maxTime = 99 * 60 + 59; // Tiempo máximo (99:59)
    private Color originalColor; // Color original del texto del temporizador
    private float finalTime = 0f; // Guarda el tiempo final cuando se detiene

    void Start()
    {
        // Guarda el color original del texto
        originalColor = timerText.color;

        UpdateTimerText(elapsedTime);
    }

    void Update()
    {
        if (timerIsRunning)
        {
            // Incrementar el tiempo cada frame
            elapsedTime += Time.deltaTime;

            // Limitar el tiempo al máximo permitido (99:59)
            elapsedTime = Mathf.Min(elapsedTime, maxTime);

            // Actualizar el texto del temporizador
            UpdateTimerText(elapsedTime);
        }
    }

    // Método para agregar tiempo al contador (por ejemplo, cuando hay errores)
    public void AddTime(float seconds)
    {
        elapsedTime += seconds;

        // Asegurar que no exceda el máximo tiempo
        elapsedTime = Mathf.Min(elapsedTime, maxTime);

        // Actualizar el texto del temporizador
        UpdateTimerText(elapsedTime);

        // Iniciar el efecto de texto en rojo
        StartCoroutine(TemporizadorEnRojo());
    }

    // Método que actualiza el texto del temporizador en formato mm:ss
    void UpdateTimerText(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    // Método para detener el temporizador y guardar el tiempo final
    public void DetenerTemporizador()
    {
        timerIsRunning = false;
        finalTime = elapsedTime; // Guarda el tiempo final
        Debug.Log("Temporizador detenido. Tiempo final: " + ObtenerTiempoActual() + " segundos.");
    }

    // Método para obtener el tiempo final como un entero (en segundos)
    public int ObtenerTiempoActual()
    {
        return Mathf.FloorToInt(finalTime);
    }

    // Corrutina para cambiar el color del temporizador a rojo y devolverlo al original
    private IEnumerator TemporizadorEnRojo()
    {
        timerText.color = Color.red;
        yield return new WaitForSeconds(2f);
        timerText.color = originalColor;
    }
}
