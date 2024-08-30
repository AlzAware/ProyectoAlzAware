using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public GameObject cardPrefab;  // El prefab de la carta
    public int columns = 4;        // Número de columnas en la cuadrícula
    public int rows = 4;           // Número de filas en la cuadrícula
    public float horizontalSpacing = 9.0f; // Espaciado horizontal entre las cartas
    public float verticalSpacing = 9.0f;   // Espaciado vertical entre las cartas
    public Vector3 startPosition = Vector3.zero; // Punto de inicio para colocar las cartas
    public Vector3 cardScale = Vector3.one; // Escala de las cartas

    public Sprite[] cardSprites;

    private List<Sprite> deck;

    void Start()
    {
        InitializeDeck();

        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < columns; x++)
            {
                // Calcula la posición de cada carta basándose en el espaciado y la posición de inicio
                Vector3 pos = startPosition + new Vector3(x * horizontalSpacing, -y * verticalSpacing, 0);
                GameObject card = Instantiate(cardPrefab, pos, Quaternion.identity);

                // Establece la escala de la carta
                card.transform.localScale = cardScale;

                // Obtiene el script de la carta y asigna la imagen
                Card cardScript = card.GetComponent<Card>();
                cardScript.cardImage = GetRandomCard();
            }
        }
    }

    void InitializeDeck()
    {
        deck = new List<Sprite>();
        foreach (Sprite sprite in cardSprites)
        {
            // Agrega el sprite dos veces para formar parejas
            deck.Add(sprite);
            deck.Add(sprite);
        }
        ShuffleDeck();
    }

    Sprite GetRandomCard()
    {
        int index = Random.Range(0, deck.Count);
        Sprite card = deck[index];
        deck.RemoveAt(index);
        return card;
    }

    void ShuffleDeck()
    {
        for (int i = 0; i < deck.Count; i++)
        {
            Sprite temp = deck[i];
            int randomIndex = Random.Range(i, deck.Count);
            deck[i] = deck[randomIndex];
            deck[randomIndex] = temp;
        }
    }
}
