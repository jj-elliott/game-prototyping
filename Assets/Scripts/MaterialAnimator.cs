using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class MaterialAnimator : MonoBehaviour {

    public float tileMin, tileMax, tileDrift, offsetDrift;
    Renderer rend;
    public bool x = false;
    // Use this for initialization
	void Start () {
        rend = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
        var mat = rend.material;
        float tileVal = x ? mat.mainTextureScale.x : mat.mainTextureScale.y;

        tileVal += tileDrift;
        if (tileVal < tileMin)
        {
            tileVal = tileMin;
            tileDrift *= -1;
        } else if(tileVal > tileMax)
        {
            tileVal = tileMax;
            tileDrift *= -1;
        }
        if (x)
        {
            mat.mainTextureScale = new Vector2(tileVal , mat.mainTextureScale.y);

        }
        else
        {
            mat.mainTextureScale = new Vector2(mat.mainTextureScale.x, tileVal);

        }

    }
}
