using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingUnitProducer : UnitProducer
{
	// Use this for initialization
	public override void Start ()
    {
        base.Start();
	}
	
	// Update is called once per frame
	protected override void Update ()
    {
        base.Update();

        if (TeamIndex == SelectionManager.instance.TeamIndex)
        {
            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                activeUnit = 0;
                buildProgress = 0.0f;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                activeUnit = 1;
                buildProgress = 0.0f;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                activeUnit = 2;
                buildProgress = 0.0f;
            }
        }
    }

    protected override void SpawnUnit()
    {
        base.SpawnUnit();

        if (TeamIndex != SelectionManager.instance.TeamIndex)
        {
            activeUnit = Random.Range(0, unitPrefab.Length);
        }
    }
}
