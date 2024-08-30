using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ControladorParejas : MonoBehaviour
{
    public TextMeshProUGUI timerText;  // Referencia al texto del temporizador
    private float timer;

    void Start()
    {
        timer = 0f;
        StartCoroutine(UpdateTimer());
    }

    private IEnumerator UpdateTimer()
    {
        while (true)
        {
            timer += Time.deltaTime;

            // Calcula los minutos y segundos
            int minutes = Mathf.FloorToInt(timer / 60f);
            int seconds = Mathf.FloorToInt(timer % 60f);

            // Actualiza el texto del temporizador con formato 00:00
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

            yield return null;
        }
    }

    // Método utilizado para volver a Ejercicios
    public void VolverAEjercicios()
    {
        SceneManager.LoadScene("Ejercicios");
    }
}