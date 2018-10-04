using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitBase : Selectable
{
    public float MovementSpeed;
    public UnitWeapon UnitWeapon;
    private Transform weaponTarget;

    // Use this for initialization
    public override void Start ()
    {
        base.Start();
	}
	
	// Update is called once per frame
	protected override void Update ()
    {
        base.Update();
	}

    protected override void OnDestroy()
    {
        base.OnDestroy();
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
