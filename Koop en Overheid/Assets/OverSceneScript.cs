using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class OverSceneScript : MonoBehaviour
{
    [TextArea(15, 20)]
    public string[] overSceneText;

    public TextMeshProUGUI displayedText, pageNumbers;

    private int currentPageIndex;

    public void Start()
    {
        currentPageIndex = 0;
        pageNumbers.text = (currentPageIndex + 1) + " / " + overSceneText.Length;
    }

    public void PreviousPage()
    {
        currentPageIndex--;

        if (currentPageIndex < 0)
        {
            currentPageIndex = 0;
        }

        displayedText.text = overSceneText[currentPageIndex];
        pageNumbers.text = (currentPageIndex + 1) + " / " + overSceneText.Length;
    }

    public void NextPage()
    {
        currentPageIndex++;

        if (currentPageIndex > overSceneText.Length - 1)
        {
            currentPageIndex = overSceneText.Length - 1;
        }

        displayedText.text = overSceneText[currentPageIndex];
        pageNumbers.text = (currentPageIndex + 1) + " / " + overSceneText.Length;
    }
}
