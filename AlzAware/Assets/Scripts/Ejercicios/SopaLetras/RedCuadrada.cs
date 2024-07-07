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


    public void SetIndex(int index)
    {
        _index = index;
    }
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
    private void OnEnable()
    {
        EventosJuego.OnEnableSquareSelection += OnEnableSquareSelection;
        EventosJuego.OnDisableSquareSelection += OnDisableSquareSelection;
        EventosJuego.OnSelectSquare += SelectSquare;
        EventosJuego.OnCorrectWord += CorrectWord;

    }
    //Deshabilitar la seleccion del cuadrado en los eventos del juego.
    private void OnDisable()
    {
        EventosJuego.OnEnableSquareSelection -= OnEnableSquareSelection;
        EventosJuego.OnDisableSquareSelection -= OnDisableSquareSelection;
        EventosJuego.OnSelectSquare -= SelectSquare;
        EventosJuego.OnCorrectWord -= CorrectWord;

    }
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

    //mientras se activa la seleccion de cuadrados puedes seguir clicando varias letras??
    public void OnEnableSquareSelection()
    {
        _clicked = true;
        _selected = false;

    }
    //al desactivar la seleccion si la palabra es correcta se activara sprite correcto y si no se desactivara la seleccion y volver a sprite normalLetras
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
    //asocia la seleccion de las letras con el sprite de seleccionLetras
    public void SelectSquare(Vector3 position)
    {
        if (this.gameObject.transform.position == position)
        {
            _displayedImage.sprite = _selectedLetterData.image;
        }
    }
    public void SetSprite(DatosAlfabeto.LetterData normalLetterData, DatosAlfabeto.LetterData selectedLetterData,
       DatosAlfabeto.LetterData correctLetterData)
    {
        _normalLetterData = normalLetterData;
        _selectedLetterData = selectedLetterData;
        _correctLetterData = correctLetterData;

        GetComponent<SpriteRenderer>().sprite = _normalLetterData.image;
    }
    private void OnMouseDown()
    {
        OnEnableSquareSelection();
        EventosJuego.EnableSquareSelectionMethod();
        CheckSquare();
        _displayedImage.sprite = _selectedLetterData.image;

    }
    //El enter del mouse comprueba los cuadrados
    private void OnMouseEnter()
    {
        CheckSquare();
    }
    //Al despulsar el boton del raton se desactiva la seleccion
    private void OnMouseUp()
    {
        EventosJuego.ClearSelectionMethod();
        EventosJuego.DisableSquareSelectionMethod();
    }

    public void CheckSquare()
    {
        if (_selected == false && _clicked == true)
        {
            _selected = true;
            EventosJuego.CheckSquareMethod(_normalLetterData.letter, gameObject.transform.position, _index);
        }
    }
}
