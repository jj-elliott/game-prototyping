using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverManager : MonoBehaviour {

    public GameObject victoryScreen, defeatScreen;


    public void OnVictory()
    {
        victoryScreen.SetActive(true);
    }
    public void OnDefeat()
    {
        defeatScreen.SetActive(true);
    }

    public void ExitToMenu()
    {
        GameManager.instance.EndGame();
    }
}
