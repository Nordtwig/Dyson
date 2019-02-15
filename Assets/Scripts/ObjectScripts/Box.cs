using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Created by Svedlund and Heimer, Heavily modified by Robin now works of pickupable object SuperClass
/// </summary>

public class Box : PickupableObject
{

    //See SuperClass, player holding box set.
    public override void PickUpItem()
    {
        base.PickUpItem();
        player.SetEnableHoldingBox(true);
    }
    
    protected override void OnCollisionEnter(Collision collision)
    {
        AudioManager.instance.PlayOnPos("Box Collision", transform);
    }
}
