using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class UnitBase : Selectable
{
    public UnitWeapon unitWeapon;
    private Transform weaponTarget;
	// Use this for initialization
	protected override void Start ()
    {
        base.Start();
	}
	
	// Update is called once per frame
	protected override void Update ()
    {
        base.Update();
	}

    public virtual Transform GetWeaponTarget()
    {
        return weaponTarget;
    }

    public void SetWeaponTarget(Transform target)
    {
        weaponTarget = target;
    }
}
