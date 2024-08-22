using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


// Declara que esta clase es un editor personalizado para la clase "ShapeData".
// "CanEditMultipleObjects" permite que se editen múltiples instancias al mismo tiempo.
// "System.Serializable" permite que la clase sea serializable y que su estado se pueda guardar.
[CustomEditor(typeof(ShapeData), false)]
[CanEditMultipleObjects]
[System.Serializable]
public class ShapeDataDrawer : Editor
{
    // Propiedad privada que devuelve la instancia de ShapeData que se está editando en el inspector.
    private ShapeData ShapeDataInstance => target as ShapeData;

    // Método principal que dibuja la interfaz de usuario personalizada en el inspector.
    public override void OnInspectorGUI()
    {
        // Actualiza el estado serializado del objeto para asegurarse de que los datos estén sincronizados.
        serializedObject.Update();
        // Llama al método que dibuja el botón para limpiar el tablero.
        ClearBoardButton();
        // Añade un espacio en la interfaz del inspector.
        EditorGUILayout.Space();
        // Llama al método que dibuja los campos de entrada para las columnas y filas.
        DrawColumnsInputFields();
        // Añade otro espacio en la interfaz del inspector.
        EditorGUILayout.Space();

         // Si el tablero no es nulo y tiene un número válido de columnas y filas, dibuja la tabla del tablero.
        if (ShapeDataInstance.board != null && ShapeDataInstance.columns > 0 && ShapeDataInstance.rows > 0)
        {
            DrawBoardTable();
        }

        // Aplica los cambios hechos a las propiedades serializadas.
        serializedObject.ApplyModifiedProperties();

        // Si hubo algún cambio en la interfaz, marca el objeto ShapeData como modificado para que Unity lo guarde.
        if (GUI.changed)
        {
            EditorUtility.SetDirty(ShapeDataInstance);
        }
    }
    // Método que dibuja un botón en el inspector para limpiar el tablero.
    private void ClearBoardButton()
    {
        // Si se hace clic en el botón "Clear Board", llama al método Clear() en la instancia de ShapeData.
        if (GUILayout.Button("Clear Board"))
        {
            ShapeDataInstance.Clear();
        }
    }


    // Método que dibuja los campos de entrada para el número de columnas y filas.
    private void DrawColumnsInputFields()
    {
        // Guarda los valores actuales de columnas y filas.
        var columnsTemp = ShapeDataInstance.columns;
        var rowsTemp = ShapeDataInstance.rows;

        // Dibuja campos de entrada para modificar el número de columnas y filas.
        ShapeDataInstance.columns = EditorGUILayout.IntField("Columns", ShapeDataInstance.columns);
        ShapeDataInstance.rows = EditorGUILayout.IntField("Rows", ShapeDataInstance.rows);

        // Si el número de columnas o filas ha cambiado y ambos son mayores que 0, se crea un nuevo tablero.
        if ((ShapeDataInstance.columns != columnsTemp || ShapeDataInstance.rows != rowsTemp) &&
            ShapeDataInstance.columns > 0 && ShapeDataInstance.rows > 0)
        {
            ShapeDataInstance.CreateNewBoard();
        }
    }

    // Método que dibuja una tabla que representa el tablero.
    private void DrawBoardTable()
    {
        // Define el estilo para la tabla en el inspector.
        var tableStyle = new GUIStyle("box");
        tableStyle.padding = new RectOffset(10, 10, 10, 10);
        tableStyle.margin.left = 32;

        // Define el estilo para las columnas de encabezado de la tabla.
        var headerColumnStyle = new GUIStyle();
        headerColumnStyle.fixedWidth = 65;
        headerColumnStyle.alignment = TextAnchor.MiddleCenter;

        // Define el estilo para las filas de la tabla.
        var rowStyle = new GUIStyle();
        rowStyle.fixedHeight = 25;
        rowStyle.alignment = TextAnchor.MiddleCenter;

        // Define el estilo para los campos de datos de la tabla.
        var dataFieldStyle = new GUIStyle(EditorStyles.miniButtonMid);
        dataFieldStyle.normal.background = Texture2D.grayTexture;
        dataFieldStyle.onNormal.background = Texture2D.whiteTexture;

        // Itera sobre cada fila del tablero.
        for (var row = 0; row < ShapeDataInstance.rows; row++)
        {
            // Comienza una nueva fila en el diseño horizontal.
            EditorGUILayout.BeginHorizontal(headerColumnStyle);

            // Itera sobre cada columna dentro de la fila actual.
            for (var column = 0; column < ShapeDataInstance.columns; column++)
            {
                // Comienza un nuevo campo horizontal para cada celda.
                EditorGUILayout.BeginHorizontal(rowStyle);
                // Dibuja un campo de tipo toggle (checkbox) para cada celda en la tabla.
                // Almacena el valor del toggle en el tablero.
                var data = EditorGUILayout.Toggle(ShapeDataInstance.board[row].column[column], dataFieldStyle);
                ShapeDataInstance.board[row].column[column] = data;

                // Termina el campo horizontal para la celda actual.
                EditorGUILayout.EndHorizontal();
            }
            // Termina la fila en el diseño horizontal.
            EditorGUILayout.EndHorizontal();
        }
    }
}