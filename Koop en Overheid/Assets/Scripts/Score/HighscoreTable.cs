using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreTable : MonoBehaviour
{
    private Transform entryContainer;
    private Transform entryTemplate;

    private List<HighscoreEntry> highscoresEntries = new List<HighscoreEntry>();

    private void Awake() {
        StartCoroutine(GetData());
    }

    IEnumerator GetData()
    {
        WWW www = new WWW("https://koopoverheid.000webhostapp.com/score.php");
        yield return www;
        string[] result = www.text.Split('\n');
        for(int i = 0; i < result.Length; i++)
        {
            if(result[i] != null)
            {
                string[] information = result[i].Split('\t');
                if(information.Length >= 2)
                {
                    string username = information[0];
                    int score = int.Parse(information[1]);
                    highscoresEntries.Add(new HighscoreEntry(username, score));
                }
            }
        }
        BuildHighscores();
        
    }

    private void BuildHighscores()
    {
        entryContainer = transform.Find("highscoreEntryContainer");
        entryTemplate = entryContainer.Find("highscoreEntryTemplate");

        entryTemplate.gameObject.SetActive(false);

        float templateHeight = 80f;
        for (int i = 0; i < highscoresEntries.Count; i++)
        {
            Transform entryTransform = Instantiate(entryTemplate, entryContainer);
            RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
            entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * i);
            entryTransform.gameObject.SetActive(true);

            int rank = i + 1;
            string rankString;
            switch (rank)
            {
                default:
                    rankString = rank + "e"; break;

                case 1: rankString = "1ste"; break;
            }

            entryTransform.Find("posText").GetComponent<Text>().text = rankString;
            entryTransform.Find("scoreText").GetComponent<Text>().text = highscoresEntries[i].score.ToString();
            entryTransform.Find("nameText").GetComponent<Text>().text = highscoresEntries[i].username;
            entryTransform.Find("background").gameObject.SetActive(rank % 2 == 0);

        }
    }
}

public class HighscoreEntry
{
    public string username;
    public int score;

    public HighscoreEntry(string username, int score)
    {
        this.username = username;
        this.score = score;
    }
}
