using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogoutScript : MonoBehaviour
{
    public void CallLogout()
    {
        StartCoroutine(SaveUserData());
    }

    IEnumerator SaveUserData()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", PlayerPrefs.GetString("username"));
        form.AddField("score", PlayerPrefs.GetInt("highscore"));
        form.AddField("privacyconditions", PlayerPrefs.GetString("privacyvoorwaarden"));
        form.AddField("blacktext", PlayerPrefs.GetString("zwartetext"));
        WWW www = new WWW("https://koopoverheid.000webhostapp.com/updatescore.php", form);
        yield return www;
        PlayerPrefs.SetInt("isLoggedIn", 0);
        PlayerPrefs.DeleteKey("username");
        PlayerPrefs.DeleteKey("highscore");
        PlayerPrefs.DeleteKey("privacyvoorwaarden");
        PlayerPrefs.DeleteKey("zwartetext");
        SceneManager.LoadScene("PreMainScene");
    }
}
