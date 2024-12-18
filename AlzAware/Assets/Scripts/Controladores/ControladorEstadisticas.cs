using UnityEngine;

public class ControladorEstadisticas : MonoBehaviour
{
    [Header("Configuración del Gráfico")]
    public int idEjercicio; // Asigna este ID desde el Inspector
    public string titulo; // Asigna el título del gráfico desde el Inspector

    void Start()
    {
        StartCoroutine(EsperarYMostrarGrafico());
    }

    private System.Collections.IEnumerator EsperarYMostrarGrafico()
    {
        var sqliteManager = FindObjectOfType<SQLiteManager>();

        if (sqliteManager == null)
        {
            Debug.LogError("No se encontró SQLiteManager.");
            yield break;
        }

        // Esperar hasta que SQLiteManager esté inicializado
        while (!sqliteManager.IsInitialized)
        {
            Debug.Log("Esperando a que SQLiteManager se inicialice...");
            yield return null; // Espera un frame
        }

        Debug.Log("SQLiteManager inicializado, cargando gráfico...");

        // Mostrar gráfico
        var grafico = FindObjectOfType<GraficoBarras>();
        if (grafico != null)
        {
            grafico.MostrarGrafico(idEjercicio, titulo);
        }
        else
        {
            Debug.LogError("No se encontró el componente GraficoBarras en la escena.");
        }
    }
}
