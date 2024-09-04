using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EventosJuego;
using static Unity.VisualScripting.Member;

public class RedCuadrada : MonoBehaviour
{
    public int SquareIndex { get; set; }

    private DatosAlfabeto.LetterData _normalLetterData;
    private DatosAlfabeto.LetterData _selectedLetterData;
    private DatosAlfabeto.LetterData _correctLetterData;

    private SpriteRenderer _displayedImage;

    private bool _selected;
    private bool _clicked;
    private int _index = -1;
    private bool _correct;

    //Metodo que posiciona un indice
    public void SetIndex(int index)
    {
        _index = index;
    }
    //Metodo que consigue la posicion del indice
    public int GetIndex()
    {
        return _index;
    }

    void Start()
    {
        _selected = false;
        _clicked = false;
        _correct = false;
        _displayedImage = GetComponent<SpriteRenderer>();
              
    }
    //Metodo que habilita los eventos de la clase eventos de juegos: habilitar, desabilitar y seleccion del cuadradoy comprobacion de la palabra correcta
    private void OnEnable()
    {
        EventosJuego.OnEnableSquareSelection += OnEnableSquareSelection;
        EventosJuego.OnDisableSquareSelection += OnDisableSquareSelection;
        EventosJuego.OnSelectSquare += SelectSquare;
        EventosJuego.OnCorrectWord += CorrectWord;

    }
    //Metodo que deshabilita los eventos de la clase eventos de juegos: habilitar, desabilitar y seleccion del cuadradoy comprobacion de la palabra correcta
    private void OnDisable()
    {
        EventosJuego.OnEnableSquareSelection -= OnEnableSquareSelection;
        EventosJuego.OnDisableSquareSelection -= OnDisableSquareSelection;
        EventosJuego.OnSelectSquare -= SelectSquare;
        EventosJuego.OnCorrectWord -= CorrectWord;

    }

    //Metodo que comprueba si la palabra es correcto y actualiza forma visual de la palabra a correcta
    private void CorrectWord(string word, List<int> squareIndexes)
    {
        if (_selected && squareIndexes.Contains(_index))
        {
            _correct = true;
            _displayedImage.sprite = _correctLetterData.image;
        }

        _selected = false;
        _clicked = false;
    }

    //Metodo que controla que el cuadrado este en un estado correcto para cuando se vaya a seleccionar
    public void OnEnableSquareSelection()
    {
        _clicked = true;
        _selected = false;

    }
    //Metodo qu al desactivar la seleccion, si la palabra es correcta, se activara sprite correcto y si no se desactivara la seleccion y volver a sprite normalLetras
    public void OnDisableSquareSelection()
    {
        _clicked = false;
        _selected = false;

        if (_correct == true)
        {
            _displayedImage.sprite = _correctLetterData.image;
        }
        else
        {
            _displayedImage.sprite = _normalLetterData.image;
        }
    }
    //Metodo para asociar la seleccion de las letras con el sprite seleccionLetras
    public void SelectSquare(Vector3 position)
    {
        if (this.gameObject.transform.position == position)
        {
            _displayedImage.sprite = _selectedLetterData.image;
        }
    }
    //Metodo que coloca los sprite correspondientes cuando no se seleccionan los cuadrados, para cuando se seleccionan y para cuando la palabra es comprobada como correcta
    public void SetSprite(DatosAlfabeto.LetterData normalLetterData, DatosAlfabeto.LetterData selectedLetterData,
       DatosAlfabeto.LetterData correctLetterData)
    {
        _normalLetterData = normalLetterData;
        _selectedLetterData = selectedLetterData;
        _correctLetterData = correctLetterData;

        GetComponent<SpriteRenderer>().sprite = _normalLetterData.image;
    }
    //Metodo que habilita la seleccion del cuadrado cuando se presiona el mouse y permite cambiar el sprite a letra seleccionada
    private void OnMouseDown()
    {
        OnEnableSquareSelection();
        EventosJuego.EnableSquareSelectionMethod();
        CheckSquare();
        _displayedImage.sprite = _selectedLetterData.image;

    }
    //Metodo que comprueba los cuadrados cuando el mouse hace enter
    private void OnMouseEnter()
    {
        CheckSquare();
    }
    //Metodo que desactiva la seleccion al despulsar el boton del raton 
    private void OnMouseUp()
    {
        EventosJuego.ClearSelectionMethod();
        EventosJuego.DisableSquareSelectionMethod();
    }
    //Metodo que evaluar las celdas seleccionadas
    public void CheckSquare()
    {
        if (_selected == false && _clicked == true)
        {
            _selected = true;
            EventosJuego.CheckSquareMethod(_normalLetterData.letter, gameObject.transform.position, _index);
        }
    }
}
