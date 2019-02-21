using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SanctuaryItem : PickupableObject
{

    Animator shieldController;

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
        shieldController.SetBool("IsDeployed", false);
        player.SetEnableHoldingSanctuary(true);
    }

    private void OnEnable() {
        shieldController = transform.GetComponentInChildren<Animator>();
        shieldController.SetBool("IsDeployed", true);
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Asteroid")
        {
            AudioManager.instance.PlayOnPos("Box Collision", transform);
        }
    }
}
