using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Selectable : MonoBehaviour {

    public float movementSpeed = 0.1f;
    Queue<Order> orderQueue;
    public UnityEvent OnSelected, OnDeselected;

	// Use this for initialization
	void Start () {
        orderQueue = new Queue<Order>();
	}
	
	// Update is called once per frame
	void Update () {
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
