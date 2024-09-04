using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  static class EventosJuego
{
    //Evento que habilita la seleccion del cuadrado
    public delegate void EnableSquareSelection();
    public static event EnableSquareSelection OnEnableSquareSelection;
    public static void EnableSquareSelectionMethod()
    {
        if (OnEnableSquareSelection != null)
            OnEnableSquareSelection();
    }

    //****************************

    //Evento que deshabilita la seleccion del cuadrado
    public delegate void DisableSquareSelection();
    public static event DisableSquareSelection OnDisableSquareSelection;
    public static void DisableSquareSelectionMethod()
    {
        if (OnDisableSquareSelection != null)
            OnDisableSquareSelection();
    }

    //****************************

    //Evento que permite la seleccion del cuadrado
    public delegate void SelectSquare(Vector3 position);
    public static event SelectSquare OnSelectSquare;
    public static void SelectSquareMethod(Vector3 position)
    {
        if (OnSelectSquare != null)
            OnSelectSquare(position);
    }

    //****************************

    //Evento para marcar el cuadrado
    public delegate void CheckSquare(string letter, Vector3 squarePosition, int squareIndex);
    public static event CheckSquare OnCheckSquare;
    public static void CheckSquareMethod(string letter, Vector3 squarePosition, int squareIndex)
    {
        if (OnCheckSquare != null)
            OnCheckSquare(letter, squarePosition, squareIndex);
    }

    //****************************

    //Evento para limpiar la seleccion del cuadrado
    public delegate void ClearSelection();
    public static event ClearSelection OnClearSelection;
    public static void ClearSelectionMethod()
    {
        if (OnClearSelection != null)
            OnClearSelection();
    }
    //****************************
    //Evento para detectar la palabra correcta
    public delegate void CorrectWord(string word, List<int> squareIndexes);
    public static event CorrectWord OnCorrectWord;

    public static void CorrectWordMethod(string word, List<int> squareIndexes)
    {
        if (OnCorrectWord != null)
        {
            OnCorrectWord(word, squareIndexes);
        }
    }
    //****************************
    //Evento para detectar la pizarra completada
    public delegate void BoardCompleted();
    public static event BoardCompleted OnBoardCompleted;
    public static void BoardCompletedMethod()
    {
        if (OnBoardCompleted != null)
            OnBoardCompleted();
    }
}
