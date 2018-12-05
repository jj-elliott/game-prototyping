using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour {

    public float time;
	// Use this for initialization

    IEnumerator Die()
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
