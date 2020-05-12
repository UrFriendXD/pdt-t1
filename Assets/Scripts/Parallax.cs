using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float length, startPos;

    private Camera cam;

    public float parallaxEffect;
    
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        startPos = transform.position.y;
        length = GetComponent<MeshRenderer>().bounds.size.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float temp = cam.transform.position.y * (1 - parallaxEffect);
        float dist = (cam.transform.position.y * parallaxEffect);
        
        transform.position = new Vector3(transform.position.x, startPos + dist, transform.position.z);

        if (temp > startPos + length) startPos += length;
        else if (temp < startPos - length) startPos -= length;
    }
}
