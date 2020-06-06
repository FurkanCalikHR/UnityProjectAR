using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class ARSceneChange : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider slider;
    public TextMeshProUGUI progressText;

    public void ChangeScene()
    {
        StartCoroutine(LoadSceneASync());
    }

    IEnumerator LoadSceneASync()
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("Zwempie");

        slider.value = 0;
        loadingScreen.SetActive(true);

        while(!asyncOperation.isDone)
        {
            float val = Mathf.Clamp01(asyncOperation.progress / .9f);

            slider.value = val;
            progressText.text = val * 100f + "%";

            yield return null;
        }
    }
}
