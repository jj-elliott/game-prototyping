using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonateOrder : Order {

    static float stopDistance = 5.0f;
    private UnitProducer prod;
    Vector3 targetLocation;

    public DonateOrder(UnitProducer prod)
    {
        this.prod = prod;
        this.targetLocation = prod.spawnLocation.position;
    }

    public override bool Complete(Selectable sel)
    {
        if((targetLocation - sel.transform.position).magnitude < stopDistance)
        {
            if(prod.TeamIndex == 0)
            {
                prod.UpdateBuildProgress(0.3f);
            }
            else
            {
                CaptureableUnitProducer capturablePro = (CaptureableUnitProducer)prod;
                capturablePro.UpdatePlayerCaptureProgress(0.1f);
            }
            UnitCombat unit = sel.transform.GetComponentInChildren<UnitCombat>();
            unit.Damage(1000);
            return true;
        }
        return false;
    }

    public override void Step(Selectable sel)
    {
        //TODO: This is probably inneficient b/c we recalculate route every frame
        sel.SetNavTarget(targetLocation);
    }
}
