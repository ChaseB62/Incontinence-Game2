using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanAnim : MonoBehaviour
{
    public Vector3 scaleSpeed;

    public bool upOrDown = false;

    public float maxScale;
    public float minScale;

    public float currentScale;

    void Update()
    {
        currentScale = transform.localScale.x;

        if(!upOrDown){
            transform.localScale -= scaleSpeed;
        } else {
            transform.localScale += scaleSpeed;
        }

        if(currentScale > maxScale){
            upOrDown = false;
        } else if (currentScale < minScale){
            upOrDown = true;
        }

    }
}

