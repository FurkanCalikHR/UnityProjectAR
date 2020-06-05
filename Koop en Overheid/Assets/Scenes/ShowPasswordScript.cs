using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.InputField;

public class ShowPasswordScript : MonoBehaviour
{
    public InputField passwordInput;

    public void ShowPassword()
    {
        if (passwordInput.inputType == InputType.Standard)
        {
            passwordInput.inputType = InputType.Password;
            passwordInput.ForceLabelUpdate();
        }
        else if (passwordInput.inputType == InputType.Password)
        {
            passwordInput.inputType = InputType.Standard;
            passwordInput.ForceLabelUpdate();
        }
    }
}
