using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public TMP_Text maxPointText;
    void Start()
    {
        int maxScore = PlayerPrefs.GetInt("maxPoint");
        maxPointText.text = "Your Maximum Score = " + maxScore;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayGame();
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
        }

    public void ExitGame()
    {
        Application.Quit();
    }
    
}
