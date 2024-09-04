using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu]

public class DatosJuego : ScriptableObject
{
    //Se le asigna a cada pizarra un categoria de nombre y se asocia con la pizarra correcta en los datos del tablero
    public string selectedCategoryName;
    public DatosTablero selectedBoardData;
}
