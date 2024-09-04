using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu]

public class DatosTablero : ScriptableObject
{

    [System.Serializable]
   
    //Representa la palabra que se busca en la pizarra
    public class SearchingWord
    {
        [HideInInspector]
        public bool Found = false;
        public string Word;
    }
    [System.Serializable]
    //Representa una fila de la pizarra
    public class BoardRow
    {
        public int Size;
        public string[] Row;

        public BoardRow() { }
        public BoardRow(int size)
        {
            CreateRow(size);

        }
        public void CreateRow(int size)
        {
            Size = size;
            Row = new string[Size];
            ClearRow();

        }
        public void ClearRow()
        {
            for (int i = 0; i < Size; i++)
            {
                Row[i] = " ";
            }
        }

    }
    //Establece a 0 el numero de columnas y filas de la pizarra
    public int Columns = 0;
    public int Rows = 0;
    //Representa la pizarra
    public BoardRow[] Board;
    //Crea una lista para añadir las palabras de las sopa de letras
    public List<SearchingWord> SearchWords = new List<SearchingWord>();

    //Metodo que limpia la pìzarra si al recorrer la lista Searchword no se ha encontrado ninguna palabra
    public void ClearData()
    {
        foreach (var word in SearchWords)
        {
            word.Found = false;
        }
    }
    //Metodo que recorre toda las filas de la pizarra y llama al metodo ClearRow()
    public void ClearWithEmptyString()
    {
        for (int i = 0; i < Columns; i++)
        {
            Board[i].ClearRow();
        }
    }
    //Metodo para inicializar una nueva pìzarra con el numero de columnas especificado.
    public void CreateNewBoard()
    {
        Board = new BoardRow[Columns];
        for (int i = 0; i < Columns; i++)
        {
            Board[i] = new BoardRow(Rows);
        }
    }
}
