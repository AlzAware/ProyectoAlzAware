using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    // Evento que se activa cuando el juego termina. 
    // El parámetro booleano indica si el juego ha terminado con éxito (true) o no (false).
    public static Action<bool> GameOver;

    // Evento que se activa para añadir puntuaciones al total del jugador.
    // El parámetro entero representa la cantidad de puntos a añadir.
    public static Action<int> AddScores;

    // Evento que se activa para verificar si una forma puede ser colocada en el tablero.
    // No tiene parámetros porque solo se utiliza para desencadenar la verificación.
    public static Action CheckIfShapeCanBePlaced;

    // Evento que se activa para mover una forma a su posición inicial.
    // Esto suele ocurrir cuando se reinicia la posición de una forma después de que no ha sido colocada en el tablero.
    public static Action MoveShapeToStartPosition;

    // Evento que se activa para solicitar la generación de nuevas formas.
    // Esto se utiliza, por ejemplo, cuando el jugador ha colocado todas las formas actuales y necesita nuevas.
    public static Action RequestNewShapes;

    // Evento que se activa para desactivar una forma, por ejemplo, cuando se ha colocado en una posición válida en el tablero.
    // Esto puede implicar deshabilitar la forma visualmente o hacerla no interactiva.
    public static Action SetShapeInactive;

    // Evento que se activa para actualizar el color de los cuadrados en el juego.
    // El parámetro es un valor del enum `Config.SquareColor`, que representa el nuevo color a aplicar.
    public static Action<Config.SquareColor> UpdateSquareColor;
}

