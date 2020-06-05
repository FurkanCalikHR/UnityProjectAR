using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoginScript : MonoBehaviour
{

    public InputField usernameField, passwordField;

    public Button loginButton;

    public TextMeshProUGUI errorDisplay, usernameErrorLog, passwordErrorLog;

    public void CallLogin()
    {
        StartCoroutine(Login());
    }

    IEnumerator Login()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", usernameField.text);
        form.AddField("password", passwordField.text);
        WWW www = new WWW("https://koopoverheid.000webhostapp.com/login.php", form);
        yield return www;
        if (www.text[0] == '0')
        {
            Debug.Log(www.text);
            PlayerPrefs.SetInt("isLoggedIn", 1);
            PlayerPrefs.SetString("username", usernameField.text);
            PlayerPrefs.SetInt("highscore", int.Parse(www.text.Split('\t')[1]));
            PlayerPrefs.SetString("privacyvoorwaarden", www.text.Split('\t')[2]);
            PlayerPrefs.SetString("zwartetext", www.text.Split('\t')[3]);
            UnityEngine.SceneManagement.SceneManager.LoadScene("Main Scene");
        }
        else
        {
            ResetFields();
            errorDisplay.text = www.text;
        }
    }

    public void VerifyInput()
    {
        VerifyInputFields();
        VerifyLoginButton();
    }

    private void VerifyInputFields()
    {
        if (usernameField.text.Length >= 5)
        {
            usernameErrorLog.enabled = false;
        }
        else if (usernameField.text.Length < 5)
        {
            usernameErrorLog.enabled = true;
        }

        if (passwordField.text.Length >= 6)
        {
            passwordErrorLog.enabled = false;
        }
        else if (passwordField.text.Length < 5)
        {
            passwordErrorLog.enabled = true;
        }
    }

    private void VerifyLoginButton()
    {
        loginButton.interactable = (usernameField.text.Length >= 5 &&
            passwordField.text.Length >= 6);
    }

    private void ResetFields()
    {
        usernameField.text = string.Empty;
        passwordField.text = string.Empty;
        usernameErrorLog.enabled = true;
        passwordErrorLog.enabled = true;
    }
}
