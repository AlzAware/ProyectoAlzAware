using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeStorage : MonoBehaviour
{
    // Lista que contiene diferentes datos de formas, probablemente configurados como ScriptableObjects.
    public List<ShapeData> shapeData;
    
    // Lista que contiene las instancias de las formas que se van a utilizar en el juego.
    public List<Shape> shapeList;

    // Este método se llama automáticamente cuando el GameObject se habilita.
    // Suscribe el método `RequestNewShapes` al evento `RequestNewShapes` de `GameEvents`.
    private void OnEnable()
    {
        GameEvents.RequestNewShapes += RequestNewShapes;
    }

    // Este método se llama automáticamente cuando el GameObject se deshabilita.
    // Desuscribe el método `RequestNewShapes` del evento `RequestNewShapes` de `GameEvents`.
    private void OnDisable()
    {
        GameEvents.RequestNewShapes -= RequestNewShapes;
    }

    // Método Start, llamado al inicio del ciclo de vida del GameObject.
    void Start()
    {
        // Para cada forma en la lista `shapeList`, selecciona aleatoriamente un ShapeData de `shapeData`
        // y crea la forma usando esos datos.
        foreach (var shape in shapeList)
        {
            var shapeIndex = UnityEngine.Random.Range(0, shapeData.Count);
            shape.CreateShape(shapeData[shapeIndex]); 
        }
    }

    // Método que obtiene la forma actualmente seleccionada en el juego.
    public Shape GetCurrentSelectedShape()
    {
        // Recorre todas las formas en `shapeList` y devuelve la primera que no esté en su posición inicial
        // y que tenga al menos un cuadrado activo.
        foreach (var shape in shapeList)
        {
            if(shape.IsOnStartPosition() == false && shape.IsAnyOfShapeSquareActive())
                return shape;
        }

        // Si no se encuentra ninguna forma seleccionada, muestra un mensaje de error.
        Debug.LogError("There is no shape selected!");
        return null;
    }

    // Método que maneja la solicitud de nuevas formas.
    // Asignar aleatoriamente nuevos datos a cada forma en `shapeList`.
    private void RequestNewShapes()
    {
        foreach (var shape in shapeList)
        {
            var shapeIndex = UnityEngine.Random.Range(0, shapeData.Count);
            shape.RequestNewShape(shapeData[shapeIndex]);
        }
    }
}
