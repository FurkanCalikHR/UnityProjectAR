using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Naam_Overheid : MonoBehaviour
{
    public string NaamMed;
    public string Overheid;
    public GameObject inputFieldNaam;
    public GameObject inputFieldOverheid;
    public GameObject textDisplay;

    public void Opslaan()
    {
        NaamMed = inputFieldNaam.GetComponent<Text>().text;
        //textDisplay.GetComponent<Text>().text = "Welkom" + NaamMed;
        Overheid = inputFieldOverheid.GetComponent<Text>().text;
        textDisplay.GetComponent<Text>().text = "Welkom " + NaamMed + " van " + Overheid;
    }
}
