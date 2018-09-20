using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleAI : MonoBehaviour {

    // Use this for initialization
    Selectable selectable;
	void Start () {
        selectable = GetComponent<Selectable>();
        if(selectable == null)
        {
            this.enabled = false;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (selectable.idle)
        {
            Transform closestEnemy = null;
            float closestDistance = float.MaxValue;
            foreach(var unit in SelectionManager.instance.GetUnits(0))
            {
                var distToEnemy = Vector3.Distance(unit.transform.position, transform.position);
                if(closestEnemy == null || distToEnemy < closestDistance)
                {
                    closestEnemy = unit.transform;
                    closestDistance = distToEnemy;
                }
            }
            if(closestEnemy != null)
            {
                selectable.SetOrder(new AttackOrder(closestEnemy));
            }
        }
	}
}
