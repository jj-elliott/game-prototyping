using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class UnitBase : Selectable
{
    public float MovementSpeed;
    public UnitWeapon UnitWeapon;
    private Transform weaponTarget;

    public static List<UnitBase> Units { get { if (_units != null) return _units; else { _units = new List<UnitBase>(); return _units; } } }
    static List<UnitBase> _units;
    // Use this for initialization
    public override void Start ()
    {
        base.Start();

        if (Units == null)
        {
            _units = new List<UnitBase>();
        }

        Units.Add(this);
	}
	
	// Update is called once per frame
	protected override void Update ()
    {
        base.Update();
	}

    protected override void OnDestroy()
    {
        base.OnDestroy();
        Units.Remove(this);
        SelectionManager.instance.UnRegisterUnit(this); 
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
