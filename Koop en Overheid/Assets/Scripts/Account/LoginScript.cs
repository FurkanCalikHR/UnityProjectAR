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
        //Username Field
        if (usernameField.text.Length >= 5)
        {
            if (usernameField.text.Length >= 10)
            {
                usernameErrorLog.text = "(Maximaal 10 tekens)";
                usernameErrorLog.enabled = true;
            }
            else
            {
                usernameErrorLog.enabled = false;
            }
        }
        else if (usernameField.text.Length < 5)
        {
            usernameErrorLog.text = "(Minimaal 5 tekens)";
            usernameErrorLog.enabled = true;
        }

        //Password Field
        if (passwordField.text.Length >= 6)
        {
            if (passwordField.text.Length >= 12)
            {
                passwordErrorLog.text = "(Maximaal 12 tekens)";
                passwordErrorLog.enabled = true;
            }
            else
            {
                passwordErrorLog.enabled = false;
            }
        }
        else if (passwordField.text.Length < 5)
        {
            passwordErrorLog.text = "(Minimaal 6 tekens)";
            passwordErrorLog.enabled = true;
        }
    }

    private void VerifyLoginButton()
    {
        loginButton.interactable = ((usernameField.text.Length >= 5 && usernameField.text.Length <= 10) &&
          (passwordField.text.Length >= 6 && passwordField.text.Length <= 12));
    }

    private void ResetFields()
    {
        usernameField.text = string.Empty;
        passwordField.text = string.Empty;
        usernameErrorLog.enabled = true;
        passwordErrorLog.enabled = true;
    }
}
