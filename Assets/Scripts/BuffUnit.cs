using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffUnit : UnitBase 
{
    public float SearchRadius = 15;

    // Use this for initialization
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
		
	}

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Selectable>().TeamIndex == this.TeamIndex)
        {
            other.gameObject.GetComponent<UnitCombat>().buffUnit();
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Selectable>().TeamIndex == this.TeamIndex)
        {
            other.gameObject.GetComponent<UnitCombat>().debuffUnit();
        }
    }
}
