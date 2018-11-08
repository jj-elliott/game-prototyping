using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIStrategy : MonoBehaviour {

    const int AI_INDEX = 1;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        List<UnitProducer> ownedFactories = new List<UnitProducer>();
        List<UnitProducer> neutralFactories = new List<UnitProducer>();
        List<UnitProducer> enemyFactories = new List<UnitProducer>();

        
        foreach(var factory in UnitProducer.FactoryList)
        {
            if(factory.TeamIndex == AI_INDEX)
            {
                ownedFactories.Add(factory);
            } else if (factory.TeamIndex == 0)
            {
                enemyFactories.Add(factory);
            } else
            {
                neutralFactories.Add(factory);
            }
        }


        foreach (var factory in ownedFactories)
        {
            if(factory.standingOrder != null && factory.standingOrder as DonateOrder != null)
            {
                var donation = factory.standingOrder as DonateOrder;
                if(donation.prod.TeamIndex == 1)
                {
                    factory.standingOrder = null;
                }
            }

            if(factory.standingOrder != null)
            {
                continue;
            }
            if(neutralFactories.Count > 0)
            {
                UnitProducer closest = null;
                float closeDist = float.MaxValue;
                foreach(var other in neutralFactories)
                {
                    var dist = Vector3.Distance(factory.transform.position, other.transform.position);
                    if(closest == null || dist < closeDist)
                    {
                        closest = other;
                        closeDist = dist;
                    }
                }
                factory.SetOrder(new DonateOrder(closest));

            }
            else
            {
                UnitProducer closest = null;
                float closeDist = float.MaxValue;
                foreach (var other in enemyFactories)
                {
                    var dist = Vector3.Distance(factory.transform.position, other.transform.position);
                    if (closest == null || dist < closeDist)
                    {
                        closest = other;
                        closeDist = dist;

                    }
                }

                if (closest.convertable)
                {
                    factory.SetOrder(new DonateOrder(closest));
                } else
                {
                    factory.SetOrder(new AttackOrder(closest.transform));
                }
            }
        }
    }
}
