using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineIndicator : MonoBehaviour
{
    // Matriz 9x9 que representa los índices de las celdas en una cuadrícula. 
    // Cada fila y columna tiene un número que identifica una celda única.
    public int[,] line_data = new int [9,9]
    {
        { 0, 1, 2,      3, 4, 5,       6, 7, 8 },
        { 9, 10, 11,    12, 13, 14,   15, 16, 17 },
        { 18, 19, 20,   21, 22, 23,   24, 25, 26 },

        { 27, 28, 29,  30, 31, 32,   33, 34, 35 },
        { 36, 37, 38,  39, 40, 41,    42, 43, 44 },
        { 45, 46, 47,  48, 49, 50,   51, 52, 53 },

        { 54, 55, 56,   57, 58, 59,   60, 61, 62 },
        { 63, 64, 65,   66, 67, 68,   69, 70, 71 },
        { 72, 73, 74,   75, 76, 77,   78, 79, 80 }
    };

    // Matriz 9x9 que representa cómo están organizados los cuadrados de la cuadrícula.
    // Cada celda contiene índices que corresponden a números en la cuadrícula.
    public int [,] square_data = new int [9,9]
    {
        { 0, 1, 2,   9, 10, 11,   18, 19, 20 },
        { 3, 4, 5,   12, 13, 14,   21, 22, 23 },
        { 6, 7, 8,   15, 16, 17,   24, 25, 26 },
        { 27, 28, 29,   36, 37, 38,   45, 46, 47 },
        { 30, 31, 32,   39, 40, 41,   48, 49, 50 },
        { 33, 34, 35,   42, 43, 44,   51, 52, 53},
        { 54, 55, 56,   63, 64, 65,   72, 73, 74 },
        { 57, 58, 59,   66, 67, 68,   75, 76, 77 },
        { 60, 61, 62,   69, 70, 71,   78, 79, 80 }
    };

    // Array que contiene los índices de las columnas, del 0 al 8.
    // Este array es 'HideInInspector', lo que significa que no será visible en el editor de Unity.
    [HideInInspector] public int [] columnIndexes = new int [9]
    {
        0, 1, 2, 3, 4, 5, 6, 7, 8
    };

    // Método privado que devuelve la posición (fila, columna) de un cuadrado dentro de 'line_data',
    // dado un índice de cuadrado. El índice pasado como parámetro es el valor que se busca en la matriz.
    private (int, int) GetSquarePosition(int square_index)
    {
        int pos_row = -1;   // Inicializamos la fila como -1 para manejar casos en los que no se encuentre el índice.
        int pos_col = -1;   // Inicializamos la columna como -1 por la misma razón.

        // Recorremos todas las filas y columnas de 'line_data' para encontrar el índice.
        for(int row = 0; row < 9; row++)
        {
            for(int col = 0; col < 9; col++)
            {   
                // Si encontramos el índice en la matriz, actualizamos las variables de posición.
                if(line_data[row, col] == square_index)
                {
                    pos_row = row;
                    pos_col = col;
                }
            }
        }
        // Devolvemos la posición del cuadrado como una tupla (fila, columna).
        return (pos_row, pos_col);
    }

    // Método público que devuelve una línea vertical (columna) a partir de un índice de cuadrado.
    // El índice se usa para determinar la columna en la que está ubicado el cuadrado.
    public int[] GetVerticalLine(int square_index)
    {
        int[] line = new int[9];       // Array que almacenará los índices de la columna correspondiente.

        // Obtenemos la columna en la que se encuentra el cuadrado usando 'GetSquarePosition'.
        var square_position_col = GetSquarePosition(square_index).Item2;

        // Recorremos todas las filas y obtenemos los elementos de la columna correspondiente.
        for(int index = 0; index < 9; index++)
        {
            line[index] = line_data[index, square_position_col];
        }
        // Devolvemos el array que contiene la línea vertical.
        return line;
    }

    // Método público que devuelve el índice de la fila de 'square_data' donde se encuentra un cuadrado específico.
    // Recorremos 'square_data' y devolvemos el índice de la fila que contiene el número pasado como parámetro.
    public int GetGridSqureIndex(int square)
    {   
        // Recorremos todas las filas y columnas de 'square_data'.
        for(int row = 0; row < 9; row++)
        {
            for(int col = 0; col < 9; col++)
            {
                // Si encontramos el número en la matriz, devolvemos el índice de la fila.
                if(square_data[row, col] == square)
                {
                    return row;
                }
            }
        }
        // Si no se encuentra el número, devolvemos -1.
        return  -1;
    }
}
