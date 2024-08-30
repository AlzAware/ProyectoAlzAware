using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Contador : MonoBehaviour
{
    public TMP_Text timerText;  // Referencia al componente de texto
    private float timeElapsed = 0; // Tiempo transcurrido en segundos
    private bool timerIsRunning = false;

    void Start()
    {
        // Inicializa y comienza el temporizador
        timerIsRunning = true;
    }

    void Update()
    {
        if (timerIsRunning)
        {
            timeElapsed += Time.deltaTime;
            UpdateTimerText(timeElapsed);
        }
    }

    void UpdateTimerText(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}


