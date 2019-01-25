using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EjectBox : MonoBehaviour
{
    public GameObject BoxEjected;
    public Transform SpawnpointBox;
    public float timer;

    void Start()
    {
        Instantiate(BoxEjected, SpawnpointBox.position, SpawnpointBox.rotation);
        Destroy(BoxEjected, 1);
    }

    void Update()
    {
        
    }
}
