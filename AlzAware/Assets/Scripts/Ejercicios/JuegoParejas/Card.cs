using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    // Start is called before the first frame update

    private SpriteRenderer spriteRenderer;  // Referencia al componente SpriteRenderer de la carta
    private GameManager gameManager;        // Referencia al GameManager
    public Sprite cardImage;                // Imagen que representa la carta (el anverso)
    public bool isRevealed = false;         // Indica si la carta está revelada o no
    private Sprite coverImage;              // Imagen de la cubierta de la carta (el reverso)


    void Start()
    {
        // Inicializa las referencias al SpriteRenderer y al GameManager
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameManager = FindObjectOfType<GameManager>();

        // Obtiene la imagen de cubierta desde el GameManager
        coverImage = gameManager.coverImage;

        // Inicia con la carta oculta
        HideCard();
    }

    void OnMouseDown()
    {

        // Si la carta no está revelada y no se está esperando una coincidencia
        if (!isRevealed && !gameManager.IsWaitingForMatch())
        {
            // Revela la carta
            RevealCard();

            // Informa al GameMAnager que esta carta ha sido revelada 
            gameManager.CardRevealed(this); // Notifica al GameManager
        }
    }

    public void RevealCard()  
    {
        isRevealed = true;
        spriteRenderer.sprite = cardImage;  // Muestra la imagen de la carta
    }

    public void HideCard()
    {
        isRevealed = false;
        spriteRenderer.sprite = coverImage;  // Muestra la imagen de cubierta
    }



    // Devuelve la imagen de la carta (utilizada para comparar cartas)
    public Sprite GetCardImage()
    {
        return cardImage;
    }


    // Update is called once per frame

}
