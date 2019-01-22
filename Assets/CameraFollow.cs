using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform anchorPos;
    public Transform asteroid;

    public Transform rotY;
    public Transform rotX;

    public float speedH = 2.0f;
    public float speedV = 2.0f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;

    Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = anchorPos.position - gameObject.transform.position;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = anchorPos.position + offset;

        Vector3 v3 = transform.position - asteroid.position;
        transform.rotation = Quaternion.FromToRotation(transform.up, v3) * transform.rotation;

        yaw += speedH * Input.GetAxis("Mouse X");
        pitch -= speedV * Input.GetAxis("Mouse Y");

        rotY.localEulerAngles = new Vector3(pitch, 0.0f, 0.0f);
        rotX.localEulerAngles = new Vector3(0.0f, yaw, 0.0f);
    }
}
