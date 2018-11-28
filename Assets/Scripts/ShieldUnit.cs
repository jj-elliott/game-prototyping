using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldUnit : UnitBase 
{
    public float MaxRadius;
    public float MinRadius;
    public Transform shieldTrans;

    private float _Radius;
    public float Radius { get { return _Radius; } }

    // Use this for initialization
    public override void Start()
    {
        base.Start();
        _Radius = MaxRadius;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
	}

    public void OnHealthChanged(float healthPercentage)
    {
        _Radius = Mathf.Lerp(MinRadius, MaxRadius, healthPercentage);
    }
}
