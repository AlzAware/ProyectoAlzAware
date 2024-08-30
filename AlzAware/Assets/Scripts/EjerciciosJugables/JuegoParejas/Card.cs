using System.Collections;
using UnityEngine;

public class Card : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;  // Referencia al componente SpriteRenderer de la carta
    private GameManager gameManager;        // Referencia al GameManager
    public Sprite cardImage;                // Imagen que representa la carta (el anverso)
    public bool isRevealed = false;         // Indica si la carta está revelada o no
    private Sprite coverImage;              // Imagen de la cubierta de la carta (el reverso)

    private bool isFlipping = false;        // Indica si la carta está en proceso de voltear
    private bool isAnimating = false;       // Indica si la carta está en proceso de animación de desaparición

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameManager = FindObjectOfType<GameManager>();

        coverImage = gameManager.coverImage;

        // Asegura que la carta esté oculta correctamente al inicio
        spriteRenderer.sprite = coverImage;
        transform.localScale = new Vector3(1, 1, 1);
    }

    void OnMouseDown()
    {
        if (!isRevealed && !gameManager.IsWaitingForMatch() && !isFlipping && !isAnimating)
        {
            StartCoroutine(FlipCard());
        }
    }

    private IEnumerator FlipCard()
    {
        isFlipping = true;

        // Escalar la carta hacia atrás hasta 0 en el eje X (simulando la rotación)
        for (float i = 1; i >= 0; i -= 0.1f)
        {
            transform.localScale = new Vector3(i, 1, 1);
            yield return new WaitForSeconds(0.01f);
        }

        // Cambia el sprite cuando la carta está "de lado"
        spriteRenderer.sprite = isRevealed ? coverImage : cardImage;

        // Completa la escala de vuelta a 1 en el eje X (simulando la rotación final)
        for (float i = 0; i <= 1; i += 0.1f)
        {
            transform.localScale = new Vector3(i, 1, 1);
            yield return new WaitForSeconds(0.01f);
        }

        // Asegura que la carta esté en la escala correcta (1 en el eje X)
        transform.localScale = new Vector3(1, 1, 1);

        isRevealed = !isRevealed;
        isFlipping = false;

        // Notifica al GameManager si la carta fue revelada
        if (isRevealed)
        {
            gameManager.CardRevealed(this);
        }
    }

    public void HideCard()
    {
        if (isRevealed && !isFlipping && !isAnimating)
        {
            StartCoroutine(FlipCard());
        }
    }

    public void AnimateAndDestroy()
    {
        if (!isAnimating)
        {
            StartCoroutine(GrowAndShrink());
        }
    }

    private IEnumerator GrowAndShrink()
    {
        isAnimating = true;

        // Crece la carta un poco
        for (float i = 1; i <= 1.2f; i += 0.05f)
        {
            transform.localScale = new Vector3(i, i, 1);
            yield return new WaitForSeconds(0.01f);
        }

        // Disminuye la carta hasta que desaparezca
        for (float i = 1.2f; i >= 0; i -= 0.05f)
        {
            transform.localScale = new Vector3(i, i, 1);
            yield return new WaitForSeconds(0.01f);
        }

        // Destruye el objeto de la carta
        Destroy(gameObject);
    }

    public Sprite GetCardImage()
    {
        return cardImage;
    }
}
