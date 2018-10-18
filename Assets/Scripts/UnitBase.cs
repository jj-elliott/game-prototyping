using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitBase : Selectable
{
    public float MovementSpeed;
    public UnitWeapon UnitWeapon;
    private Transform weaponTarget;
    public UnitProducer homeBase;

    // Use this for initialization
    public override void Start ()
    {
        base.Start();
	}
	
    void UpdateHomeBase()
    {

    }

	// Update is called once per frame
	protected override void Update ()
    {
        if (idle && this as UnitProducer == null)
        {
            if (homeBase == null)
            {
                UpdateHomeBase();
            }

            if(homeBase.standingOrder != null)
                SetOrder(homeBase.standingOrder);
        }

        base.Update();
	}

    protected override void OnDeath()
    {
        base.OnDeath();
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
