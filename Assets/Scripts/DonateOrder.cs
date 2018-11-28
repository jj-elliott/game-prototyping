using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonateOrder : Order {

    static float stopDistance =.05f;
    public UnitProducer prod;
    Vector3 targetLocation;

    public DonateOrder(UnitProducer prod)
    {
        this.prod = prod;
        this.targetLocation = prod.spawnLocation.position;
        location = prod.spawnLocation.position;
    }

    public override bool Complete(Selectable sel)
    {
        if((targetLocation - sel.transform.position).magnitude < stopDistance)
        {
            if(prod.TeamIndex == sel.TeamIndex)
            {
                prod.UpdateBuildProgress(0.3f);
            }
            else
            {
                CaptureableUnitProducer capturablePro = (CaptureableUnitProducer)prod;
                if (sel.TeamIndex == 0)
                    capturablePro.UpdatePlayerCaptureProgress(0.1f);
                else
                    capturablePro.UpdateEnemyCaptureProgress(0.1f);
            }
            UnitCombat unit = sel.transform.GetComponentInChildren<UnitCombat>();
            if(unit)
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
