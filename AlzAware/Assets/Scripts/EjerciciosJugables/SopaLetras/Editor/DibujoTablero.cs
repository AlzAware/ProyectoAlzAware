using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine.TextCore.Text;
using System.Text.RegularExpressions;


[CustomEditor(typeof(DatosTablero), false)]
[CanEditMultipleObjects]
[System.Serializable]
/*Clase que dibuja en el editor un tablero con cuadrado imitando a la sopa de letras final que aparece en el juego. Contiene: zonas para indicar el numero de columnas y filas para crear la pizarra,
//Un boton para convertir las letras en mayusculas, un boton para rellenar aleatoriamente las celdas vacias, un boton para limpiar la pizarra y la creaccion de una lista donde se puede añadir las
palabras que queremos localizar en la sopa de letra*/
public class DibujoTablero : Editor
{
    private DatosTablero GameDataInstance => target as DatosTablero;
    private ReorderableList _dataList;

    private void OnEnable()
    {
        InitializeReordableList(ref _dataList, "SearchWords", "Searching Words");

    }

    //Metodo que coloca en el inspector los elementos creados(seleccion de columnas y filas, botones,etc)
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        DrawColumnsRowsInputFields();
        EditorGUILayout.Space();
        ConvertToUpperButton();

        if (GameDataInstance.Board != null && GameDataInstance.Columns > 0 && GameDataInstance.Rows > 0)
            DrawBoardTable();

        GUILayout.BeginHorizontal();

        ClearBoardButton();
        FillUpWithRandomLettersButton();

        GUILayout.EndHorizontal();

        EditorGUILayout.Space();
        _dataList.DoLayoutList();

        serializedObject.ApplyModifiedProperties();
        if (GUI.changed)
        {
            EditorUtility.SetDirty(GameDataInstance);
        }
    }
    //Metodo que crea las lineas para poder añadir las columnas y filas que queramos
    private void DrawColumnsRowsInputFields()
    {
        var columnsTemp = GameDataInstance.Columns;
        var rowsTemp = GameDataInstance.Rows;

        GameDataInstance.Columns = EditorGUILayout.IntField("Columns", GameDataInstance.Columns);
        GameDataInstance.Rows = EditorGUILayout.IntField("Rows", GameDataInstance.Rows);

        if ((GameDataInstance.Columns != columnsTemp || GameDataInstance.Rows != rowsTemp)
            && (GameDataInstance.Columns > 0 && GameDataInstance.Rows > 0))
        {

            GameDataInstance.CreateNewBoard();
        }
    }
    //Metodo para dibujar la pizarra en el editor
    private void DrawBoardTable()
    {
        var tableStyle = new GUIStyle("box");
    tableStyle.padding = new RectOffset(10, 10, 10, 10);
    tableStyle.margin.left = 32;

        var headerColumnStyle = new GUIStyle();
    headerColumnStyle.fixedWidth = 35;

        var columnStyle = new GUIStyle();
    columnStyle.fixedWidth = 50;

        var rowStyle = new GUIStyle();
    rowStyle.fixedHeight = 25;
        rowStyle.fixedWidth = 40;
        rowStyle.alignment = TextAnchor.MiddleCenter;

        var textFieldStyle = new GUIStyle();
    textFieldStyle.normal.background =Texture2D.grayTexture;
        textFieldStyle.normal.textColor = Color.white;
        textFieldStyle.alignment = TextAnchor.MiddleCenter; 

        EditorGUILayout.BeginHorizontal(tableStyle);

        for (var x=0; x<GameDataInstance.Columns; x++)
        {
            EditorGUILayout.BeginVertical(x == -1 ? headerColumnStyle : columnStyle);
            for (var y =0; y<GameDataInstance.Rows; y++)
            {
                if(x >= 0 && y >= 0)
                {
                    EditorGUILayout.BeginHorizontal(rowStyle);
                    var character = (string)EditorGUILayout.TextArea(GameDataInstance.Board[x].Row[y], textFieldStyle);
                    if (GameDataInstance.Board[x].Row[y].Length > 1)
                    {
                        character= GameDataInstance.Board[x].Row[y].Substring(0,1);
                    }
                    GameDataInstance.Board[x].Row[y] = character;
                    EditorGUILayout.EndHorizontal();    
                }
            }
            EditorGUILayout.EndVertical();
        }
        EditorGUILayout.EndHorizontal();              
    }

    //Metodo para inicializar y configurar la lista de los elementos en el inspector 
    private void InitializeReordableList(ref ReorderableList list, string propertyName, string listLabel)
    {
        list = new ReorderableList(serializedObject, serializedObject.FindProperty(propertyName),
            true, true, true, true);

        list.drawHeaderCallback = (Rect rect) =>
        {
            EditorGUI.LabelField(rect, listLabel);
        };
        var l = list;
        list.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
        {
            var element = l.serializedProperty.GetArrayElementAtIndex(index);
            rect.y += 2;

            EditorGUI.PropertyField(
                new Rect(rect.x, rect.y, EditorGUIUtility.labelWidth, EditorGUIUtility.singleLineHeight),
                element.FindPropertyRelative("Word"), GUIContent.none);
        };
    }
    //Metodo que crea un boton para convertir en mayusculas las letras
    private void ConvertToUpperButton()
    {
        if (GUILayout.Button("To Upper"))
        {
            for (var i = 0; i < GameDataInstance.Columns; i++)
            {
                for (var j = 0; j < GameDataInstance.Rows; j++)
                {
                    var errorCounter = Regex.Matches(GameDataInstance.Board[i].Row[j], @"[a-z]").Count;
                    if (errorCounter > 0)
                        GameDataInstance.Board[i].Row[j] = GameDataInstance.Board[i].Row[j].ToUpper();

                }
            }
            foreach (var searchWord in GameDataInstance.SearchWords)
            {
                var errorCounter = Regex.Matches(searchWord.Word, @"[a-z]").Count;

                if (errorCounter > 0)
                {
                    searchWord.Word = searchWord.Word.ToUpper();
                }

            }
        }
    }
    //Metodo que crea un boton para limpiar la pizarra
    private void ClearBoardButton()
    {
        if (GUILayout.Button("Clear Board"))
        {
            for (int i = 0; i < GameDataInstance.Columns; i++)
            {
                for (int j = 0; j < GameDataInstance.Rows; j++)
                {
                    GameDataInstance.Board[i].Row[j] = " ";
                }
            }
        }
    }
    //Metodo que crea un boton para rellenas las celdas vacias con letras aleatorias.
    private void FillUpWithRandomLettersButton()
    {
        if (GUILayout.Button("Fill Up With Random"))
        {
            for (int i = 0; i < GameDataInstance.Columns; i++)
            {
                for (int j = 0; j < GameDataInstance.Rows; j++)
                {
                    int errorCounter = Regex.Matches(GameDataInstance.Board[i].Row[j], @"[a-zA-Z]").Count;
                    string letters = "ABCDEFGHIJKLMNOPQRSTUVWYZ";
                    int index = UnityEngine.Random.Range(0, letters.Length);

                    if (errorCounter == 0)
                    {
                        GameDataInstance.Board[i].Row[j] = letters[index].ToString();
                    }
                }
            }
        }
    }
}
