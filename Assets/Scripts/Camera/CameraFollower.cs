using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public Transform camerapoint;
    public float speed;

    void Start()
    {
        
    }

    private void LateUpdate()
    {
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            transform.position = Vector2.Lerp(transform.position, camerapoint.position, Time.deltaTime * speed);
        }
    }
}
