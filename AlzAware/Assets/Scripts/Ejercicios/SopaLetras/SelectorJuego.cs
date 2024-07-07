using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorJuego : MonoBehaviour
{
    public DatosJuego currentGameData;
    public DatosNivelJuego levelData;

    void Awake()
    {
        SelectSequentalBoardData();
    }


    private void SelectSequentalBoardData()
    {
        foreach (var data in levelData.data)
        {
            if (data.categoryName == currentGameData.selectedCategoryName)
            {
                var boardIndex = 0;
                if (boardIndex < data.boardData.Count)
                {
                    currentGameData.selectedBoardData = data.boardData[boardIndex];
                }
                else
                {
                    var randomIndex = Random.Range(0, data.boardData.Count);
                    currentGameData.selectedBoardData = data.boardData[randomIndex];
                }
            }
        }
    }

}
