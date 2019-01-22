using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Christoffer Brandt
/// </summary>

public class CameraAI : MonoBehaviour
{
    public string tag = "Player";
    public float cameraSpeed = 1.0f;
    public float cameraRotationSpeed = 30.0f;
    private Vector3 cameraOffset;

    void Start()
    {
        Transform player = GameObject.FindGameObjectWithTag(tag).transform;
        cameraOffset = transform.position - player.position;
    }

    void Update()
    {
        Transform player = GameObject.FindGameObjectWithTag(tag).transform;

        transform.position = player.position + cameraOffset;
    }
  
}
    