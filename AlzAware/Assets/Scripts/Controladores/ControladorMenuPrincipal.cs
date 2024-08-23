using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorMenuPrincipal : MonoBehaviour
{
    // Referencias a los Animator de cada objeto
    [Header("Animator Menu")]
    [SerializeField] Animator panelAzulAnimator;
    [Header("Animator EjerciciosDiarios")]
    [SerializeField] Animator logoEjerciciosDiariosAnimator;
    [SerializeField] Animator textoEjerciciosDiariosAnimator;
    [SerializeField] Animator panelNaranjaEjerciciosDiariosAnimator;
    [Header("Animator Ejercicios")]
    [SerializeField] Animator logoEjerciciosAnimator;
    [SerializeField] Animator textoEjerciciosAnimator;
    [SerializeField] Animator panelVerdeEjerciciosAnimator;
    [Header("Animator Estadisticas")]
    [SerializeField] Animator logoEstadisticasAnimator;
    [SerializeField] Animator textoEstadisticasAnimator;
    [SerializeField] Animator panelAmarilloEstadisticasAnimator;
    [Header("Animator Ajustes")]
    [SerializeField] Animator logoAjustesAnimator;
    [SerializeField] Animator textoAjustesAnimator;
    [SerializeField] Animator panelMoradoAjustesAnimator;
    [Header("Animator Info")]
    [SerializeField] Animator logoInfoAnimator;
    [SerializeField] Animator textoInfoAnimator;
    [SerializeField] Animator panelRojoInfoAnimator;

    // Referencias a los objetos que deben desactivarse
    [Header("Objeto Menu")]
    [SerializeField] GameObject menu;
    [Header("Objeto Ejercicios Diarios")]
    [SerializeField] GameObject ejerciciosDiarios;
    [SerializeField] GameObject panelNaranjaEjerciciosDiarios;
    [SerializeField] GameObject botonEjerciciosDiarios;
    [SerializeField] GameObject textoEjerciciosDiarios;
    [Header("Objeto Ejercicios")]
    [SerializeField] GameObject ejercicios;
    [SerializeField] GameObject panelVerdeEjercicios;
    [SerializeField] GameObject botonEjercicios;
    [SerializeField] GameObject textoEjercicios;
    [Header("Objeto Estadísticas")]
    [SerializeField] GameObject estadisticas;
    [SerializeField] GameObject panelAmarilloEstadisticas;
    [SerializeField] GameObject botonEstadisticas;
    [SerializeField] GameObject textoEstadisticas;
    [Header("Objeto Ajustes")]
    [SerializeField] GameObject ajustes;
    [SerializeField] GameObject panelMoradoAjustes;
    [SerializeField] GameObject botonAjustes;
    [SerializeField] GameObject textoAjustes;
    [SerializeField] GameObject logoAjustes;
    [Header("Objeto Info")]
    [SerializeField] GameObject info;
    [SerializeField] GameObject panelRojoInfo;
    [SerializeField] GameObject botonInfo;
    [SerializeField] GameObject textoInfo;
    [SerializeField] GameObject logoInfo;

    // Método utilizado para saltar a la escena de los Ejercicios Diarios
    public void IrAEjerciciosDiarios()
    {
        StartCoroutine(AnimarYCambiarEscenaEjerciciosDiarios());
    }

    // Método utilizado para saltar a la escena Ejercicios
    public void IrAEjercicios()
    {
        StartCoroutine(AnimarYCambiarEscenaEjercicios());
    }

    // Método utilizado para saltar a la escena de las Estadísticas
    public void IrAEstadisticas()
    {
        StartCoroutine(AnimarYCambiarEscenaEstadisticas());
    }

    // Método utilizado para saltar a la escena de Ajustes
    public void IrAAjustes()
    {
        StartCoroutine(AnimarYCambiarEscenaAjustes());
    }

    // Método utilizado para saltar a la escena de Información
    public void IrAInfo()
    {
        StartCoroutine(AnimarYCambiarEscenaInfo());
    }

    #region Corrutinas

    // Corrutina que gestiona las animaciones y el cambio de escena de los Ejercicios Diarios
    IEnumerator AnimarYCambiarEscenaEjerciciosDiarios()
    {
        // Sacar el texto del botón
        textoEjerciciosDiarios.transform.SetParent(ejerciciosDiarios.transform);

        // Activar el panel naranja
        panelNaranjaEjerciciosDiarios.SetActive(true);

        // Reproducir las animaciones
        logoEjerciciosDiariosAnimator.Play("LogoEjerciciosDiarios");
        textoEjerciciosDiariosAnimator.Play("TextoEjerciciosDiarios");
        panelNaranjaEjerciciosDiariosAnimator.Play("SalidaPanelNaranja");

        // Desactivar los objetos
        botonEjerciciosDiarios.SetActive(false);
        ejercicios.SetActive(false);
        estadisticas.SetActive(false);
        ajustes.SetActive(false);
        info.SetActive(false);
        menu.SetActive(false);

        // Esperar a que todas las animaciones terminen
        yield return new WaitForSeconds(2.5f);

        // Cambiar a la escena "EjerciciosDiarios"
        SceneManager.LoadScene("EjerciciosDiarios");
    }

    // Corrutina que gestiona las animaciones y el cambio de escena de los Ejercicios
    IEnumerator AnimarYCambiarEscenaEjercicios()
    {
        // Sacar el texto del botón
        textoEjercicios.transform.SetParent(ejercicios.transform);

        // Activar el panel verde
        panelVerdeEjercicios.SetActive(true);

        // Reproducir las animaciones
        logoEjerciciosAnimator.Play("LogoEjercicios");
        textoEjerciciosAnimator.Play("TextoEjercicios");
        panelVerdeEjerciciosAnimator.Play("SalidaPanelVerde");

        // Desactivar los objetos
        botonEjercicios.SetActive(false);
        ejerciciosDiarios.SetActive(false);
        estadisticas.SetActive(false);
        ajustes.SetActive(false);
        info.SetActive(false);
        menu.SetActive(false);

        // Esperar a que todas las animaciones terminen
        yield return new WaitForSeconds(3f);

        // Cambiar a la escena "Ejercicios"
        SceneManager.LoadScene("Ejercicios");
    }

    // Corrutina que gestiona las animaciones y el cambio de escena de las Estadísticas
    IEnumerator AnimarYCambiarEscenaEstadisticas()
    {
        // Sacar el texto del botón
        textoEstadisticas.transform.SetParent(estadisticas.transform);

        // Activar el panel amarillo
        panelAmarilloEstadisticas.SetActive(true);

        // Reproducir las animaciones
        logoEstadisticasAnimator.Play("LogoEstadisticas");
        textoEstadisticasAnimator.Play("TextoEstadisticas");
        panelAmarilloEstadisticasAnimator.Play("SalidaPanelAmarillo");

        // Desactivar los objetos
        ejercicios.SetActive(false);
        ejerciciosDiarios.SetActive(false);
        botonEstadisticas.SetActive(false);
        ajustes.SetActive(false);
        info.SetActive(false);
        menu.SetActive(false);

        // Esperar a que todas las animaciones terminen
        yield return new WaitForSeconds(3f);

        // Cambiar a la escena "Estadisticas"
        SceneManager.LoadScene("EstadisticasGlobales");
    }

    // Corrutina que gestiona las animaciones y el cambio de escena de los Ajustes
    IEnumerator AnimarYCambiarEscenaAjustes()
    {
        // Sacar el texto del botón
        textoAjustes.transform.SetParent(ajustes.transform);
        logoAjustes.transform.SetParent(ajustes.transform);

        // Activar el panel amarillo
        panelMoradoAjustes.SetActive(true);

        // Reproducir las animaciones
        logoAjustesAnimator.Play("LogoAjustes");
        textoAjustesAnimator.Play("TextoAjustes");
        panelMoradoAjustesAnimator.Play("SalidaPanelMorado");

        // Desactivar los objetos
        ejercicios.SetActive(false);
        ejerciciosDiarios.SetActive(false);
        estadisticas.SetActive(false);
        botonAjustes.SetActive(false);
        info.SetActive(false);
        menu.SetActive(false);

        // Esperar a que todas las animaciones terminen
        yield return new WaitForSeconds(3f);

        // Cambiar a la escena "Ajustes"
        SceneManager.LoadScene("Ajustes");
    }

    // Corrutina que gestiona las animaciones y el cambio de escena de la Información
    IEnumerator AnimarYCambiarEscenaInfo()
    {
        // Sacar el texto del botón
        logoInfo.transform.SetParent(info.transform);

        // Activar el panel rojo
        panelRojoInfo.SetActive(true);

        // Reproducir las animaciones
        logoInfoAnimator.Play("LogoInfo");
        textoInfoAnimator.Play("TextoInfo");
        panelRojoInfoAnimator.Play("SalidaPanelRojo");

        // Desactivar los objetos
        ejercicios.SetActive(false);
        ejerciciosDiarios.SetActive(false);
        estadisticas.SetActive(false);
        ajustes.SetActive(false);
        botonInfo.SetActive(false);
        menu.SetActive(false);

        // Esperar a que todas las animaciones terminen
        yield return new WaitForSeconds(3f);

        // Cambiar a la escena "Info"
        SceneManager.LoadScene("Info");
    }

    #endregion
}
