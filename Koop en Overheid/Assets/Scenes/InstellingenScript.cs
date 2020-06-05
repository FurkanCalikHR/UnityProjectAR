using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InstellingenScript : MonoBehaviour
{
    public Toggle privacyVoorwaarden, zwarteText;

    public void Start()
    {
        if(PlayerPrefs.GetString("privacyvoorwaarden").Equals("True"))
        {
            privacyVoorwaarden.isOn = true;
        }
        else
        {
            privacyVoorwaarden.isOn = false;
        }

        if (PlayerPrefs.GetString("zwartetext").Equals("True"))
        {
            zwarteText.isOn = true;
        }
        else
        {
            zwarteText.isOn = false;
        }
    }

    public void UpdateSettings()
    {
        PlayerPrefs.SetString("privacyvoorwaarden", privacyVoorwaarden.isOn ? "True" : "False");
        PlayerPrefs.SetString("zwartetext", zwarteText.isOn ? "True" : "False");
        CallSaveData();
    }

    private void CallSaveData()
    {
        StartCoroutine(SaveUserData());
    }

    IEnumerator SaveUserData()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", PlayerPrefs.GetString("username"));
        form.AddField("privacyconditions", PlayerPrefs.GetString("privacyvoorwaarden"));
        form.AddField("blacktext", PlayerPrefs.GetString("zwartetext"));
        WWW www = new WWW("https://koopoverheid.000webhostapp.com/updatesettings.php", form);
        yield return www;
    }
}
