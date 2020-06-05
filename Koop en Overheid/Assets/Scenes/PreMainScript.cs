using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PreMainScript : MonoBehaviour
{
    void Awake()
    {
        if(PlayerPrefs.GetInt("isLoggedIn") == 1)
        {
            SceneManager.LoadScene("Main Scene");
        }
    }

    public void GoToRegister()
    {
        SceneManager.LoadScene("RegistrationScene");
    }

    public void GoToLogin()
    {
        SceneManager.LoadScene("LoginScene");
    }
}
