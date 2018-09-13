using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackOrder : Order
{
    private Transform target;
    private UnitCombat targetCombat;

    public AttackOrder(Transform target)
    {
        this.target = target;
        this.targetCombat = target.gameObject.GetComponent<UnitCombat>();
    }
    public override bool Complete(Selectable sel)
    {
        UnitWeapon weap = sel.gameObject.GetComponentInChildren<UnitWeapon>();

        if(weap == null || target == null)
        {
            return true;
        }
        return false;
    }

    public override void Step(Selectable sel)
    {
        UnitWeapon weap = sel.gameObject.GetComponentInChildren<UnitWeapon>();
        UnitBase unit = sel.gameObject.GetComponent<UnitBase>();
        if (weap == null || unit == null)
        {
            return;
        }
        unit.SetWeaponTarget(target);
        float distToTarget = (target.position - sel.transform.position).magnitude;
        if (distToTarget < weap.MaxRange)
        {
            //Don't move any closer
            sel.SetNavTarget(sel.transform.position);
        } else
        {
            sel.SetNavTarget(target.position);
        }
    }

}
