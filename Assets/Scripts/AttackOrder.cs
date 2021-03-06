﻿using System.Collections;
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
        location = targetCombat.transform.position;
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
        location = targetCombat.transform.position;
        unit.SetWeaponTarget(target);
        float distToTarget = (target.position - sel.transform.position).magnitude;

        ProjectileWeapon pw = weap as ProjectileWeapon;
        bool canSee = true;

        if(pw != null)
        {
            canSee = pw.CanSee(target.position);
        }

        if (distToTarget < weap.MaxRange && canSee)
        {
            //Don't move any closer
            sel.SetNavTarget(sel.transform.position);
        } else
        {
            sel.SetNavTarget(target.position);
        }
    }

}
