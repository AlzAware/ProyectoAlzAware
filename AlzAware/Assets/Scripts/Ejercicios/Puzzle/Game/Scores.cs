
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

// Definición de una clase serializable BestScoreData, que se utilizará para almacenar el puntaje más alto.
[System.Serializable]
public class BestScoreData
{
    public int score = 0; 
}

public class Scores : MonoBehaviour


{
    public SquareTextureData squareTextureData; // Referencia para manejar datos de texturas de cuadrados.
    public TMP_Text scoreText; // Referencia al componente de texto que muestra el puntaje actual.

    // Variables privadas para manejar el estado interno del puntaje.
    private bool newBestScore_ = false;     // Indica si se ha alcanzado un nuevo puntaje más alto.
    private BestScoreData bestScores_ = new BestScoreData();    // Objeto que almacena el puntaje más alto.
    private int currentScores_;     // Variable para almacenar el puntaje actual.

    // Clave para acceder al almacenamiento del puntaje más alto.
    private string bestScoreKey_ = "bsdat";

    // Método Awake, llamado antes de que el juego comience.
    private void Awake()
    {
        // Comprueba si ya existe un archivo guardado con la clave `bestScoreKey_`.
        if(BinaryDataStream.Exist(bestScoreKey_))
        {
            // Si existe, inicia una corrutina para leer el archivo.
            StartCoroutine(ReadDataFile());
        }
    }

    // Corrutina para leer el archivo de datos de puntaje.
    private IEnumerator ReadDataFile()
    {
        // Lee los datos del puntaje más alto desde un archivo binario.
        bestScores_ = BinaryDataStream.Read<BestScoreData>(bestScoreKey_);
        yield return new WaitForEndOfFrame(); // Espera hasta el final del frame para continuar la ejecución.
        //GameEvents.UpdateBestScoreBar(currentScores_, bestScores_.score);
        
    }
    
    void Start()
    {
        currentScores_ = 0;
        newBestScore_ = false;
        squareTextureData.SetStartColor();  // Establece el color inicial de las texturas cuadradas.
        UpdateScoreText();  // Actualiza el texto que muestra el puntaje.
    }

    private void OnEnable()
    {
        GameEvents.AddScores += AddScores;
        GameEvents.GameOver += SaveBestScores;
        
    } 

    private void OnDisable()
    {
        GameEvents.AddScores -= AddScores;
        GameEvents.GameOver -= SaveBestScores;
    }

    
    public void SaveBestScores(bool newBestScores)
    {
        BinaryDataStream.Save<BestScoreData>(bestScores_, bestScoreKey_);
    }
    

    
    private void AddScores(int scores)
    {
        // Incrementa el puntaje actual con los puntos recibidos.
        currentScores_ += scores;
        // Si el puntaje actual es mayor que el puntaje más alto registrado, actualiza el puntaje más alto.
        if(currentScores_ > bestScores_.score)
        {
            newBestScore_ = true;   // Marca que se ha alcanzado un nuevo puntaje máximo.
            bestScores_.score = currentScores_; // Actualiza el puntaje más alto con el actual.
            SaveBestScores(true);   // Guarda el nuevo puntaje más alto.
        }

        // Actualiza el color de los cuadrados en función del puntaje actual.
        UpdateSquareColor();
        //GameEvents.UpdateBestScoreBar (currentScores_, bestScores_.score);
        UpdateScoreText();
        
    }
   

    // Método para actualizar el color de los cuadrados.
    private void UpdateSquareColor()
    {
        // Si el evento UpdateSquareColor no es nulo y el puntaje actual supera el umbral, actualiza los colores.
        if (GameEvents.UpdateSquareColor != null && currentScores_ >= squareTextureData.tresholdVal)
        {
            squareTextureData.UpdateColors(currentScores_);     // Actualiza los colores basados en el puntaje.
            GameEvents.UpdateSquareColor(squareTextureData.currentColor);   // Dispara el evento para actualizar el color.
        }
    }

    // Método para actualizar el texto del puntaje en pantalla.
    private void UpdateScoreText()
    {
        scoreText.text = currentScores_.ToString(); // Convierte el puntaje actual a texto y lo muestra.
    }

 
}


