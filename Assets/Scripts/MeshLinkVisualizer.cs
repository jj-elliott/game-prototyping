using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[ExecuteInEditMode]
public class MeshLinkVisualizer : MonoBehaviour {

    private void OnDrawGizmos()
    {
        var Link = gameObject.GetComponent<OffMeshLink>();

        if(Link != null)
        {
            Gizmos.color = Color.cyan;
            
            Gizmos.DrawSphere(transform.position , .05f);
            if(Link.startTransform != null && Link.endTransform != null)
            {
                Gizmos.color = Color.magenta;
                Gizmos.DrawLine(Link.startTransform.position, Link.endTransform.position);
            }
        }
    }
}
