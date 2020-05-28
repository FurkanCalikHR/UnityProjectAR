using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreTable : MonoBehaviour
{
    private Transform entryContainer;
    private Transform entryTemplate;

    private void Awake(){
        entryContainer = transform.Find("highscoreEntryContainer");
        entryTemplate = entryContainer.Find("highscoreEntryTemplate");

        entryTemplate.gameObject.SetActive(false);

        float templateHeight = 80f;
        for (int i = 0; i <10; i++){
            Transform entryTransform = Instantiate(entryTemplate, entryContainer);
            RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
            entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * i);
            entryTransform.gameObject.SetActive(true);

            // int rank = i + 1;
            // string rankString;
            // switch (rank) {
            //     default:
            //         rankString = rank + "TH"; break;

            // case 1: rankString = "1ST"; break; 
            // case 2: rankString = "2ND"; break; 
            // case 3: rankString = "3RD"; break;  
            // }

            // entryTransform.Find("posText").GetComponent<Text>().text = rankString;

            // int score = Random.Range(0, 120);

            // entryTransform.Find("scoreText").GetComponent<Text>().text = score.ToString();

            // string name = "KOOP";

            // entryTransform.Find("nameText").GetComponent<Text>().text = name;          

        }
    }
}
