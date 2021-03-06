﻿using System.Collections;
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
    public bool isSelectable = false;
    public bool isSelected = false;
    public bool isAttacking
    {
        get
        {
            var order = orderQueue.Peek();
            if(order == null || order as AttackOrder == null)
            {
                return false;
            }
            return true;
        }
    }
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

    protected virtual void OnDeath()
    {
        SelectionManager.instance.UnRegisterUnit(this);
    }

    public void SetNavTarget(Vector3 targetPos)
    {
        if(meshAgent.enabled)
            meshAgent.SetDestination(targetPos);
    }

    public virtual void SetOrder(Order order)
    {
        orderQueue.Clear();
        orderQueue.Enqueue(order);
    }
    public void AddOrder(Order order)
    {
        orderQueue.Enqueue(order);
    }

    public void PreemptOrder(Order order)
    {
        var existingOrders = orderQueue.ToArray();
        orderQueue.Clear();
        orderQueue.Enqueue(order);

        foreach(var o in existingOrders)
        {
            orderQueue.Enqueue(order);
        }
    }

    public void SetTeam(int index)
    {
        TeamIndex = index;
        foreach(var rend in GetComponentsInChildren<Renderer>())
        {
            if (TeamIndex != -1)
                rend.material.color = SelectionManager.instance.teamColors[TeamIndex];
            else
                rend.material.color = Color.white;
        }
    }
}
