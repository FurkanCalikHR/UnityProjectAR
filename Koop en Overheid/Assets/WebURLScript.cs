using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebURLScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Open()
    {
        Application.OpenURL("https://www.koopoverheid.nl/");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
