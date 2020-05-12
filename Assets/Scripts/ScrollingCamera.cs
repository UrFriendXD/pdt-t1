using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingCamera : MonoBehaviour
{
    [SerializeField] private float speed;

    private void FixedUpdate()
    {
        transform.position = transform.position + (Vector3.up * (speed * Time.deltaTime));
    }
}
