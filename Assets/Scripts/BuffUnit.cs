using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffUnit : UnitBase 
{
    public float SearchRadius = 15;
    private List<GameObject> buffedUnits;
    // Use this for initialization
    public override void Start()
    {
        base.Start();
        buffedUnits = new List<GameObject>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
		
	}

    public void OnTriggerEnter(Collider other)
    {
        var sel = other.gameObject.GetComponent<Selectable>();
        if (sel != null && sel.TeamIndex == this.TeamIndex)
        {
            var combat = other.gameObject.GetComponent<UnitCombat>();
            if(combat != null)
            {
                buffedUnits.Add(combat.gameObject);
            combat.buffUnit();
            }
        }
    }
    public void OnTriggerExit(Collider other)
    {
        var sel = other.gameObject.GetComponent<Selectable>();
        if (sel != null && sel.TeamIndex == this.TeamIndex)
        {
            var combat = other.gameObject.GetComponent<UnitCombat>();
            if (combat != null)
            {
                buffedUnits.Remove(combat.gameObject);
                combat.debuffUnit();
            }
        }
    }

    private void OnDestroy()
    {
        foreach(var unit in buffedUnits)
        {
            if(unit)
            unit.GetComponent<UnitCombat>().debuffUnit();
        }
    }
}
