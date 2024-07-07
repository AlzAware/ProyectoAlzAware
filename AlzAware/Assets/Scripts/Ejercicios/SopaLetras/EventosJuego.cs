using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  static class EventosJuego
{
    //habilitar la seleccion del cuadrado
    public delegate void EnableSquareSelection();
    public static event EnableSquareSelection OnEnableSquareSelection;
    public static void EnableSquareSelectionMethod()
    {
        if (OnEnableSquareSelection != null)
            OnEnableSquareSelection();
    }

    //****************************

    //deshabilitar la seleccion del cuadrado
    public delegate void DisableSquareSelection();
    public static event DisableSquareSelection OnDisableSquareSelection;
    public static void DisableSquareSelectionMethod()
    {
        if (OnDisableSquareSelection != null)
            OnDisableSquareSelection();
    }

    //****************************

    //Seleccionar el cuadrado
    public delegate void SelectSquare(Vector3 position);
    public static event SelectSquare OnSelectSquare;
    public static void SelectSquareMethod(Vector3 position)
    {
        if (OnSelectSquare != null)
            OnSelectSquare(position);
    }

    //****************************

    //Marcar el cuadrado
    public delegate void CheckSquare(string letter, Vector3 squarePosition, int squareIndex);
    public static event CheckSquare OnCheckSquare;
    public static void CheckSquareMethod(string letter, Vector3 squarePosition, int squareIndex)
    {
        if (OnCheckSquare != null)
            OnCheckSquare(letter, squarePosition, squareIndex);
    }

    //****************************

    //Limpiar la seleccion del cuadrado
    public delegate void ClearSelection();
    public static event ClearSelection OnClearSelection;
    public static void ClearSelectionMethod()
    {
        if (OnClearSelection != null)
            OnClearSelection();
    }
    //****************************
    //Palabra correcta
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
    //Pizarra completada
    public delegate void BoardCompleted();
    public static event BoardCompleted OnBoardCompleted;
    public static void BoardCompletedMethod()
    {
        if (OnBoardCompleted != null)
            OnBoardCompleted();
    }
    //****************************
    //Cargar siguiente nivel
    public delegate void LoadNextLevel();
    public static event LoadNextLevel OnLoadNextLevel;
    public static void LoadNextLevelMethod()
    {
        if (OnLoadNextLevel != null)
            OnLoadNextLevel();
    }
}
