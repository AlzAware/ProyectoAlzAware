using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// La clase Shape maneja la lógica de un objeto de forma en un juego. Implementa varias interfaces para manejar 
// eventos de puntero y arrastre.
public class Shape : MonoBehaviour, IPointerClickHandler, IPointerUpHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler
{
    // Referencia al objeto de imagen que representa un cuadrado dentro de la forma.
    public GameObject squareShapeImage;
    // Escala que tomará la forma cuando sea seleccionada.
    public Vector3 shapeSelectedScale;
    // Desplazamiento aplicado a la posición de la forma durante el arrastre.
    public Vector2 offset = new Vector2(0f, 700f);

    // Almacena los datos actuales de la forma.
    [HideInInspector]
    public ShapeData CurrentShapeData;

    // Propiedad que almacena el número total de cuadrados en la forma.
    public int TotalSquareNumber { get; set; }

    // Lista que almacena los objetos de los cuadrados que forman la forma.
    private List<GameObject> _currentShape = new List<GameObject>();
    // Escala inicial de la forma.
    private Vector3 _shapeStartScale;
    // Referencia al RectTransform de la forma.
    private RectTransform _transform;
    // Indica si la forma es arrastrable.
    private bool _shapeDraggable = true;
    // Referencia al canvas padre.
    private Canvas _canvas;
    // Posición inicial de la forma.
    private Vector3 _startPosition;
    // Indica si la forma está activa.
    private bool _shapeActive = true;

    // Método llamado al iniciar la escena.
    public void Awake()
    {
        // Guarda la escala inicial de la forma.
        _shapeStartScale = this.GetComponent<RectTransform>().localScale;
        // Guarda la referencia al RectTransform.
        _transform = this.GetComponent<RectTransform>();
        // Obtiene el Canvas padre.
        _canvas = GetComponentInParent<Canvas>();
        // Marca la forma como arrastrable.
        _shapeDraggable = true;
        // Almacena la posición inicial de la forma.
        _startPosition = _transform.localPosition;
        // Marca la forma como activa.
        _shapeActive = true;
    }

    // Método llamado cuando el objeto se habilita.
    private void OnEnable()
    {
        // Suscribirse a eventos para mover la forma a la posición inicial y desactivarla.
        GameEvents.MoveShapeToStartPosition += MoveShapeToStartPosition;
        GameEvents.SetShapeInactive += SetShapeInactive;
    }

    // Método llamado cuando el objeto se deshabilita.
    private void OnDisable()
    {
        // Desuscribirse de los eventos.
        GameEvents.MoveShapeToStartPosition -= MoveShapeToStartPosition;
        GameEvents.SetShapeInactive -= SetShapeInactive;
    }

    // Verifica si la forma está en su posición inicial.
    public bool IsOnStartPosition()
    {
        return _transform.localPosition == _startPosition;
    }

    // Verifica si al menos un cuadrado de la forma está activo.
    public bool IsAnyOfShapeSquareActive()
    {
        foreach (var square in _currentShape)
        {
            if(square.gameObject.activeSelf)
                return true;
        }
        return false;
    }

    // Desactiva la forma y sus cuadrados.
    public void DeactivateShape()
    {
        if(_shapeActive)
        {
            foreach (var square in _currentShape)
            {
                square?.GetComponent<ShapeSquare>().DeactivateShape();
            }
        }
        _shapeActive = false;
    }

    // Método que desactiva la forma si no está en la posición inicial y tiene cuadrados activos.
    private void SetShapeInactive()
    {
        if(IsOnStartPosition() == false && IsAnyOfShapeSquareActive())
        {
            foreach (var square in _currentShape)
            {
                square.gameObject.SetActive(false);
            }
        }
    }

    // Activa la forma y sus cuadrados.
    public void ActivateShape()
    {
        if(!_shapeActive)
        {
            foreach (var square in _currentShape)
            {
                square?.GetComponent<ShapeSquare>().ActivateShape();
            }
        }
        _shapeActive = true;
    }

    // Solicita una nueva forma y la posiciona en la posición inicial.
    public void RequestNewShape(ShapeData shapeData)
    {
        _transform.localPosition = _startPosition;
        CreateShape(shapeData);
    }

    // Crea la forma basada en los datos de forma proporcionados.
    public void CreateShape(ShapeData shapeData)
    {
        CurrentShapeData = shapeData;
        TotalSquareNumber = GetNumberOfSquares(shapeData);

        // Asegura que la lista tenga suficientes elementos para representar la forma.
        while (_currentShape.Count <= TotalSquareNumber)
        {
            _currentShape.Add(Instantiate(squareShapeImage, transform) as GameObject);
        }

        // Inicializa cada cuadrado en la lista de la forma.
        foreach (var square in _currentShape)
        {
            square.transform.position = Vector3.zero;
            square.SetActive(false);
        }

        var squareRect = squareShapeImage.GetComponent<RectTransform>();
        var moveDistance = new Vector2(squareRect.rect.width * squareShapeImage.transform.localScale.x, 
        squareRect.rect.height * squareShapeImage.transform.localScale.y);

        int currentIndexInList = 0;

        // Verifica que las filas no excedan la longitud de la matriz del tablero.
        if (shapeData.rows > shapeData.board.Length)
        {
            Debug.LogError("ShapeData rows exceed board length.");
            return;
        }

        // Itera sobre las filas y columnas de la matriz del tablero para activar los cuadrados necesarios.
        for (var row = 0; row < shapeData.rows; row++)
        {
            if (shapeData.columns > shapeData.board[row].column.Length)
            {
                Debug.LogError("ShapeData columns exceed board row length.");
                return;
            }

            for (var column = 0; column < shapeData.columns; column++)
            {
                if (shapeData.board[row].column[column])
                {
                    if (currentIndexInList < _currentShape.Count)
                    {
                        // Activa y posiciona el cuadrado en su lugar correspondiente.
                        _currentShape[currentIndexInList].SetActive(true);
                        _currentShape[currentIndexInList].GetComponent<RectTransform>().localPosition = 
                            new Vector2(GetXPositionForShapeSquare(shapeData, column, moveDistance),
                            GetYPositionForShapeSquare(shapeData, row, moveDistance));
                        currentIndexInList++;
                    }
                    else
                    {
                        Debug.LogError("Index out of range: " + currentIndexInList);
                        return;
                    }
                }
            }
        }

        Debug.Log("Shape created successfully.");
    }

    // Calcula la posición Y para un cuadrado dentro de la forma.
    private float GetYPositionForShapeSquare(ShapeData shapeData, int row, Vector2 moveDistance)
    {
        float shiftOnY = 0f;

        if (shapeData.rows > 1)
        {
            if (shapeData.rows % 2 != 0)
            {
                var middleSquareIndex = (shapeData.rows - 1) / 2;

                if (row < middleSquareIndex) // Mueve hacia abajo.
                {
                    shiftOnY = moveDistance.y * (row - middleSquareIndex); // Cálculo ajustado.
                }
                else if (row > middleSquareIndex) // Mueve hacia arriba.
                {
                    shiftOnY = moveDistance.y * (row - middleSquareIndex); // Cálculo ajustado.
                }
            }
            else
            {
                var middleSquareIndex2 = (shapeData.rows == 2) ? 1 : (shapeData.rows / 2);
                var middleSquareIndex1 = (shapeData.rows == 2) ? 0 : shapeData.rows / 2 - 1;

                if (row == middleSquareIndex1 || row == middleSquareIndex2)
                {
                    if (row == middleSquareIndex2)
                        shiftOnY = (moveDistance.y / 2) * -1;
                    if (row == middleSquareIndex1)
                        shiftOnY = (moveDistance.y / 2);
                }

                if (row < middleSquareIndex1 && row < middleSquareIndex2) // Mueve hacia abajo.
                {
                    shiftOnY = moveDistance.y * (row - middleSquareIndex1); // Cálculo ajustado.
                }
                else if (row > middleSquareIndex1 && row > middleSquareIndex2) // Mueve hacia arriba.
                {
                    shiftOnY = moveDistance.y * (row - middleSquareIndex1); // Cálculo ajustado.
                }
            }
        }

        return shiftOnY;
    }

    // Calcula la posición X para un cuadrado dentro de la forma.
    private float GetXPositionForShapeSquare(ShapeData shapeData, int column, Vector2 moveDistance)
    {
        float shiftOnX = 0f;

        if (shapeData.columns > 1)
        {
            if (shapeData.columns % 2 != 0)
            {
                var middleSquareIndex = (shapeData.columns - 1) / 2;

                if (column < middleSquareIndex) // Mueve hacia la izquierda.
                {
                    shiftOnX = moveDistance.x * (column - middleSquareIndex); // Cálculo ajustado.
                }
                else if (column > middleSquareIndex) // Mueve hacia la derecha.
                {
                    shiftOnX = moveDistance.x * (column - middleSquareIndex); // Cálculo ajustado.
                }
            }
            else
            {
                var middleSquareIndex2 = (shapeData.columns == 2) ? 1 : (shapeData.columns / 2);
                var middleSquareIndex1 = (shapeData.columns == 2) ? 0 : shapeData.columns / 2 - 1;

                if (column == middleSquareIndex1 || column == middleSquareIndex2)
                {
                    if (column == middleSquareIndex2)
                        shiftOnX = moveDistance.x / 2;
                    if (column == middleSquareIndex1)
                        shiftOnX = (moveDistance.x / 2) * -1;
                }

                if (column < middleSquareIndex1 && column < middleSquareIndex2) // Mueve hacia la izquierda.
                {
                    shiftOnX = moveDistance.x * (column - middleSquareIndex1); // Cálculo ajustado.
                }
                else if (column > middleSquareIndex1 && column > middleSquareIndex2) // Mueve hacia la derecha.
                {
                    shiftOnX = moveDistance.x * (column - middleSquareIndex1); // Cálculo ajustado.
                }
            }
        }

        return shiftOnX;
    }

    // Cuenta el número total de cuadrados activos en la forma.
    private int GetNumberOfSquares(ShapeData shapeData)
    {
        int number = 0;

        // Itera sobre cada fila y cuenta los cuadrados activos.
        foreach (var rowData in shapeData.board)
        {
            foreach (var active in rowData.column)
            {
                if (active)
                    number++;
            }
        }
        return number;
    }

    // Eventos de puntero que actualmente no tienen implementación.
    public void OnPointerClick(PointerEventData eventData)
    {

    }

    public void OnPointerUp(PointerEventData eventData)
    {

    }

    // Evento que se activa cuando comienza el arrastre de la forma.
    public void OnBeginDrag(PointerEventData eventData)
    {
        // Cambia la escala de la forma cuando se selecciona.
        this.GetComponent<RectTransform>().localScale = shapeSelectedScale;
    }

    // Evento que se activa durante el arrastre de la forma.
    public void OnDrag(PointerEventData eventData)
    {
        // Ajusta los anclajes y pivotes del RectTransform.
        _transform.anchorMin = new Vector2(0, 0);
        _transform.anchorMax = new Vector2(0, 0);
        _transform.pivot = new Vector2(0, 0);

        Vector2 pos;
        // Convierte la posición del puntero en coordenadas locales dentro del Canvas.
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_canvas.transform as RectTransform, 
        eventData.position, Camera.main, out pos);
        // Actualiza la posición de la forma con el desplazamiento aplicado.
        _transform.localPosition = pos + offset;
    }

    // Evento que se activa cuando termina el arrastre de la forma.
    public void OnEndDrag(PointerEventData eventData)
    {
        // Restablece la escala de la forma a la original.
        this.GetComponent<RectTransform>().localScale = _shapeStartScale;
        // Llama al evento que verifica si la forma puede ser colocada en el tablero.
        GameEvents.CheckIfShapeCanBePlaced();
    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }

    // Método para mover la forma a su posición inicial.
    private void MoveShapeToStartPosition()
    {
        _transform.transform.localPosition = _startPosition;
    }

}
