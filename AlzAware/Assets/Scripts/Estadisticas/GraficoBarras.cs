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
            Debug.Log("GraficoBarras encontró SQLiteManager correctamente.");
        }
        else
        {
            Debug.LogError("SQLiteManager aún no está disponible.");
        }
    }

    /// <summary>
    /// Muestra un gráfico de barras para las mejores estadísticas de un ejercicio específico.
    /// </summary>
    /// <param name="idEjercicio">ID del ejercicio</param>
    /// <param name="titulo">Título del gráfico</param>
    public void MostrarGrafico(int idEjercicio, string titulo)
    {
        if (sqliteManager == null)
        {
            Debug.LogError("SQLiteManager no está inicializado.");
            return;
        }

        // Limpiar las barras existentes en el contenedor
        foreach (Transform child in barsContainer)
        {
            Destroy(child.gameObject);
        }

        // Obtener las mejores estadísticas de la base de datos
        List<(string fecha, int puntuacion)> estadisticas = sqliteManager.ObtenerMejoresEstadisticas(idEjercicio);

        if (estadisticas.Count == 0)
        {
            Debug.LogWarning("No hay estadísticas disponibles para el ejercicio seleccionado.");
            return;
        }

        // Crear las barras
        foreach (var estadistica in estadisticas)
        {
            // Crear barra
            GameObject bar = Instantiate(barPrefab, barsContainer);
            RectTransform barRect = bar.GetComponent<RectTransform>();

            // Escalar barra según la puntuación (0 a 100 -> altura máxima 800)
            float height = Mathf.Clamp((estadistica.puntuacion / 100f) * 800, 0f, 800);
            barRect.sizeDelta = new Vector2(barRect.sizeDelta.x, height);
        }

        Debug.Log($"Gráfico mostrado para el ejercicio {idEjercicio}.");
    }
}
