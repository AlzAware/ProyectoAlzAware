using System.Collections.Generic;
using UnityEngine;

public class GraficoBarras : MonoBehaviour
{
    public GameObject barPrefab; // Prefab de las barras
    public Transform barsContainer; // Contenedor de las barras

    private SQLiteManager sqliteManager;

    void Start()
    {
        // Encuentra el componente SQLiteManager en la escena
        sqliteManager = FindObjectOfType<SQLiteManager>();
        if (sqliteManager != null)
        {
            Debug.Log("GraficoBarras encontr� SQLiteManager correctamente.");
        }
        else
        {
            Debug.LogError("SQLiteManager a�n no est� disponible.");
        }
    }

    /// <summary>
    /// Muestra un gr�fico de barras para las mejores estad�sticas de un ejercicio espec�fico.
    /// </summary>
    /// <param name="idEjercicio">ID del ejercicio</param>
    /// <param name="titulo">T�tulo del gr�fico</param>
    public void MostrarGrafico(int idEjercicio, string titulo)
    {
        if (sqliteManager == null)
        {
            Debug.LogError("SQLiteManager no est� inicializado.");
            return;
        }

        // Limpiar las barras existentes en el contenedor
        foreach (Transform child in barsContainer)
        {
            Destroy(child.gameObject);
        }

        // Obtener las mejores estad�sticas de la base de datos
        List<(string fecha, int puntuacion)> estadisticas = sqliteManager.ObtenerMejoresEstadisticas(idEjercicio);

        if (estadisticas.Count == 0)
        {
            Debug.LogWarning("No hay estad�sticas disponibles para el ejercicio seleccionado.");
            return;
        }

        // Crear las barras
        foreach (var estadistica in estadisticas)
        {
            // Crear barra
            GameObject bar = Instantiate(barPrefab, barsContainer);
            RectTransform barRect = bar.GetComponent<RectTransform>();

            // Escalar barra seg�n la puntuaci�n (0 a 100 -> altura m�xima 800)
            float height = Mathf.Clamp((estadistica.puntuacion / 100f) * 800, 0f, 800);
            barRect.sizeDelta = new Vector2(barRect.sizeDelta.x, height);
        }

        Debug.Log($"Gr�fico mostrado para el ejercicio {idEjercicio}.");
    }
}
