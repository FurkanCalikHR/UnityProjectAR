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
            } else
            {
                characters[currentCharacterIndex].GetComponent<Animator>().Play("Angry");
                ApplyColor(button, Color.red);
            }
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
            if (age >= 0 && age <= 5)
            {
                SetCharacterActive(0);
            }
            else if(age >= 6 && age <= 11)
            {
                SetCharacterActive(1);
            }
            else if (age >= 12 && age <= 18)
            {
                SetCharacterActive(2);
            }
            else if (age >= 19)
            {
                SetCharacterActive(3);
            }
        }
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
        if(PlayerPrefs.GetInt("highscore") < age)
        {
            PlayerPrefs.SetInt("highscore", age);
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

    private void AddAge(int ageToAdd)
    {
        age += ageToAdd;
        scoreDisplay.text = "Leeftijd: " + age;
    }

}

[System.Serializable]
public class Question
{
    public int points;
    public string question, answer;
    public List<string> answerOptions;
}