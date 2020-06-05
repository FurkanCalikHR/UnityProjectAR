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

    public List<GameObject> characters;

    private int currentIndex = 0, age = 0, currentCharacterIndex = 0;

    public List<Button> options;

    public TextMeshProUGUI questionAnswerDisplay, scoreDisplay;

    public Slider slider;

    public GameObject textDisplay;

    private string wq;


    void Start()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        characters.ForEach(chars => chars.SetActive(false));
        characters[0].SetActive(true);
        currentQuestion = questions[0];
        if(currentQuestion != null)
        {
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
            options.ForEach(baseButton => ApplyColor(baseButton, Color.white));
            if (currentQuestion.answer.Equals(answer))
            {
                AddAge(currentQuestion.points);
                CharacterSwap();
                characters[currentCharacterIndex].GetComponent<Animator>().Play("Happy Idle");
                ApplyColor(button, Color.green);
            }
            else
            {
                UpdateQuestionScore(currentQuestion);
                characters[currentCharacterIndex].GetComponent<Animator>().Play("Angry");
                ApplyColor(button, Color.red);
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
                options.ForEach(baseButton => ApplyColor(baseButton, Color.white));
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
            if (age >= 0 && age <= 1)
            {
                SetCharacterActive(0);
            }
            else if(age >= 2 && age <= 4)
            {
                SetCharacterActive(1);
            }
            else if (age >= 5)
            {
                SetCharacterActive(2);
            }
        }
    }

    private void UpdateProgressBar()
    {
        float val = ((float) questions.Count) / ((float) (currentIndex + 1));
        slider.value = (float) (1.0f / val);
    }

    private void SetCharacterActive(int index)
    {
        currentCharacterIndex = index;
        characters.ForEach(chars => chars.SetActive(false));
        characters[currentCharacterIndex].SetActive(true);
    }

    private void EndQuiz()
    {
        PlayerPrefs.SetInt("latestage", age);
        PlayerPrefs.SetInt("highscore", PlayerPrefs.GetInt("highscore") < age ? age : PlayerPrefs.GetInt("highscore"));
        PlayerPrefs.SetString("questionsscore", wq == null ? "You have answered everything correctly!" : wq.ToString());
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
        string[] optionsTags = {"A", "B", "C", "D"};
        StringBuilder builder = new StringBuilder();
        builder.Append((currentIndex + 1) + ". " + currentQuestion.question + "\n");
        for(int i = 0; i < currentQuestion.answerOptions.Count; i++)
        {
            builder.Append(optionsTags[i] + ") " + currentQuestion.answerOptions[i] + "\n");
        }
        questionAnswerDisplay.text = builder.ToString();
    }

    private void ApplyColor(Button button, Color color)
    {
        var colors = button.colors;
        colors.disabledColor = color;
        colors.highlightedColor = color;
        colors.selectedColor = color;
        colors.normalColor = color;
        button.colors = colors;
    }

    private void AddAge(int ageToAdd)
    {
        age += ageToAdd;
        scoreDisplay.text = "Leeftijd: " + age;
    }

    public void UpdateQuestionScore(Question currentQuestion)
    {
        wq = wq + currentQuestion.question + "\n\n" + "A: " + currentQuestion.answerOptions[0] + "\n" + 
            "B: " + currentQuestion.answerOptions[1] + "\n" + "C: " + currentQuestion.answerOptions[2] + "\n" +
            "D: " + currentQuestion.answerOptions[3] + "\n\n" + "Antwoord = " + currentQuestion.answer + "\n\n";
    }
}

[System.Serializable]
public class Question
{
    public int points;
    public string question, answer;
    public List<string> answerOptions;
}