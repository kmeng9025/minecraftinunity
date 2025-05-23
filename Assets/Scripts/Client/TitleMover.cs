using System;
using UnityEngine;

public class TitleMover : MonoBehaviour
{
    int startPos = -20;
    float endPos = -25;
    bool movingUp = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(movingUp){
            transform.position = new Vector3(0, 10, 9);
            if(Mathf.Approximately(transform.position.z, endPos)){
                movingUp = false;
            }
        }
        // else {
        //     transform.position = new Vector3(0, 10, Mathf.Lerp(transform.position.z, startPos, 0.2f));
        //     if(Mathf.Approximately(transform.position.z, startPos)){
        //         movingUp = true;
        //     }
        // }
    }

    float Lerp(float a, float b, float t)
    {
        return a + (b - a) * t;
    }
}
