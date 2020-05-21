using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{

    public GameObject PanelInfo;
    private string selected = "";

    private bool show = false;
    private int points = 0;

    public Button aOption;
    public Button bOption;
    public Button cOption;
    public Button dOption;

    public void ShowHideInfo()
    {
        if(!show)
        {
            PanelInfo.SetActive(true);
            show = true;
        }
        else
        {
            PanelInfo.SetActive(false);
            show = false;
        }
    }

    public void SelectA()
    {
        selected = "A";
        SubmitAnswer();
    }

    public void SelectB()
    {
        selected = "B";
        SubmitAnswer();
    }

    public void SelectC()
    {
        selected = "C";
        SubmitAnswer();
    }

    public void SelectD()
    {
        selected = "D";
        SubmitAnswer();
    }

    public void SubmitAnswer()
    {
        if (selected == "A")
        {
            ApplyColor(aOption, Color.red);
            ApplyColor(bOption, Color.white);
            ApplyColor(cOption, Color.white);
            ApplyColor(dOption, Color.white);
        }
        else if(selected == "B")
        {
            ApplyColor(aOption, Color.white);
            ApplyColor(bOption, Color.red);
            ApplyColor(cOption, Color.white);
            ApplyColor(dOption, Color.white);
        }
        else if(selected == "C")
        {
            ApplyColor(aOption, Color.white);
            ApplyColor(bOption, Color.white);
            ApplyColor(cOption, Color.red);
            ApplyColor(dOption, Color.white);
        }
        else
        {
            ApplyColor(aOption, Color.white);
            ApplyColor(bOption, Color.white);
            ApplyColor(cOption, Color.white);
            ApplyColor(dOption, Color.green);
        }
    }

    public void ApplyColor(Button button, Color color)
    {
        var colors = button.colors;
        colors.disabledColor = color;
        colors.highlightedColor = color;
        colors.selectedColor = color;
        colors.normalColor = color;
        button.colors = colors;
    }


}
