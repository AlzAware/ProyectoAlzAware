using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Esta clase permite crear un nuevo activo (ScriptableObject) desde el menú de Unity.
[CreateAssetMenu]
[System.Serializable]

// La clase ShapeData se utiliza para definir la estructura de una "forma" en un juego,
// permitiendo la configuración de una cuadrícula de filas y columnas que define la forma.
public class ShapeData : ScriptableObject
{
    // La clase interna Row representa una fila de la cuadrícula, donde cada columna se
    // define como un booleano (true si está ocupada, false si está vacía).
    [System.Serializable]
    public class Row
    {
        public bool[] column; // Arreglo que representa las columnas de la fila.
        private int _size = 0; // Tamaño de la fila.

        // Constructor por defecto.
        public Row(){}

        // Constructor que inicializa la fila con un tamaño específico.
        public Row(int size)
        {
            CreateRow(size); // Llama a CreateRow para inicializar la fila.
        }

        // Método que crea una fila de un tamaño específico, inicializando el arreglo de columnas.
        public void CreateRow(int size)
        {
            _size = size; // Establece el tamaño de la fila.
            column = new bool[_size]; // Inicializa el arreglo de columnas con el tamaño especificado.
            ClearRow(); // Limpia la fila (establece todas las columnas como false).
        }

        // Método que limpia la fila, estableciendo todas las columnas como false.
        public void ClearRow()
        {
            for (int i = 0; i < _size; i++)
            {
                column[i] = false; // Establece cada columna como false.
            }
        }
    }

    public int columns = 0; // Número de columnas de la cuadrícula.
    public int rows = 0; // Número de filas de la cuadrícula.
    public Row[] board; // Arreglo de filas que representa la cuadrícula completa.

    // Método que limpia todo el tablero, estableciendo cada celda como false.
    public void Clear()
    {
        for (var i = 0; i < rows; i++)
        {
            board[i].ClearRow(); // Limpia cada fila en el tablero.
        }
    }

    // Método que crea un nuevo tablero con el número de filas y columnas especificadas.
    public void CreateNewBoard()
    {
        board = new Row[rows]; // Inicializa el arreglo de filas con el número especificado de filas.

        for(var i = 0; i < rows; i++)
        {
            board[i] = new Row(columns); // Inicializa cada fila con el número especificado de columnas.
        }
    }
}
