using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resize : MonoBehaviour
{
    public float referenceSize = 10f;

    void Update()
    {
        float screenRatio = (float)Screen.width / (float)Screen.height;
        float objectWidth = referenceSize * screenRatio;
        transform.localScale = new Vector3(objectWidth, referenceSize, 1f);

        // Optional: Center the GameObject on the screen
        transform.position = new Vector3(0, 0, 0f);
    }
}
