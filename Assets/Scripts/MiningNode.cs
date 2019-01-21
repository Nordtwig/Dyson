using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  Heimer created core, edited by Robin
/// </summary>

public class MiningNode : MonoBehaviour
{
    public int resourceValue = 5;

    public bool OnBoxSpawn()
    {
        resourceValue--;
        if (resourceValue == 0)
        {
            Destroy(gameObject);
            return false;
        }
        return true;
    }
}
