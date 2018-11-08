using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour {

    Camera cam;
	// Use this for initialization
	void Start () {
        cam = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
		if(cam != null)
        {
            var vec = (transform.position - cam.transform.position).normalized;
            transform.forward = (vec - Vector3.up * Vector3.Dot(vec , Vector3.up)).normalized;
        }
	}
}
