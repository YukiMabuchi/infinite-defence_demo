using UnityEngine;

public class Rotate : MonoBehaviour
{

    public float rotateSpeed;

    void Update()
    {
        transform.Rotate(0, rotateSpeed * Time.deltaTime, 0f); // if rotateSpeed is 25, 25 degrees per second
    }
}