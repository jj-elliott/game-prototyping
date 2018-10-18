using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class AutoAttack : MonoBehaviour {

    UnitBase unit;
	// Use this for initialization
	void Start () {
        unit = GetComponent<UnitBase>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        UnitBase combat = other.GetComponent<UnitBase>();

        if(combat != null && combat.TeamIndex != unit.TeamIndex)
        {
            unit.PreemptOrder(new AttackOrder(other.transform));
        }
    }
}
