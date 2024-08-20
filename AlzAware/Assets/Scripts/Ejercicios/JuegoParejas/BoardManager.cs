using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject cardPrefab;  // El prefab de la carta
    public int columns = 4;        // Número de columnas en la cuadrícula
    public int rows = 4;           // Número de filas en la cuadrícula
    public float spacing = 9.0f;   // Espacio entre las cartas

    public Sprite[] cardSprites;

    private List<Sprite> deck;

    void Start()
    {

        InitializeDeck();
        Vector3 startPos = new Vector3(-columns / 2.7f * spacing, rows / 3.0f * spacing, 0);

        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < columns; x++)
            {
                Vector3 pos = startPos + new Vector3(x * spacing, -y * spacing, 0);
                GameObject card = Instantiate(cardPrefab, pos, Quaternion.identity);
                Card cardScript = card.GetComponent<Card>();
                cardScript.cardImage = GetRandomCard();  // Asigna la imagen correcta a cada carta
                Debug.Log("Asignado sprite: " + cardScript.cardImage.name);
            }
        }
    }

    void InitializeDeck()
    {
        deck = new List<Sprite>();
        foreach (Sprite sprite in cardSprites)
        {
            deck.Add(sprite);  // Agrega el sprite dos veces para formar parejas
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


    // Update is called once per frame
   

