using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtMouse : MonoBehaviour
{
    public int minRotation = 90;
    public int maxRotation = -90;

    void Update()
    {
        // Rotation
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 5.23f;

        Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        // Scale adjustment
        Vector3 normalScale = new Vector3(1f, 1f, 1f);
        Vector3 flippedScale = new Vector3(1f, -1f, 1f);

        float deltaAngle = Mathf.DeltaAngle(0, transform.eulerAngles.z);

        if (deltaAngle < minRotation && deltaAngle > maxRotation)
        {
            transform.localScale = normalScale;
            Debug.Log("normal");
        }
        else
        {
            transform.localScale = flippedScale;
            Debug.Log("flipped");
        }
    }
}