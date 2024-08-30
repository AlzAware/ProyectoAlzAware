using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShapeSquare : MonoBehaviour
{
    // Referencia a la imagen que indica si el cuadrado está ocupado.
    public Image occupiedImage;

    // Método Start, llamado al inicio del ciclo de vida del GameObject.
    void Start()
    {
        // Desactiva la imagen ocupada al iniciar el juego.
        occupiedImage.gameObject.SetActive(false);
    }

    // Método para desactivar el cuadrado de la forma.
    public void DeactivateShape()
    {
        // Desactiva el BoxCollider2D para que el cuadrado no pueda interactuar.
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        // Desactiva el GameObject, ocultando el cuadrado.
        gameObject.SetActive(false);
    }

    // Método para activar el cuadrado de la forma.
    public void ActivateShape()
    {
        // Activa el BoxCollider2D para que el cuadrado pueda interactuar.
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        // Activa el GameObject, mostrando el cuadrado.
        gameObject.SetActive(true);
    }

    // Método para marcar el cuadrado como ocupado.
    public void SetOccupied()
    {
        // Activa la imagen ocupada para indicar que este cuadrado está ocupado.
        occupiedImage.gameObject.SetActive(true);
    }

    // Método para desmarcar el cuadrado como ocupado.
    public void UnSetOccupied()
    {
        // Desactiva la imagen ocupada para indicar que este cuadrado ya no está ocupado.
        occupiedImage.gameObject.SetActive(false);
    }
}
