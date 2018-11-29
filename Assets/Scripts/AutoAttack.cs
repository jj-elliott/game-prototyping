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
        UnitCombat combat = other.GetComponent<UnitCombat>();

        if(combat != null && combat.TeamIndex != unit.TeamIndex)
        {
            if (!unit.isAttacking)
            {
                ArtilleryUnit au = unit as ArtilleryUnit;
                ShieldUnit su = combat.gameObject.GetComponent<ShieldUnit>();
                if(au != null && su != null)
                {
                    return;
                }
                unit.PreemptOrder(new AttackOrder(other.transform));
            }
        }
    }
}
