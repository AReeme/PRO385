using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : MonoBehaviour
{
    float hoverSpeed = 1f;
    float hoverHeight = 0.5f;
    Vector3 originalPosition;

    void Start()
    {
        originalPosition = new Vector3(1.89f, 1.692f, 4.918f);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 hoverPosition = originalPosition;
        hoverPosition.y += Mathf.Sin(Time.time * hoverSpeed) * hoverHeight;
        transform.position = hoverPosition;
    }
}
