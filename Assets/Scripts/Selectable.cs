using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

public class Selectable : MonoBehaviour {

    public float movementSpeed = 0.1f;
    Queue<Order> orderQueue;
    public UnityEvent OnSelected, OnDeselected;
    public NavMeshAgent meshAgent;
    public bool idle { get { return orderQueue.Count == 0; } }
    public int TeamIndex;

    // Use this for initialization
    public virtual void Start () {
        if (orderQueue == null)
        {
            orderQueue = new Queue<Order>();
        }
        meshAgent = GetComponent<NavMeshAgent>();
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

    public void SetNavTarget(Vector3 targetPos)
    {
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
