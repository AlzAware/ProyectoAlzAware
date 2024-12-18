using UnityEngine;

public class ControladorEstadisticas : MonoBehaviour
{
    [Header("Configuraci�n del Gr�fico")]
    public int idEjercicio; // Asigna este ID desde el Inspector
    public string titulo; // Asigna el t�tulo del gr�fico desde el Inspector

    void Start()
    {
        StartCoroutine(EsperarYMostrarGrafico());
    }

    private System.Collections.IEnumerator EsperarYMostrarGrafico()
    {
        var sqliteManager = FindObjectOfType<SQLiteManager>();

        if (sqliteManager == null)
        {
            Debug.LogError("No se encontr� SQLiteManager.");
            yield break;
        }

        // Esperar hasta que SQLiteManager est� inicializado
        while (!sqliteManager.IsInitialized)
        {
            Debug.Log("Esperando a que SQLiteManager se inicialice...");
            yield return null; // Espera un frame
        }

        Debug.Log("SQLiteManager inicializado, cargando gr�fico...");

        // Mostrar gr�fico
        var grafico = FindObjectOfType<GraficoBarras>();
        if (grafico != null)
        {
            grafico.MostrarGrafico(idEjercicio, titulo);
        }
        else
        {
            Debug.LogError("No se encontr� el componente GraficoBarras en la escena.");
        }
    }
}
