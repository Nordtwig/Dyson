using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteroid : MonoBehaviour
{
    [SerializeField] GameObject dangerZone;
    private GameObject zone;

    private void Start()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit))
        {
            zone = Instantiate(dangerZone, hit.point, Quaternion.identity);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(zone);
        Destroy(gameObject.transform.parent.gameObject);        
    }
}
