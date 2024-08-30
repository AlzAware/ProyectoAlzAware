using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BusquedaPalabra : MonoBehaviour
{
    public TMP_Text displayedText;
    public Image crossLine;


    private string _word;
    void Start()
    {

    }

    private void OnEnable()
    {
        EventosJuego.OnCorrectWord += CorrectWord;
    }
    private void OnDisable()
    {
        EventosJuego.OnCorrectWord -= CorrectWord;

    }

    public void SetWord(string word)
    {
        _word = word;
        displayedText.text = word;

    }
    //Funcion para tachar la palabra si corresponde con la seleccionada
    private void CorrectWord(string word, List<int> squareIndexes)
    {
        if (word == _word)
        {
            crossLine.gameObject.SetActive(true);

        }
    }
}
