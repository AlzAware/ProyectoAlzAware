using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridSquare : MonoBehaviour
{

    // Referencias a las imágenes asociadas al cuadrado de la cuadrícula
    public Image hooverImage;   //Imagen que aparece cuando se pasa el ratón por encima
    public Image activeImage;   // Imagen que se muestra cuando el cuadrado está activado
    public Image normalImage;   // Imagen normal del cuadrado
    public List<Sprite> normalImages;   // Lista de sprites para las imágenes normales

    // Variable que guarda el color actual del cuadrado
    private Config.SquareColor currentSquareColor_ = Config.SquareColor.NotSet;

    // Devuelve el color actual del cuadrado
    public Config.SquareColor GetCurrentColor()
    {
        return currentSquareColor_;
    }

    // Propiedades públicas para saber si el cuadrado está seleccionado o ocupado
    public bool Selected { get; set; }
    public int SquareIndex { get; set; }    // Índice del cuadrado en la cuadrícula
    public bool SquareOccupied { get; set; }    // Indica si el cuadrado está ocupado


    // Inicializa las variables al inicio del juego
    void Start()
    {
        Selected = false;
        SquareOccupied = false; 
    }

    // Función temporal para verificar si se puede usar este cuadrado
    // Basada en si la imagen hoover está activa
    public bool CanWeUseThisSquare()
    {
        return hooverImage.gameObject.activeSelf;
    }

    // Coloca una figura en el tablero cambiando el color del cuadrado
    public void PlaceShapeOnBoard(Config.SquareColor color)
    {
        
        currentSquareColor_ = color;    // Establece el color
        ActivateSquare();   // Activa el cuadrado si no está ocupado
        
    }

    // Activa el cuadrado mostrando la imagen activa y marcándolo como ocupado
    public void ActivateSquare()
    {
        hooverImage.gameObject.SetActive(false);    // Desactiva la imagen de hover
        activeImage.gameObject.SetActive(true);     // Activa la imagen activa
        Selected = true;    // Marca el cuadrado como seleccionado
        SquareOccupied = true;  // Marca el cuadrado como ocupado
    }

    // Desactiva el cuadrado y resetea su color
    public void Deactivate()
    {
        currentSquareColor_ = Config.SquareColor.NotSet;    // Restaura el color por defecto
        activeImage.gameObject.SetActive(false);    // Desactiva la imagen activa
    }

    // Limpia la ocupación del cuadrado y lo resetea
    public void ClearOccupied()
    {
        currentSquareColor_ = Config.SquareColor.NotSet;    // Restaura el color por defecto
        Selected = false;   // Marca el cuadrado como no seleccionado
        SquareOccupied = false;     // Lo marca como no ocupado
    }
    
    // Cambia la imagen del cuadrado dependiendo del valor del parámetro
    public void SetImage(bool setFirstImage)
    {
        // Si setFirstImage es verdadero, selecciona el segundo sprite de la lista, 
        // de lo contrario, selecciona el primero
        normalImage.GetComponent<Image>().sprite = setFirstImage ? normalImages[1] : normalImages[0];
    }

    // Evento que ocurre cuando otro objeto entra en contacto con el cuadrado
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (SquareOccupied == false)
        {
            Selected = true;
            hooverImage.gameObject.SetActive(true);
        }
        else if (collision.GetComponent<ShapeSquare>() != null)
        {
            collision.GetComponent<ShapeSquare>().SetOccupied();
        }
    }

    // Evento que ocurre mientras otro objeto se mantiene en contacto con el cuadrado
    private void OnTriggerStay2D(Collider2D collision)
    {
        Selected = true;

        if (SquareOccupied == false)
        {
            hooverImage.gameObject.SetActive(true);
        }
        else if (collision.GetComponent<ShapeSquare>() != null)
        {
            collision.GetComponent<ShapeSquare>().SetOccupied();
        }
    }

    // Evento que ocurre cuando otro objeto sale del contacto con el cuadrado
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (SquareOccupied == false)
        {
            Selected = false;
            hooverImage.gameObject.SetActive(false);
        }
        else if (collision.GetComponent<ShapeSquare>() != null)
        {
            collision.GetComponent<ShapeSquare>().UnSetOccupied();
        }
    }
}