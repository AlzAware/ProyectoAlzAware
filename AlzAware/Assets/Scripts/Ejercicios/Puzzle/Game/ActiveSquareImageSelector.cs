using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActiveSquareImageSelector : MonoBehaviour
{
    // Objeto que contiene los datos de las texturas del cuadrado.
    public SquareTextureData squareTextureData;

    // Booleano para indicar si se debe actualizar la imagen cuando se alcance un cierto umbral de puntos.
    public bool updateImageOnRechedTreshold = false;

    // Método llamado cuando el objeto se habilita en la escena o se activa.
    private void OnEnable()
    {
        // Actualiza el color del cuadrado según los puntos actuales al activarse el objeto.
        UpdateSquareColorBaseOnCurrentPoints();

        // Si 'updateImageOnRechedTreshold' está activada, se suscribe al evento de actualización del color del cuadrado.
        if (updateImageOnRechedTreshold)
            GameEvents.UpdateSquareColor += UpdateSquareColor;
    }
    // Método llamado cuando el objeto se deshabilita o se destruye.
    private void OnDisable()
    {   
        // Si está activada, se desuscribe del evento de actualización de color para evitar posibles errores o llamadas no deseadas.
        if (updateImageOnRechedTreshold)
            GameEvents.UpdateSquareColor -= UpdateSquareColor;
    }

    // Método para actualizar el color del cuadrado según los puntos actuales.
    // Este método recorre las texturas activas y selecciona la que coincida con el color actual.
    private void UpdateSquareColorBaseOnCurrentPoints()
    {
        // Recorremos todas las texturas activas del cuadrado.
        foreach (var squareTexture in squareTextureData.activeSquareTextures)
        {
            // Si el color actual del cuadrado coincide con el color de la textura, actualizamos la imagen (sprite) del cuadrado.
            if (squareTextureData.currentColor == squareTexture.squareColor)
            {
                // Actualizamos el componente de la imagen del cuadrado con la textura correspondiente.
                GetComponent<Image>().sprite = squareTexture.texture;
            }
        }
    }

    // Método que actualiza el color del cuadrado cuando se recibe el evento de cambio de color.
    // Este método se suscribe al evento y actualiza la imagen del cuadrado basado en el nuevo color recibido.
    private void UpdateSquareColor(Config.SquareColor color)
    {
        // Recorremos todas las texturas activas del cuadrado.
        foreach (var squareTexture in squareTextureData.activeSquareTextures)
        {   
            // Si el nuevo color coincide con el color de la textura, actualizamos la imagen del cuadrado.
            if (color == squareTexture.squareColor)
            {
                // Actualizamos el componente de la imagen con la textura correspondiente al nuevo color.
                GetComponent<Image>().sprite = squareTexture.texture;
            }
        }
    }
 
}
