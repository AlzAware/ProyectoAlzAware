using System.Collections;
using UnityEngine;
using TMPro;  // Aseg�rate de tener esta referencia si no la tienes ya
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    public Sprite coverImage;           // Imagen de la cubierta que se muestra en las cartas al estar ocultas
    public float waitTime = 1.0f;       // Tiempo de espera antes de voltear las cartas nuevamente si no coinciden
    public TextMeshProUGUI timerText;   // Referencia al texto del temporizador
    public BoardManager boardManager;   // Referencia al script BoardManager

    private Card firstRevealedCard;     // Referencia a la primera carta revelada
    private Card secondRevealedCard;    // Referencia a la segunda carta revelada
    private bool isProcessing = false;  // Indica si se est� esperando a que las cartas se comparen
    private float timer;
    private bool gameEnded = false;

    private int totalPairs;             // N�mero total de parejas
    private int matchedPairs = 0;       // N�mero de parejas emparejadas correctamente

    private Contador contador;
    private SQLiteManager dbManager;
    void Start()
    {
        timer = 0f;
        StartCoroutine(UpdateTimer());

        // Obtener el n�mero total de parejas desde el BoardManager
        totalPairs = boardManager.cardSprites.Length; // El n�mero total de sprites es igual al n�mero de parejas
        
        dbManager = FindObjectOfType<SQLiteManager>();
        contador = FindObjectOfType<Contador>();
    }

    private IEnumerator UpdateTimer()
    {
        while (!gameEnded)
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

    public void CardRevealed(Card card)
    {
        if (firstRevealedCard == null)
        {
            firstRevealedCard = card;
        }
        else if (secondRevealedCard == null)
        {
            secondRevealedCard = card;
            StartCoroutine(CheckMatch());
        }
    }

    private IEnumerator CheckMatch()
    {
        isProcessing = true;

        yield return new WaitForSeconds(waitTime);

        if (firstRevealedCard.GetCardImage() == secondRevealedCard.GetCardImage())
        {
            // Si las cartas coinciden, realiza la animaci�n de crecimiento y desaparici�n
            firstRevealedCard.AnimateAndDestroy();
            secondRevealedCard.AnimateAndDestroy();

            matchedPairs++;

            // Verificar si hemos emparejado la �ltima pareja
            if (matchedPairs >= totalPairs)
            {
                GameOver();
            }
        }
        else
        {
            // Si no coinciden, oc�ltalas nuevamente
            firstRevealedCard.HideCard();
            secondRevealedCard.HideCard();
        }

        firstRevealedCard = null;
        secondRevealedCard = null;
        isProcessing = false;
    }

    private void GameOver()
    {
        Debug.Log("Juego completado, guardando estad�sticas...");

        // Detener el temporizador
        contador.DetenerTemporizador();

        // Obtener valores
        int idUsuario = ObtenerIdUsuario(); // ID del usuario activo
        int idEjercicio = 3; // ID del ejercicio (Busca Objetos)
        int puntuacion = CalcularPuntuacion(contador.ObtenerTiempoActual());
        string fecha = DateTime.Now.ToString("dd/MM/yyyy");

        // Guardar estad�sticas en la base de datos
        dbManager.InsertEstadistica(idUsuario, idEjercicio, puntuacion, fecha);

        // Cargar escena
        SceneManager.LoadScene("EstadisticasEj3");
    }

    public bool IsWaitingForMatch()
    {
        return isProcessing;
    }

    public void VolverAEjercicios()
    {
        SceneManager.LoadScene("Ejercicios");
    }

    private int ObtenerIdUsuario()
    {
        return PlayerPrefs.GetInt("UsuarioActivoID", 0); // Obtiene el ID del usuario activo
    }

    private int CalcularPuntuacion(int tiempoSegundos)
    {
        // Tiempo m�nimo y m�ximo en segundos
        int tiempoMinimo = 120; // 2:00
        int tiempoMaximo = 600; // 10:00

        // Si el tiempo es menor o igual al m�nimo, puntuaci�n m�xima
        if (tiempoSegundos <= tiempoMinimo)
        {
            return 100;
        }
        // Si el tiempo es mayor o igual al m�ximo, puntuaci�n m�nima
        if (tiempoSegundos >= tiempoMaximo)
        {
            return 10;
        }

        // C�lculo proporcional de la puntuaci�n
        float puntuacion = 100 - ((float)(tiempoSegundos - tiempoMinimo) / (tiempoMaximo - tiempoMinimo) * 90);
        return Mathf.RoundToInt(puntuacion);
    }
}
