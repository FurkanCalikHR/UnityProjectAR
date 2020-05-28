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

    public GameObject character;

    private int currentIndex = 0, score = 0;

    public List<Button> options;

    public TextMeshProUGUI questionAnswerDisplay, scoreDisplay;

    private FinalScoreScript finalScoreScript;

    void Start()
    {
        finalScoreScript = GameObject.FindObjectOfType<FinalScoreScript>();
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
                AddScore(currentQuestion.points);
                character.GetComponent<Animator>().Play("Happy Idle");
                ApplyColor(button, Color.green);
            } else
            {
                character.GetComponent<Animator>().Play("Angry");
                ApplyColor(button, Color.red);
            }
            NextQuestion();
        }
    }

    private void NextQuestion()
    {
        currentIndex = currentIndex + 1;
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

    private void EndQuiz()
    {
        PlayerPrefs.SetInt("latestscore", score);
        if(PlayerPrefs.GetInt("highscore") < score)
        {
            PlayerPrefs.SetInt("highscore", score);
        }
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene(6);
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

    private void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreDisplay.text = "Points: " + score;
    }

}

[System.Serializable]
public class Question
{
    public int points;
    public string question, answer;
    public List<string> answerOptions;
}