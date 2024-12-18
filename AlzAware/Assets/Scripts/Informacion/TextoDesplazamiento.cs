using UnityEngine;
using TMPro;

public class TextoDesplazamiento : MonoBehaviour
{
    [SerializeField] private TMP_Text texto; // Referencia al TextMeshPro que se desplazar�
    [SerializeField] private float velocidadDesplazamiento = 20f; // Velocidad del desplazamiento
    [SerializeField] private RectTransform rectTransform; // El RectTransform del TextMeshPro
    [SerializeField] private float tiempoParaReiniciar = 10f; // Tiempo antes de reiniciar la posici�n del texto

    private Vector2 posicionInicial; // Posici�n inicial capturada autom�ticamente
    private bool detenerDesplazamiento = false; // Variable para detener el desplazamiento si es necesario
    private float tiempoTranscurrido = 0f; // Tiempo transcurrido desde el inicio del desplazamiento

    void Start()
    {
        if (texto == null)
        {
            Debug.LogError("El TextMeshPro no est� asignado.");
        }

        if (rectTransform == null)
        {
            rectTransform = texto.GetComponent<RectTransform>();
        }

        // Capturar la posici�n inicial del texto
        posicionInicial = rectTransform.anchoredPosition;
    }

    void Update()
    {
        if (!detenerDesplazamiento)
        {
            // Mover el texto hacia arriba
            rectTransform.anchoredPosition += Vector2.up * velocidadDesplazamiento * Time.deltaTime;

            // Incrementar el tiempo transcurrido
            tiempoTranscurrido += Time.deltaTime;

            // Reiniciar posici�n si se alcanza el tiempo determinado
            if (tiempoTranscurrido >= tiempoParaReiniciar)
            {
                ResetearPosicion();
            }
        }
    }

    public void DetenerDesplazamiento()
    {
        detenerDesplazamiento = true; // Detener el movimiento
    }

    public void ReiniciarDesplazamiento()
    {
        detenerDesplazamiento = false; // Reiniciar el movimiento
    }

    public void ResetearPosicion()
    {
        // Reinicia la posici�n del texto a la inicial capturada
        rectTransform.anchoredPosition = posicionInicial;
        tiempoTranscurrido = 0f; // Reiniciar el contador de tiempo
    }
}
