using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Created by Petter
/// </summary>

public class MiniMapIcon : MonoBehaviour
{

    MiningRig miningRig;
    Material material;

    //Start is called before the first frame update
    void Start()
    {
        miningRig = transform.parent.gameObject.GetComponent<MiningRig>();
        material = gameObject.GetComponent<MeshRenderer>().material;
    }

    //Update is called once per frame
    void Update()
    {
        if (miningRig.functioning && miningRig.coBoxSpawnRunning)
        {
            material.color = Color.green;
        }
        else if (miningRig.functioning && !miningRig.coBoxSpawnRunning)
        {
            material.color = Color.red;
        }
        else
        {
            material.color = Color.yellow;
        }
    }
}
