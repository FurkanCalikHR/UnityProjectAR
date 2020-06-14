using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainSceneScript : MonoBehaviour
{
    public TextMeshProUGUI usernameText, highscoreText;

    void Start()
    {
        usernameText.text = PlayerPrefs.GetString("username");
        highscoreText.text = PlayerPrefs.GetInt("highscore").ToString();
    }
    
}
