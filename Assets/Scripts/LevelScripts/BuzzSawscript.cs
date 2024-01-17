using UnityEngine;
//made by Aiden
public class BuzzSawscript : MonoBehaviour
{
    public float rotationSpeed = 50f;

    void Update()
    {
        // Rotate the object around the Z-axis at a constant speed
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }
}