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
    //Metodo que habilita el evento de palabra correcta
    private void OnEnable()
    {
        EventosJuego.OnCorrectWord += CorrectWord;
    }
    //Metodo que deshabilita el evento de palabra correcta
    private void OnDisable()
    {
        EventosJuego.OnCorrectWord -= CorrectWord;

    }
    //Metodo para establecer las palabras que se colocaron
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
