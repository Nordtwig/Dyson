using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SanctuaryItem : Box
{
    public override void PickUpBox()
    {
        base.PickUpBox();
        player.SetEnableHoldingBox(false);
        player.SetEnableHoldingSanctuary(true);
    }
}
