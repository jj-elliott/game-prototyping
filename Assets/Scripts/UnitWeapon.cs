using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitWeapon : MonoBehaviour
{
    [SerializeField]
    protected float MaxRange;

    [SerializeField]
    protected float MinRange;

    [SerializeField]
    protected float Damage;

    [SerializeField]
    protected UnitBase unit;

	// Use this for initialization
	protected virtual void Start()
    {
        if (unit == null)
        {
            unit = GetComponentInParent<UnitBase>();
        }
	}
	
	// Update is called once per frame
	protected virtual void Update()
    {
		if (unit != null)
        {
            Transform t = unit.GetWeaponTarget();
            if (t != null && CanFire(t.position))
            {
                Fire(t);
            }
        }
	}

    protected virtual bool CanFire(Vector3 targetPos)
    {
        if (unit != null)
        {
            float distToTarget = (targetPos - unit.transform.position).magnitude;
            return distToTarget > MinRange && distToTarget < MaxRange;
        }

        return false;
    }

    protected abstract void Fire(Transform target);
}
