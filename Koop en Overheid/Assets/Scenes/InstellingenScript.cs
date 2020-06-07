using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InstellingenScript : MonoBehaviour
{
    public Toggle privacyVoorwaarden, zwarteText;
    public TextMeshProUGUI resultLogger;

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
        if(www.text == "0")
        {
            StartCoroutine(ShowMessage("Uw instellingen zijn opgeslagen!", Color.green, 2));
        }
        else
        {
            StartCoroutine(ShowMessage("Er is iets mis gegaan met het opslaan!", Color.red, 2));
        }
    }

    IEnumerator ShowMessage(string text, Color color, float delay)
    {
        resultLogger.text = text;
        resultLogger.enabled = true;
        resultLogger.color = color;
        yield return new WaitForSeconds(delay);
        resultLogger.enabled = false;
    }
}
