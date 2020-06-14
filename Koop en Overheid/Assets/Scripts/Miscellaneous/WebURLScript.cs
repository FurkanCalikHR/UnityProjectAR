using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebURLScript : MonoBehaviour
{

    public void Open()
    {
        Application.OpenURL("https://data.overheid.nl/");
    }

    public void DownloadPdf()
    {
        Application.OpenURL("https://data.overheid.nl/sites/default/files/uploaded_files/Handreiking%20data.overheid.nl%202.0.pdf");
    }
    
}
