using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArScreenOrientationScript : MonoBehaviour
{
    public ScreenOrientation orientation;

    public void Rotate()
    {
        Screen.orientation = orientation;
    }

}
