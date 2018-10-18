using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class NavMeshBuilder : MonoBehaviour {

	// Use this for initialization
	void Start () {
        
        var islands = GameObject.FindGameObjectsWithTag("Terrain");

        foreach(var island in islands)
        {
            var links = island.transform.parent.GetComponentsInChildren<OffMeshLink>();
            
        }
        

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
