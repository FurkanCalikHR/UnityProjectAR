using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalScoreScript : MonoBehaviour
{
    public Text latestScoreDisplayText, highScoreDisplayText;

    void Start()
    {
        latestScoreDisplayText.text = "Latest Score: " + PlayerPrefs.GetInt("latestscore");
        highScoreDisplayText.text = "High Score: " + PlayerPrefs.GetInt("highscore");
    }
}
