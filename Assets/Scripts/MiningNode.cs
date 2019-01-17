using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiningNode : MonoBehaviour
{
    [SerializeField] private int resourceValue = 5;
    
    void OnBoxSpawn()
    {
        resourceValue--;
        if (resourceValue == 0)
        {
            Destroy(gameObject);
        }
    }
}
