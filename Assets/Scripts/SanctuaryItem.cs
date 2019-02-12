using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SanctuaryItem : PickupableObject
{
    public override void PickUpItem()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 10);
        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].tag == "Rig")
            {
                hitColliders[i].transform.GetComponent<MiningRig>().shielded = false;
            }
        }

        base.PickUpItem(); 
        player.SetEnableHoldingSanctuary(true);
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        AudioManager.instance.PlayOnPos("Box Collision", transform);
    }
}
