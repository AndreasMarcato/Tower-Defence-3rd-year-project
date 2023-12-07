using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpVisual : MonoBehaviour
{
    float rotationSpeed = 360;

    private void Update()
    {
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
}
