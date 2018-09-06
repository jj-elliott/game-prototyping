using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOrder : Order
{
    // TODO: This should make use of the navmesh system
    // For demenstration, we're going with a very simple example

    
    static float stopDistance = 0.1f;
    Vector3 targetLocation;

    public MoveOrder(Vector3 targetLocation)
    {
        this.targetLocation = targetLocation;
    }

    public override bool Complete(Selectable sel)
    {
        return (targetLocation - sel.transform.position).magnitude < stopDistance;
    }

    public override void Step(Selectable sel)
    {
        Vector3 vecToTarget = targetLocation - sel.transform.position;
        float distToTarget = vecToTarget.magnitude;
        vecToTarget = vecToTarget.normalized;

        sel.transform.position += vecToTarget * Mathf.Min(distToTarget, sel.movementSpeed);
    }
}
