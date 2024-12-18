using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorEjercicios : MonoBehaviour
{
    public GameObject panelSopa;
    public Animator panelAnimatorSopa;

    public GameObject panelBuscaObjetos;
    public Animator panelAnimatorBuscaObjetos;

    public GameObject panelFormasGeometricas;
    public Animator panelAnimatorFormasGeometricas;

    public GameObject panelRefranes;
    public Animator panelAnimatorRefranes;

    public GameObject panelJuegoParejas;
    public Animator panelAnimatorJuegoParejas;

    public GameObject panelPuzzle;
    public Animator panelAnimatorPuzzle;

    private const string animacionEntrada = "EntradaPanelEjercicios";

    public int escenasSopaLetras;
    public int escenasBuscaObjetos;
    public int escenasRefranes;
    public int escenasFormasGeometricas;

    public void CargarSopaDeLetrasAleatoria()
    {
        StartCoroutine(CargarEscenaSopa());
    }

    public void CargarBuscaObjetosAleatorio()
    {
        StartCoroutine(CargarEscenaBuscaObjetos());
    }

    public void CargarFormasGeometricasAleatorio()
    {
        StartCoroutine(CargarEscenaFormasGeometricas());
    }

    public void CargarRefranesAleatorio()
    {
        StartCoroutine(CargarEscenaRefranes());
    }

    public void CargarJuegoParejas()
    {
        StartCoroutine(CargarEscenaJuegoParejas());
    }

    public void CargarPuzzle()
    {
        StartCoroutine(CargarEscenaPuzzle());
    }

    public void VolverAlMenu()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }

    // Corrutina para Sopa de Letras
    private IEnumerator CargarEscenaSopa()
    {
        panelSopa.SetActive(true);
        panelAnimatorSopa.Play(animacionEntrada);
        yield return new WaitForSeconds(1.5f);

        int aleatorioEscena = UnityEngine.Random.Range(1, escenasSopaLetras + 1);

        switch (aleatorioEscena)
        {
            case 1: SceneManager.LoadScene("SopaLetras"); break;
            case 2: SceneManager.LoadScene("SopaLetras1"); break;
            case 3: SceneManager.LoadScene("SopaLetras2"); break;
            case 4: SceneManager.LoadScene("SopaLetras3"); break;
            case 5: SceneManager.LoadScene("SopaLetras4"); break;
            case 6: SceneManager.LoadScene("SopaLetras5"); break;
            case 7: SceneManager.LoadScene("SopaLetras6"); break;
            case 8: SceneManager.LoadScene("SopaLetras7"); break;
            case 9: SceneManager.LoadScene("SopaLetras8"); break;
            case 10: SceneManager.LoadScene("SopaLetras9"); break;
            default: Debug.LogError("No hay escenas en la lista para cargar."); break;
        }
    }

    // Corrutina para Busca Objetos
    private IEnumerator CargarEscenaBuscaObjetos()
    {
        panelBuscaObjetos.SetActive(true);
        panelAnimatorBuscaObjetos.Play(animacionEntrada);
        yield return new WaitForSeconds(1.5f);

        int aleatorioEscena = UnityEngine.Random.Range(1, escenasBuscaObjetos + 1);

        switch (aleatorioEscena)
        {
            case 1: SceneManager.LoadScene("BuscaObjetos1"); break;
            case 2: SceneManager.LoadScene("BuscaObjetos2"); break;
            case 3: SceneManager.LoadScene("BuscaObjetos3"); break;
            case 4: SceneManager.LoadScene("BuscaObjetos4"); break;
            default: Debug.LogError("No hay escenas en la lista para cargar."); break;
        }
    }

    // Corrutina para Formas Geométricas
    private IEnumerator CargarEscenaFormasGeometricas()
    {
        panelFormasGeometricas.SetActive(true);
        panelAnimatorFormasGeometricas.Play(animacionEntrada);
        yield return new WaitForSeconds(1.5f);

        int aleatorioEscena = UnityEngine.Random.Range(1, escenasFormasGeometricas + 1);

        switch (aleatorioEscena)
        {
            case 1: SceneManager.LoadScene("FormaGeometrica"); break;
            case 2: SceneManager.LoadScene("FormaGeometrica1"); break;
            case 3: SceneManager.LoadScene("FormaGeometrica2"); break;
            case 4: SceneManager.LoadScene("FormaGeometrica3"); break;
            case 5: SceneManager.LoadScene("FormaGeometrica4"); break;
            default: Debug.LogError("No hay escenas en la lista para cargar."); break;
        }
    }

    // Corrutina para Refranes
    private IEnumerator CargarEscenaRefranes()
    {
        panelRefranes.SetActive(true);
        panelAnimatorRefranes.Play(animacionEntrada);
        yield return new WaitForSeconds(1.5f);

        int aleatorioEscena = UnityEngine.Random.Range(1, escenasRefranes + 1);

        switch (aleatorioEscena)
        {
            case 1: SceneManager.LoadScene("Refran1"); break;
            case 2: SceneManager.LoadScene("Refran2"); break;
            case 3: SceneManager.LoadScene("Refran3"); break;
            case 4: SceneManager.LoadScene("Refran4"); break;
            case 5: SceneManager.LoadScene("Refran5"); break;
            case 6: SceneManager.LoadScene("Refran6"); break;
            case 7: SceneManager.LoadScene("Refran7"); break;
            default: Debug.LogError("No hay escenas en la lista para cargar."); break;
        }
    }

    // Corrutina para Juego de Parejas
    private IEnumerator CargarEscenaJuegoParejas()
    {
        panelJuegoParejas.SetActive(true);
        panelAnimatorJuegoParejas.Play(animacionEntrada);
        yield return new WaitForSeconds(1.5f);

        SceneManager.LoadScene("JuegoParejas");
    }

    // Corrutina para Puzzle
    private IEnumerator CargarEscenaPuzzle()
    {
        panelPuzzle.SetActive(true);
        panelAnimatorPuzzle.Play(animacionEntrada);
        yield return new WaitForSeconds(1.5f);

        SceneManager.LoadScene("Puzzle");
    }
}
