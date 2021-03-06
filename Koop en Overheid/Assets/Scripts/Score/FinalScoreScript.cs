﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalScoreScript : MonoBehaviour
{
    public Text latestScoreDisplayText, highScoreDisplayText, AllQuestions;

    void Start()
    {
        latestScoreDisplayText.text = "Leeftijd Bereikt: " + PlayerPrefs.GetInt("latestage");
        highScoreDisplayText.text = "High Score: " + PlayerPrefs.GetInt("highscore");
        AllQuestions.text = PlayerPrefs.GetString("questionsscore");
    }
}
