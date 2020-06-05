using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RegistrationScript : MonoBehaviour
{
    public InputField usernameField, passwordField, governmentField;

    public Button registerButton;

    public Toggle termsAndConditions;

    public TextMeshProUGUI errorDisplay, usernameErrorLog, passwordErrorLog, governmentErrorLog;

    public void CallRegister()
    {
        StartCoroutine(Register());
    }

    IEnumerator Register()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", usernameField.text);
        form.AddField("password", passwordField.text);
        form.AddField("government", governmentField.text);
        form.AddField("termsandconditions", termsAndConditions.isOn.ToString());
        WWW www = new WWW("https://koopoverheid.000webhostapp.com/register.php", form);
        yield return www;
        if(www.text == "0")
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("PreMainScene");
        } else
        {
            ResetFields();
            errorDisplay.text = www.text;
        }
    }

    public void VerifyInput()
    {
        VerifyInputFields();
        VerifyRegisterButton();
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
        else if (passwordField.text.Length < 6)
        {
            passwordErrorLog.enabled = true;
        }

        if (governmentField.text.Length >= 3)
        {
            governmentErrorLog.enabled = false;
        }
        else if (governmentField.text.Length < 3)
        {
            governmentErrorLog.enabled = true;
        }
    }

    private void VerifyRegisterButton()
    {
        registerButton.interactable = (usernameField.text.Length >= 5 &&
          passwordField.text.Length >= 6 && governmentField.text.Length >= 3);
    }

    private void ResetFields()
    {
        usernameField.text = string.Empty;
        passwordField.text = string.Empty;
        governmentField.text = string.Empty;
        usernameErrorLog.enabled = true;
        passwordErrorLog.enabled = true;
        governmentErrorLog.enabled = true;
    }
}
