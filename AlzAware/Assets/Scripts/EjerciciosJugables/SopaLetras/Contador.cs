using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Contador : MonoBehaviour
{
    public TMP_Text timerText;   // Referencia al componente de texto
    public float startTime; // Tiempo inicial en segundos
    private float initialTime; // Tiempo inicial guardado para reiniciar
    private bool timerIsRunning = false;

    void Start()
    {
        SetStartTimeBasedOnScene();
        initialTime = startTime;     // Guardar el tiempo inicial para reinicios
        timerIsRunning = true;
    }

    void Update()
    {
        if (timerIsRunning)
        {
            startTime -= Time.deltaTime;
            UpdateTimerText(startTime);

            // Cambiar de escena cuando llegue a 0 o menos
            if (startTime <= 0f)
            {
                timerIsRunning = false;
                SceneManager.LoadScene("EstadisticasEjercicioSeleccionado");
            }
        }
    }

    void SetStartTimeBasedOnScene()
    {
        // Define tiempos específicos según la escena actual
        string currentScene = SceneManager.GetActiveScene().name;

        if (currentScene.Contains("SopaLetras"))
        {
            startTime = 180f;
        }
        else if (currentScene.Contains("BuscaObjetos"))
        {
            startTime = 60f;
        }
        else if (currentScene.Contains("Refranes"))
        {
            startTime = 60f;
        }
    }
    // Método para reiniciar el temporizador
    public void RestartTimer()
    {
        startTime = initialTime;  // Reiniciar al tiempo original del ejercicio
        timerIsRunning = true;    // Asegurar que el temporizador siga corriendo
    }

    // Método que actualiza el texto del temporizador en formato mm:ss
    void UpdateTimerText(float timeToDisplay)
    {
        timeToDisplay = Mathf.Max(timeToDisplay, 0); // Evita valores negativos en pantalla

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}

