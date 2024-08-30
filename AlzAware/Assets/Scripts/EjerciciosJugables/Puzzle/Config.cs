using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Config : MonoBehaviour
{
    // Definimos un enumerado llamado SquareColor que representa varios colores posibles para un objeto cuadrado.
    // El primer valor, NotSet, indica que el color no ha sido establecido.
    public enum SquareColor
    {
        NotSet,   
        Red,      
        Blue,     
        Orange,   
        Mint,     
        Yellow,   
        Green,    
        Pink,     
        Purple    
    };
}
