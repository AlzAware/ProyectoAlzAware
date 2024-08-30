using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverPopUp : MonoBehaviour
{
    // Referencias a los objetos que serán las ventanas emergentes del juego.
    public GameObject gameOverPopUp;    // Ventana emergente general de "Game Over"
    public GameObject loosePopUp;   // Ventana emergente que indica que el jugador ha perdido
    public GameObject newBestScorePopUp;    // Ventana emergente que indica si se ha alcanzado un nuevo récord.

    void Start()
    {
        // Desactiva la ventana emergente de "Game Over" para que no sea visible al principio.
        gameOverPopUp.SetActive(false);
    }

    // Método OnEnable se llama cuando el objeto al que está asignado este script es activado.
    private void OnEnable()
    { 
        // Esto significa que cuando ocurra el evento GameOver, se ejecutará el método OnGameOver.
        GameEvents.GameOver += OnGameOver;
    }

    // Método OnDisable se llama cuando el objeto al que está asignado este script es desactivado.
    private void OnDisable()
    {
        // Desuscribirse del evento GameOver para evitar que OnGameOver se llame accidentalmente 
        // después de que el objeto haya sido desactivado.
        GameEvents.GameOver -= OnGameOver;
    }

    // Método OnGameOver que se ejecuta cuando se dispara el evento GameOver.
    // Este método recibe un parámetro booleano que indica si se ha alcanzado un nuevo récord.
    private void OnGameOver(bool newBestScore)
    {
        // Activa la ventana emergente general de "Game Over" para que sea visible.
        gameOverPopUp.SetActive(true);
        // Desactiva la ventana emergente de "Perdido" para que no sea visible.
        loosePopUp.SetActive(false);
        // Activa la ventana emergente de "Nuevo Récord" si se ha alcanzado un nuevo récord.
        // (Actualmente, siempre se activa sin importar el valor de newBestScore, lo cual podría ser un error).
        newBestScorePopUp.SetActive(true);
    }

}
