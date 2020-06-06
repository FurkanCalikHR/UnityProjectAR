using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RegistrationScript : MonoBehaviour
{
    public InputField usernameField, passwordField, governmentField;

    public Button registerButton;

    public Toggle privacyconditions;

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
        form.AddField("privacyconditions", privacyconditions.isOn ? "True" : "False");
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
        //Username Field
        if (usernameField.text.Length >= 5)
        {
            if(usernameField.text.Length >= 10)
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
        else if (passwordField.text.Length < 6)
        {
            passwordErrorLog.text = "(Minimaal 6 tekens)";
            passwordErrorLog.enabled = true;
        }

        //Government Field
        if (governmentField.text.Length >= 3)
        {
            if (governmentField.text.Length >= 20)
            {
                governmentErrorLog.text = "(Maximaal 20 tekens)";
                governmentErrorLog.enabled = true;
            }
            else
            {
                governmentErrorLog.enabled = false;
            }
        }
        else if (governmentField.text.Length < 3)
        {
            governmentErrorLog.text = "(Minimaal 3 tekens)";
            governmentErrorLog.enabled = true;
        }
    }

    private void VerifyRegisterButton()
    {
        registerButton.interactable = ((usernameField.text.Length >= 5 && usernameField.text.Length <= 10) &&
          (passwordField.text.Length >= 6 && passwordField.text.Length <= 12) && (governmentField.text.Length >= 3 && governmentField.text.Length <= 20));
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
