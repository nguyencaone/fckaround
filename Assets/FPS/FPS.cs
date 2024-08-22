using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FPS : MonoBehaviour
{
    [SerializeField]TextMeshProUGUI txt;
    float frameCounter;
    float timeCounter;

    private void Start()
    {
        timeCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(timeCounter < 1)
        {
            timeCounter += Time.deltaTime;
            frameCounter++;
        }
        else
        {
            txt.text = ((int)(frameCounter / timeCounter)).ToString();
            timeCounter = 0;
            frameCounter = 0;
        }
    }
}
