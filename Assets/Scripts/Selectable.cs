using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

public class Selectable : MonoBehaviour {

    public int TeamIndex;
    Queue<Order> orderQueue;
    public UnityEvent OnSelected, OnDeselected;
    public NavMeshAgent meshAgent;
    public bool idle { get { return orderQueue.Count == 0; } }

    // Use this for initialization
    public virtual void Start () {
        if (orderQueue == null)
        {
            orderQueue = new Queue<Order>();
        }
        meshAgent = GetComponent<NavMeshAgent>();
        SelectionManager.instance.RegisterUnit(this);
	}

    // Update is called once per frame
    protected virtual void Update () {
		if(orderQueue.Count > 0)
        {
            Order currentOrder = orderQueue.Peek();
            if (currentOrder.Complete(this))
            {
                orderQueue.Dequeue();
            } else
            {
                currentOrder.Step(this);
            }
        }
	}

    protected virtual void OnDestroy()
    {
        SelectionManager.instance.UnRegisterUnit(this);
    }

    public void SetNavTarget(Vector3 targetPos)
    {
        if(meshAgent.enabled)
            meshAgent.SetDestination(targetPos);
    }

    public void SetOrder(Order order)
    {
        orderQueue.Clear();
        orderQueue.Enqueue(order);
    }
    public void AddOrder(Order order)
    {
        orderQueue.Enqueue(order);
    }
}
