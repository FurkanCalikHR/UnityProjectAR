using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{

    [SerializeField]
    private List<Question> questions;

    private Question currentQuestion;

    [SerializeField]
    private List<CharacterDisplay> characters;

    private int currentIndex = 0, age = 0, currentCharacterIndex = 0;

    public List<Button> options;

    public TextMeshProUGUI questionAnswerDisplay, scoreDisplay;

    public Slider slider;

    public Text correctIncorrectTextDisplay;

    private StringBuilder wq = new StringBuilder();

    private string[] optionsTags = { "A", "B", "C", "D" };


    void Start()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        characters.ForEach(chars => chars.character.SetActive(false));
        characters[0].character.SetActive(true);
        currentQuestion = questions[0];
        questionAnswerDisplay.color = PlayerPrefs.GetString("zwartetext").Equals("True") ? Color.black : Color.white;
        scoreDisplay.color = PlayerPrefs.GetString("zwartetext").Equals("True") ? Color.black : Color.white;
        if (currentQuestion != null)
        {
            ShowOptions();
            BuildDisplayText();
        }
    }

    public void MultipleChoiceSelection(int index)
    {
        if(currentQuestion != null)
        {
            SubmitAnswer(options[index], currentQuestion.answerOptions[index]);
        }
    }

    private void SubmitAnswer(Button button, string answer)
    {
        if(currentQuestion != null)
        {
            if (currentQuestion.answer.Equals(answer))
            {
                AddAge(currentQuestion.points);
                CharacterSwap();
                characters[currentCharacterIndex].character.GetComponent<Animator>().Play("Happy Idle");
                DisplayAnswer(true);
            }
            else
            {
                UpdateQuestionScore(currentQuestion);
                characters[currentCharacterIndex].character.GetComponent<Animator>().Play("Angry");
                DisplayAnswer(false);
            }
            UpdateProgressBar();
            NextQuestion();
        }
    }

    private void NextQuestion()
    {
        currentIndex += 1;
        if(currentIndex < questions.Count)
        {
            Question nextQuestion = questions[currentIndex];
            if (nextQuestion != null)
            {
                currentQuestion = nextQuestion;
                ShowOptions();
                BuildDisplayText();
            }
        }
        else
        {
            EndQuiz();
        }
    }

    private void CharacterSwap()
    {
        if(currentCharacterIndex < characters.Count)
        {
            for(int i = 0; i < characters.Count; i++)
            {
                if (age >= characters[i].minAge && age <= characters[i].maxAge)
                {
                    SetCharacterActive(i);
                }
            }
        }
    }
    private void SetCharacterActive(int index)
    {
        currentCharacterIndex = index;
        characters.ForEach(chars => chars.character.SetActive(false));
        characters[currentCharacterIndex].character.SetActive(true);
    }

    private void ShowOptions()
    {
        if(currentQuestion != null)
        {
            options.ForEach(option => option.gameObject.SetActive(false));
            for(int i = 0; i < currentQuestion.answerOptions.Count; i++)
            {
                options[i].gameObject.SetActive(true);
            }
        }
    }

    private void UpdateProgressBar()
    {
        float val = ((float) questions.Count) / ((float) (currentIndex + 1));
        slider.value = (float) (1.0f / val);
    }

    private void EndQuiz()
    {
        PlayerPrefs.SetInt("latestage", age);
        PlayerPrefs.SetInt("highscore", PlayerPrefs.GetInt("highscore") < age ? age : PlayerPrefs.GetInt("highscore"));       
        PlayerPrefs.SetString("questionsscore", wq == null ? "Gefeliciteerd, u heeft alles goed beantwoordt!" : wq.ToString());
        CallSaveData();
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("FinalScoreScene");

    }

    public void CallSaveData()
    {
        StartCoroutine(SaveUserData());
    }

    IEnumerator SaveUserData()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", PlayerPrefs.GetString("username"));
        form.AddField("score", PlayerPrefs.GetInt("highscore"));
        WWW www = new WWW("https://koopoverheid.000webhostapp.com/updatescore.php", form);
        yield return www;
    }

    private void BuildDisplayText()
    {
        StringBuilder builder = new StringBuilder();
        builder.Append(currentQuestion.question + "\n");
        for(int i = 0; i < currentQuestion.answerOptions.Count; i++)
        {
            builder.Append(optionsTags[i] + ") " + currentQuestion.answerOptions[i] + "\n");
        }
        questionAnswerDisplay.text = builder.ToString();
    }

    private void AddAge(int ageToAdd)
    {
        age += ageToAdd;
        scoreDisplay.text = "Leeftijd: " + age;
    }

    public void UpdateQuestionScore(Question currentQuestion)
    {
        wq.Append(currentQuestion.question + "\n");
        wq.Append("Antwoord = " + optionsTags[currentQuestion.answerOptions.IndexOf(currentQuestion.answer)] + ") " + currentQuestion.answer + "\n\n");
    }

    public void DisplayAnswer(bool correct)
    {
        StartCoroutine(ShowMessage(correct, 2));
    }

    IEnumerator ShowMessage(bool correct, float delay)
    {
        correctIncorrectTextDisplay.text = correct ? "Goed Gedaan! +1" : "Fout!";
        correctIncorrectTextDisplay.enabled = true;
        correctIncorrectTextDisplay.color = correct ? Color.green : Color.red;
        yield return new WaitForSeconds(delay);
        correctIncorrectTextDisplay.enabled = false;
    }
}

[System.Serializable]
public class Question
{
    public int points;
    public string question, answer;
    public List<string> answerOptions;
}

[System.Serializable]
public class CharacterDisplay
{
    public int minAge, maxAge;
    public GameObject character;
}