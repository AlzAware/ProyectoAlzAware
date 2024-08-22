using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class Grid : MonoBehaviour
    {
        // Referencia al objeto ShapeStorage que almacena las formas disponibles en el juego.
        public ShapeStorage shapeStorage;
        public int columns = 0; // Asegúrate de que este valor sea mayor que 0 en el Inspector.
        public int rows = 0; // Asegúrate de que este valor sea mayor que 0 en el Inspector.
        // Espacio entre los cuadrados en la cuadrícula.
        public float squaresGap = 0.1f;
        // Prefab del cuadrado que se usará para construir la cuadrícula.
        public GameObject gridSquare; // Asegúrate de asignar este prefab en el Inspector.
        // Posición inicial donde se empezará a dibujar la cuadrícula.
        public Vector2 startPosition = new Vector2(0.0f, 0.0f);
        // Escala de los cuadrados en la cuadrícula.
        public float squareScale = 0.5f;
        // Offset que se aplica a cada cuadrado en la cuadrícula.
        public float everySquareOffset = 0.0f;
        // Datos de la textura que se aplicarán a los cuadrados de la cuadrícula.
        public SquareTextureData  squareTextureData;

        // Offset utilizado para posicionar los cuadrados en la cuadrícula.
        private Vector2 _offset = new Vector2(0.0f, 0.0f);

        // Lista que almacena los cuadrados generados para la cuadrícula.
        private System.Collections.Generic.List<GameObject> _gridSquares = new System.Collections.Generic.List<GameObject>();
        //private List<GameObject> _gridSquares = new List<GameObject>();

        // Referencia al indicador de líneas (posiblemente para resaltar líneas completadas).
        private LineIndicator _lineIndicator;
        // Color del cuadrado actualmente activo en la cuadrícula.
        private Config.SquareColor currentActiveSquareColor_ = Config.SquareColor.NotSet;


        // Método llamado cuando el objeto se desactiva.
        // Desuscribe los métodos de los eventos para evitar errores.
        private void OnDisable()
        {
            GameEvents.CheckIfShapeCanBePlaced -= CheckIfShapeCanBePlaced;
            GameEvents.UpdateSquareColor -= OnUpdateSquareColor;
        }

        // Método llamado cuando el objeto se activa.
        // Suscribe los métodos a los eventos relevantes.
        private void OnEnable()
        {
            GameEvents.CheckIfShapeCanBePlaced += CheckIfShapeCanBePlaced;
            GameEvents.UpdateSquareColor += OnUpdateSquareColor;
        }
        
        // Método llamado al inicio del juego.
        // Inicializa el LineIndicator, crea la cuadrícula y establece el color activo.
        void Start()
        {

            _lineIndicator = GetComponent<LineIndicator>();
            CreateGrid();
            currentActiveSquareColor_ = squareTextureData.activeSquareTextures[0].squareColor;
        }

        // Método llamado cuando se actualiza el color de los cuadrados.
        private void OnUpdateSquareColor(Config.SquareColor color)
        {
            currentActiveSquareColor_ = color;
        }

        
        // Método para crear la cuadrícula. Se divide en dos partes: generar los cuadrados y posicionarlos.
        private void CreateGrid()
        {
            SpawnGridSquares();
            SetGridSquaresPositions();
        }

        // Método para generar los cuadrados de la cuadrícula.
        private void SpawnGridSquares()
        {
            int square_index = 0;

            for (var row = 0; row < rows; ++row)
            {
                for (var column = 0; column < columns; ++column)
                {   
                    // Instancia un nuevo cuadrado y lo añade a la lista.
                    _gridSquares.Add(Instantiate(gridSquare) as GameObject);

                    // Asigna el índice al cuadrado, lo asocia al objeto padre y establece su escala.
                    _gridSquares[_gridSquares.Count - 1].GetComponent<GridSquare>().SquareIndex = square_index;
                    _gridSquares[_gridSquares.Count -1].transform.SetParent(this.transform);
                    _gridSquares[_gridSquares.Count -1].transform.localScale = new Vector3(x:squareScale, y:squareScale, z:squareScale);
                    // Establece la textura del cuadrado en función de su índice.
                    _gridSquares[_gridSquares.Count -1].GetComponent<GridSquare>().SetImage(_lineIndicator.GetGridSqureIndex(square_index) % 2 == 0);
                    square_index++;     
                }   
            }
        }
        // Método para posicionar los cuadrados en la cuadrícula.
        private void SetGridSquaresPositions()
        {
           
            int column_number = 0;
            int row_number = 0;
            Vector2 square_gap_number = new Vector2(0.0f, 0.0f);
            bool row_moved = false;

            // Obtiene las dimensiones del primer cuadrado para calcular el offset.
            var square_rect = _gridSquares[0].GetComponent<RectTransform>();
           
            _offset.x = square_rect.rect.width * square_rect.transform.localScale.x + everySquareOffset;
            _offset.y = square_rect.rect.height * square_rect.transform.localScale.y + everySquareOffset;

            // Posiciona cada cuadrado en su lugar correcto dentro de la cuadrícula.
            foreach (GameObject square in _gridSquares)
            {
                if (column_number + 1 > columns)
                {
                    square_gap_number.x = 0;
                    //va a la siguiente columna
                    column_number = 0; 
                    row_number++;
                    row_moved = false;
                }

                var pos_x_offset = _offset.x * column_number + (square_gap_number.x * squaresGap);
                var pos_y_offset = _offset.y * row_number + (square_gap_number.y * squaresGap);

                if (column_number > 0 && column_number % 3 == 0)
                {
                    square_gap_number.x++;
                    pos_x_offset += squaresGap;
                }

                if (row_number > 0 && row_number % 3 == 0 && row_moved == false)
                {
                    row_moved = true;
                    square_gap_number.y++;
                    pos_y_offset += squaresGap;
                }

                square.GetComponent<RectTransform>().anchoredPosition = new Vector2(startPosition.x + pos_x_offset,
                startPosition.y - pos_y_offset);
                
                square.GetComponent<RectTransform>().localPosition = new Vector3(startPosition.x + pos_x_offset,
                startPosition.y - pos_y_offset, 0.0f);
             
                column_number++;
            }
        }
        // Método para comprobar si una forma puede ser colocada en la cuadrícula.
        private void CheckIfShapeCanBePlaced()
        {

            var squareIndexes = new List<int>();
            // Recorre todos los cuadrados de la cuadrícula y guarda los índices de los seleccionados y no ocupados.
            foreach (var square in _gridSquares)
            {
                var gridSquare = square.GetComponent<GridSquare>();

                if (gridSquare.Selected && !gridSquare.SquareOccupied)
                {
                    squareIndexes.Add(gridSquare.SquareIndex);
                    gridSquare.Selected = false;
                    //gridSquare.ActivateSquare();
                }
            }


            var currentSelectedShape = shapeStorage.GetCurrentSelectedShape();
            if(currentSelectedShape == null) return; //there is no selected shape

            // Si el número de cuadrados seleccionados coincide con la forma actual, coloca la forma en la cuadrícula.
            if(currentSelectedShape.TotalSquareNumber == squareIndexes.Count)
            {

                foreach(var squareIndex in squareIndexes)
                {
                    _gridSquares[squareIndex].GetComponent<GridSquare>().PlaceShapeOnBoard(currentActiveSquareColor_);
                }

                var shapeLeft = 0;

                // Comprueba si quedan formas en la posición inicial.
                foreach (var shape in shapeStorage.shapeList)
                {
                    if(shape.IsOnStartPosition() && shape.IsAnyOfShapeSquareActive())
                    {
                        shapeLeft++;
                    }
                }

                // Si no quedan formas, solicita nuevas formas, de lo contrario, desactiva la forma actual.
                if (shapeLeft == 0)
                {
                    GameEvents.RequestNewShapes();
                }
                else
                {
                    GameEvents.SetShapeInactive();
                }

                CheckIfAnyLineIsCompleted();

                
            }
            else
            {   
                // Si no se puede colocar la forma, la mueve de nuevo a la posición inicial.
                GameEvents.MoveShapeToStartPosition();
            }
            
        }
        // Método para comprobar si se ha completado alguna línea (horizontal, vertical o cuadrada).
        void CheckIfAnyLineIsCompleted()
        {
            List<int[]> lines = new List<int[]>();

            //columnas. Añade todas las columnas como posibles líneas completadas.
            foreach (var column in _lineIndicator.columnIndexes)
            {
                lines.Add(_lineIndicator.GetVerticalLine(column));
            }

            //filas. Añade todas las filas como posibles líneas completadas.
            for (var row = 0; row < 9; row++)
            {
                List<int> data = new List<int>(9);
                for(var index = 0; index < 9; index++)
                {
                    data.Add(_lineIndicator.line_data[row, index]);
                }

                lines.Add(data.ToArray());
            }

            //squares. Añade todos los cuadrados 3x3 como posibles líneas completadas.
            for(var square = 0; square < 9; square++)
            {
                List<int> data = new List<int>(9);
                for(var index = 0; index < 9; index++)
                {
                    data.Add(_lineIndicator.square_data[square, index]);
                }
                lines.Add(data.ToArray());
            }

            // Comprueba cuántas líneas se completaron.
            var completedLines = CheckIfSquaresAreCompleted(lines);

            // Si se completaron más de dos líneas, activa una animación de bonificación.
            if (completedLines > 2)
            {
                //TODO: Animación bonus
            }
            // Añade la puntuación correspondiente y comprueba si el jugador ha perdido.
            var totalScores = 10 * completedLines;
            GameEvents.AddScores(totalScores);
            CheckIfPlayerLost();


        }

        // Método para comprobar si los cuadrados de la cuadrícula forman una línea completada.
        private int CheckIfSquaresAreCompleted(List<int[]> data)
        {
            List<int[]> completedLines = new List<int[]>();

            var linesCompleted = 0;

            // Comprueba cada línea para ver si está completa (todos los cuadrados ocupados).
            foreach (var line in data)
            {
                var lineCompleted = true;
                foreach (var squareIndex in line)
                {
                    var comp = _gridSquares[squareIndex].GetComponent<GridSquare>();
                    if(comp.SquareOccupied == false)
                    {
                        lineCompleted = false;
                    }
                }

                if (lineCompleted)
                {
                    completedLines.Add(line);
                }
            }

            // Desactiva y limpia los cuadrados en las líneas completadas.
            foreach (var line in completedLines)
            {
                var completed = false;

                foreach (var squareIndex in line)
                {
                    var comp = _gridSquares[squareIndex].GetComponent<GridSquare>();
                    comp.Deactivate();
                    completed = true;
                }

                foreach (var squareIndex in line)
                {
                    var comp = _gridSquares[squareIndex].GetComponent<GridSquare>();
                    comp.ClearOccupied();
                }

                if(completed)
                {
                    linesCompleted++;
                }
            }

            return linesCompleted;
        }

        // Método para comprobar si el jugador ha perdido.
        private void CheckIfPlayerLost()
        {
            var validShapes = 0;
            // Comprueba si alguna forma puede ser colocada en la cuadrícula.
            for(var index = 0; index < shapeStorage.shapeList.Count; index++)
            {
                var isShapeActive = shapeStorage.shapeList[index].IsAnyOfShapeSquareActive();
                if(CheckIfShapeCanBePlacedOnGrid(shapeStorage.shapeList[index]) && isShapeActive)
                {
                    shapeStorage.shapeList[index]?.ActivateShape(); //si solo hay 3 elementos, por ejemplo, e intento coger el cuarto (que no existe), el simbolo de interrogación hace la función de prevenir que el juego "pete"
                    validShapes++;
                }
            }

            // Si no hay formas válidas que se puedan colocar, el juego termina.
            if(validShapes == 0)
            {
                //GAME OVER
                GameEvents.GameOver(false);
                //Debug.Log("GAME OVER");
            }
        }
        
        // Método para comprobar si una forma específica se puede colocar en la cuadrícula.
        private bool CheckIfShapeCanBePlacedOnGrid(Shape currentShape)
        {
            var CurrentShapeData = currentShape.CurrentShapeData;
            var shapeColumns = CurrentShapeData.columns;
            var shapeRows = CurrentShapeData.rows;

            //Lista de todos los índices de cuadrados rellenos
            List<int> originalShapeFilledUpSquares = new List<int>();
            var squareIndex = 0;

            for (var rowIndex = 0; rowIndex < shapeRows; rowIndex++)
            {
                for(var columnIndex = 0; columnIndex < shapeColumns; columnIndex++)
                {
                    if (CurrentShapeData.board[rowIndex].column[columnIndex])
                    {
                        originalShapeFilledUpSquares.Add(squareIndex);
                    }
                    squareIndex++;
                }
            }

            if(currentShape.TotalSquareNumber != originalShapeFilledUpSquares.Count)
                Debug.LogError("Number of filled up squares are not the same as the original shape have.");

            var squareList = GetAllSquaresCombination(shapeColumns, shapeRows);

            bool canBePlaced = false;

            // Comprueba si alguna de las combinaciones posibles de cuadrados en la cuadrícula está disponible para colocar la forma.
            foreach(var number in squareList)
            {
                bool shapeCanBePlacedOnTheBoard =  true; //aplicamos el valor "true" a esta variable ya que luego queremos ponerla como "false"
                foreach(var squareIndexToCheck in originalShapeFilledUpSquares)
                {
                    var comp = _gridSquares[number[squareIndexToCheck]].GetComponent<GridSquare>();
                    if(comp.SquareOccupied) 
                    {
                        shapeCanBePlacedOnTheBoard = false;
                    }
                }

                if(shapeCanBePlacedOnTheBoard)
                {
                    canBePlaced = true; 
                }
            }

            return canBePlaced;

        }

        // Método para obtener todas las combinaciones posibles de cuadrados en la cuadrícula para colocar una forma.
        private List<int[]> GetAllSquaresCombination(int columns, int rows)
        {
            var squareList = new List<int[]>();
            var lastColumnIndex = 0;
            var lastRowIndex = 0;

            int safeIndex = 0;

            // Genera todas las combinaciones posibles de posiciones en la cuadrícula.
            while(lastRowIndex + (rows -1) < 9)
            {
                var rowData = new List<int>();

                for(var row = lastRowIndex; row < lastRowIndex + rows; row++)
                {
                    for (var column = lastColumnIndex; column < lastColumnIndex + columns; column++)
                    {
                        rowData.Add(_lineIndicator.line_data[row, column]);
                    }
                }

                squareList.Add(rowData.ToArray());

                lastColumnIndex++;

                if (lastColumnIndex + (columns -1) >=9)
                {
                    lastRowIndex++;
                    lastColumnIndex = 0;
                }

                safeIndex++;
                //En caso de bucle, utilizamos esto para romperlo
                if(safeIndex > 100)
                {
                    break;
                }
            }

            return squareList;
        }
    }



