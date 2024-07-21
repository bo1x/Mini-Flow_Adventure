using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scrool : MonoBehaviour
{
    [SerializeField] private RawImage _img;
    [SerializeField] private Vector2 dir;
    [SerializeField] private float speed;


    void Update()
    {
        _img.uvRect = new Rect(_img.uvRect.position + dir *speed * Time.deltaTime, _img.uvRect.size);
    }
}
