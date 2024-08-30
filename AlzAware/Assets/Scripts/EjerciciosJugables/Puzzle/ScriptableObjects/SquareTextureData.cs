using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Permite crear instancias de este ScriptableObject desde el menú de Unity.
[CreateAssetMenu]
[System.Serializable]
public class SquareTextureData : ScriptableObject
{
      
   [System.Serializable]
   public class TextureData
   {
        public Sprite texture;     // La textura asociada al cuadrado.
        public Config.SquareColor squareColor;    // El color asociado al cuadrado.
   }
   // Valor umbral que determinará cuándo cambiar los colores.
   public int tresholdVal = 10;
   private const int StartTresholdVal = 10; 
   // Lista de texturas activas para los cuadrados.
   public List<TextureData> activeSquareTextures;

   // Variables para almacenar el color actual y el siguiente color.
   public Config.SquareColor currentColor;
   private Config.SquareColor _nextColor;

   // Método que obtiene el índice del color actual en la lista de texturas activas.
   public int GetCurrentColorIndex()
   {
        var currentIndex = 0; 
        // Recorre la lista de texturas activas para encontrar el índice del color actual.
        for (int index = 0; index < activeSquareTextures.Count; index++)
        {
            if(activeSquareTextures[index].squareColor == currentColor)
            {
                currentIndex = index;   // Actualiza el índice cuando se encuentra el color actual.
            }
        }

        return currentIndex;
   }

   // Método que actualiza los colores basados en la puntuación actual.
   public void UpdateColors(int current_score)
   {
        // Actualiza el color actual al siguiente color.
        currentColor = _nextColor;
        // Obtiene el índice del color actual en la lista.
        var currentColorIndex = GetCurrentColorIndex();

        // Si el color actual es el último de la lista, el siguiente color será el primero.
        if(currentColorIndex == activeSquareTextures.Count -1)
            _nextColor = activeSquareTextures[0].squareColor;
        // Si no es el último, el siguiente color será el que le sigue en la lista.
        else
            _nextColor = activeSquareTextures[currentColorIndex +1].squareColor;
        // Actualiza el valor umbral para el próximo cambio de color, sumando la puntuación actual.    
        tresholdVal = StartTresholdVal + current_score;
   }

   // Método que establece el color inicial al comenzar el juego.
   public void SetStartColor()
   {
        tresholdVal = StartTresholdVal; // Reinicia el valor umbral al valor inicial.
        currentColor = activeSquareTextures[0].squareColor; // Establece el primer color como el color inicial.
        _nextColor = activeSquareTextures[1].squareColor;   // Establece el segundo color como el próximo color.
   }
   
   // Método Awake, llamado cuando el ScriptableObject se carga por primera vez.
   private void Awake()
   {
        SetStartColor(); // Establece el color inicial.
   }
   
   // Método OnEnable, llamado cuando el ScriptableObject se activa.
   private void OnEnable()
   {
        SetStartColor(); // Restablece el color inicial cada vez que se activa.
   }
}
