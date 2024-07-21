using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public float offsetMultiplier = 1f;
    public float smoothTime = .3f;

    private Vector2 startPosition;
    private Vector3 velocity;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        Vector2 offset = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        if (offset != null && !float.IsNaN(offset.y) && !float.IsNaN(offset.x))
        {

           transform.position = Vector3.SmoothDamp(transform.position, startPosition + (offset * offsetMultiplier), ref velocity, smoothTime);
        }
    }
}
