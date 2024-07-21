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
        Debug.Log(-Mathf.Infinity == offset.y);
        Debug.Log(+Mathf.Infinity == offset.y);


        if (offset != null && !float.IsNaN(offset.y) && !float.IsNaN(offset.x)&& -Mathf.Infinity != offset.y && Mathf.Infinity != offset.y && -Mathf.Infinity != offset.x && Mathf.Infinity != offset.x)
        {

           transform.position = Vector3.SmoothDamp(transform.position, startPosition + (offset * offsetMultiplier), ref velocity, smoothTime);
        }
        else
        {
            Debug.Log("CANT DO IT IS NAN");
        }
    }
}
