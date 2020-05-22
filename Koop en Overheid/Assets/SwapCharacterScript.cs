using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapCharacterScript : MonoBehaviour
{
    public GameObject[] gameObjects;

    // Start is called before the first frame update
    public void Swap()
    {
        if(gameObjects[0].activeInHierarchy)
        {
            gameObjects[0].SetActive(false);
            gameObjects[1].SetActive(true);
        } else {
            gameObjects[1].SetActive(false);
            gameObjects[0].SetActive(true);
        }
    }

   
}
