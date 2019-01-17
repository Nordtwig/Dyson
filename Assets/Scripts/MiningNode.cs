using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiningNode : MonoBehaviour
{
    public int resourceValue = 5;
    
    public void OnBoxSpawn()
    {
        resourceValue--;
        if (resourceValue == 0)
        {
            Destroy(gameObject);
        }
    }
}
