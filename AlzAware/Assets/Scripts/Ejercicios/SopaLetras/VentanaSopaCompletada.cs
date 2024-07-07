using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VentanaSopaCompletada : MonoBehaviour
{
    public GameObject winPopUp;
    void Start()
    {
        winPopUp.SetActive(false);
    }
    private void OnEnable()
    {
        EventosJuego.OnBoardCompleted += ShowWinPopUp;
    }
    private void OnDisable()
    {
        EventosJuego.OnBoardCompleted -= ShowWinPopUp;
    }
    private void ShowWinPopUp()
    {
        winPopUp.SetActive(true);
    }

    public void LoadNextLevel()
    {
        EventosJuego.LoadNextLevelMethod();
    }
}
