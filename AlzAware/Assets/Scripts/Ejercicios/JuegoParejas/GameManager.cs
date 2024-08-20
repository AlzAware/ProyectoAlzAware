using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Sprite coverImage;           // Imagen de la cubierta que se muestra en las cartas al estar ocultas
    public float waitTime = 1.0f;       // Tiempo de espera antes de voltear las cartas nuevamente si no coinciden

    private Card firstRevealedCard;     // Referencia a la primera carta revelada
    private Card secondRevealedCard;    // Referencia a la segunda carta revelada
    private bool isProcessing = false;  // Indica si se está esperando a que las cartas se comparen

    // Método llamado por las cartas cuando se revelan
    public void CardRevealed(Card card)
    {
        // Si esta es la primera carta que se revela
        if (firstRevealedCard == null)
        {
            firstRevealedCard = card;
        }
        // Si esta es la segunda carta que se revela
        else if (secondRevealedCard == null)
        {
            secondRevealedCard = card;

            // Inicia la comprobación de coincidencia entre las dos cartas
            StartCoroutine(CheckMatch());
        }
    }

    // Método para verificar si las dos cartas reveladas coinciden
    private IEnumerator CheckMatch()
    {
        isProcessing = true;

        // Espera el tiempo definido antes de proceder (para que el jugador vea ambas cartas)
        yield return new WaitForSeconds(waitTime);

        // Si las cartas coinciden
        if (firstRevealedCard.GetCardImage() == secondRevealedCard.GetCardImage())
        {
            // Puedes agregar lógica aquí para incrementar puntuación, etc.
            yield return new WaitForSeconds(0.5f);
            Destroy(firstRevealedCard.gameObject);
            Destroy(secondRevealedCard.gameObject);
        }
        // Si las cartas no coinciden
        {
            // Oculta las cartas nuevamente
            firstRevealedCard.HideCard();
            secondRevealedCard.HideCard();
        }

        // Resetea las referencias para la siguiente jugada
        firstRevealedCard = null;
        secondRevealedCard = null;
        isProcessing = false;
    }

    // Método para comprobar si se está esperando la coincidencia (evitar que se revelen más de dos cartas a la vez)
    public bool IsWaitingForMatch()
    {
        return isProcessing;
    }
}
