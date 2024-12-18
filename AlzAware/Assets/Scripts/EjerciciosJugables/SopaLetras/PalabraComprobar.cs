using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.SceneManagement;
using static EventosJuego;

public class PalabraComprobar : MonoBehaviour
{
    public DatosTablero currentGameData;
    private string _word;
    private HashSet<int> completedBoards = new HashSet<int>();

    private int _assignedPoints = 0;
    private int _completedWords = 0;
    private Ray _rayUp, _rayDown;
    private Ray _rayLeft, _rayRight;
    private Ray _rayDiagonalLeftUp, _rayDiagonalLeftDown;
    private Ray _rayDiagonalRightUp, _rayDiagonalRightDown;
    private Ray _currentRay = new Ray();
    private Vector3 _rayStartPosition;
    private List<int> _correctSquareList = new List<int>();

    private SQLiteManager dbManager;
    private Contador contador;

    private void OnEnable()
    {
        EventosJuego.OnCheckSquare += SquareSelected;
        EventosJuego.OnClearSelection += ClearSelection;
    }

    private void OnDisable()
    {
        EventosJuego.OnCheckSquare -= SquareSelected;
        EventosJuego.OnClearSelection -= ClearSelection;
    }

    void Start()
    {
        currentGameData.ClearData();
        _assignedPoints = 0;
        _completedWords = 0;

        dbManager = FindObjectOfType<SQLiteManager>();
        contador = FindObjectOfType<Contador>();
    }

    void Update()
    {
        if (_assignedPoints > 0 && Application.isEditor)
        {
            Debug.DrawRay(_rayUp.origin, _rayUp.direction * 4);
            Debug.DrawRay(_rayDown.origin, _rayDown.direction * 4);
            Debug.DrawRay(_rayLeft.origin, _rayLeft.direction * 4);
            Debug.DrawRay(_rayRight.origin, _rayRight.direction * 4);
            Debug.DrawRay(_rayDiagonalLeftUp.origin, _rayDiagonalLeftUp.direction * 4);
            Debug.DrawRay(_rayDiagonalLeftDown.origin, _rayDiagonalLeftDown.direction * 4);
            Debug.DrawRay(_rayDiagonalRightUp.origin, _rayDiagonalRightUp.direction * 4);
            Debug.DrawRay(_rayDiagonalRightDown.origin, _rayDiagonalRightDown.direction * 4);
        }
    }

    private void SquareSelected(string letter, Vector3 squarePosition, int squareIndex)
    {
        if (_assignedPoints == 0)
        {
            _rayStartPosition = squarePosition;
            _correctSquareList.Add(squareIndex);
            _word += letter;

            _rayUp = new Ray(new Vector2(squarePosition.x, squarePosition.y), new Vector2(0f, 1));
            _rayDown = new Ray(new Vector2(squarePosition.x, squarePosition.y), new Vector2(0f, -1));
            _rayLeft = new Ray(new Vector2(squarePosition.x, squarePosition.y), new Vector2(-1, 0f));
            _rayRight = new Ray(new Vector2(squarePosition.x, squarePosition.y), new Vector2(1, 0f));
            _rayDiagonalLeftUp = new Ray(new Vector2(squarePosition.x, squarePosition.y), new Vector2(-1, 1));
            _rayDiagonalLeftDown = new Ray(new Vector2(squarePosition.x, squarePosition.y), new Vector2(-1, -1));
            _rayDiagonalRightUp = new Ray(new Vector2(squarePosition.x, squarePosition.y), new Vector2(1, 1));
            _rayDiagonalRightDown = new Ray(new Vector2(squarePosition.x, squarePosition.y), new Vector2(1, -1));
        }
        else if (_assignedPoints == 1)
        {
            _correctSquareList.Add(squareIndex);
            _currentRay = SelectRay(_rayStartPosition, squarePosition);
            EventosJuego.SelectSquareMethod(squarePosition);
            _word += letter;
            CheckWord();
        }
        else
        {
            if (IsPointOnTheRay(_currentRay, squarePosition))
            {
                _correctSquareList.Add(squareIndex);
                EventosJuego.SelectSquareMethod(squarePosition);
                _word += letter;
                CheckWord();
            }
        }

        _assignedPoints++;
    }

    private void CheckWord()
    {
        foreach (var searchingWord in currentGameData.SearchWords)
        {
            if (_word == searchingWord.Word && searchingWord.Found == false)
            {
                searchingWord.Found = true;
                EventosJuego.CorrectWordMethod(_word, _correctSquareList);
                _completedWords++;
                _word = string.Empty;
                _correctSquareList.Clear();
                CheckBoardCompleted();
                return;
            }
        }
    }

    private bool IsPointOnTheRay(Ray currentRay, Vector3 point)
    {
        var hits = Physics.RaycastAll(currentRay, 100.0f);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].transform.position == point)
                return true;
        }
        return false;
    }

    private Ray SelectRay(Vector2 firstPosition, Vector2 secondPosition)
    {
        var direction = (secondPosition - firstPosition).normalized;
        float tolerance = 0.01f;
        if (Mathf.Abs(direction.x) < tolerance && Mathf.Abs(direction.y - 1f) < tolerance) return _rayUp;
        if (Mathf.Abs(direction.x) < tolerance && Mathf.Abs(direction.y - (-1f)) < tolerance) return _rayDown;
        if (Mathf.Abs(direction.x - (-1f)) < tolerance && Mathf.Abs(direction.y) < tolerance) return _rayLeft;
        if (Mathf.Abs(direction.x - 1f) < tolerance && Mathf.Abs(direction.y) < tolerance) return _rayRight;
        if (direction.x < 0f && direction.y > 0f) return _rayDiagonalLeftUp;
        if (direction.x < 0f && direction.y < 0f) return _rayDiagonalLeftDown;
        if (direction.x > 0f && direction.y > 0f) return _rayDiagonalRightUp;
        if (direction.x > 0f && direction.y < 0f) return _rayDiagonalRightDown;
        return _rayDown;
    }

    private void ClearSelection()
    {
        _assignedPoints = 0;
        _correctSquareList.Clear();
        _word = string.Empty;
    }

    private void CheckBoardCompleted()
    {
        if (currentGameData.SearchWords.Count == _completedWords)
        {
            Debug.Log("Juego completado, guardando estadísticas...");

            // Detener el temporizador
            contador.DetenerTemporizador();

            // Obtener valores
            int idUsuario = ObtenerIdUsuario(); // ID del usuario activo
            int idEjercicio = 1; // ID del ejercicio (Sopa de Letras)
            int puntuacion = CalcularPuntuacion(contador.ObtenerTiempoActual());
            string fecha = DateTime.Now.ToString("dd/MM/yyyy");

            // Guardar estadísticas en la base de datos
            dbManager.InsertEstadistica(idUsuario, idEjercicio, puntuacion, fecha);

            // Cargar escena
            SceneManager.LoadScene("EstadisticasEj1");
        }
    }

    private int ObtenerIdUsuario()
    {
        return PlayerPrefs.GetInt("UsuarioActivoID", 0); // Obtiene el ID del usuario activo
    }

    private int CalcularPuntuacion(int tiempoSegundos)
    {
        // Tiempo mínimo y máximo en segundos
        int tiempoMinimo = 180; // 3:00
        int tiempoMaximo = 600; // 10:00

        // Si el tiempo es menor o igual al mínimo, puntuación máxima
        if (tiempoSegundos <= tiempoMinimo)
        {
            return 100;
        }
        // Si el tiempo es mayor o igual al máximo, puntuación mínima
        if (tiempoSegundos >= tiempoMaximo)
        {
            return 10;
        }

        // Cálculo proporcional de la puntuación
        float puntuacion = 100 - ((float)(tiempoSegundos - tiempoMinimo) / (tiempoMaximo - tiempoMinimo) * 90);
        return Mathf.RoundToInt(puntuacion);
    }
}
