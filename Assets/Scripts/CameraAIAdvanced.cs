using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Christoffer Brandt
/// </summary>
public class CameraAI2 : MonoBehaviour
{
    public string tag = "Player";
    public float cameraSpeed = 1.0f;
    public float cameraRotationSpeed = 30.0f;
    private Vector3 cameraOffset;

    void Start()
    {
        Transform player = GameObject.FindGameObjectWithTag(tag).transform;
        cameraOffset = player.position - transform.position;
    }

    void Update()
    {
        Transform player = GameObject.FindGameObjectWithTag(tag).transform;
        Vector3 goalPosition;
        Quaternion goalRotation;

        //Move player to same position (This will not show up in game, all this happens in the frame before rendering
        goalPosition = player.position;

        //Change orientation of the camera
        goalRotation = Quaternion.LookRotation(player.up, player.forward);


        //Move camera back to the original position relative to the player (cameraoffset) and rotation
        goalPosition -= (transform.rotation * cameraOffset);

        Vector3 goalDirection = (goalPosition - transform.position);
        Vector3 goalOffset = goalDirection * cameraSpeed * Time.deltaTime;
        if (Vector3.Dot((goalPosition - transform.position), goalPosition - (transform.position + goalOffset)) > 0.0f)
        {
            transform.position += goalOffset;
        }
        else
        {
            transform.position = goalPosition;
        }

        transform.rotation = Quaternion.RotateTowards(transform.rotation, goalRotation, cameraRotationSpeed);
    }
}