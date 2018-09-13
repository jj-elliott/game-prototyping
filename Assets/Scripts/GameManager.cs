using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour {

    [SerializeField]
    public int gameSceneIndex, titleSceneIndex;
    public static GameManager instance;
    private void Start()
    {
        if(instance == null)
        {
            instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(gameSceneIndex);
    }

    public void EndGame()
    {
        SceneManager.LoadScene(titleSceneIndex);
    }
}
