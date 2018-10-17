using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[System.Serializable]
public class OnBuildProgressEvent : UnityEvent<float> { };

public class UnitProducer : UnitBase
{
    [SerializeField]
    public GameObject unitPrefab;
    public float buildTime;
    public Transform spawnLocation;
    public OnBuildProgressEvent OnBuildProgress;
    public Text productionText;
    public float minRallyDistance = 5;
    public Order standingOrder;
    public int convertable = 0;
    protected float buildProgress;


    public override void Start()
    {
        isSelectable = true;
        base.Start();
        bool wasEnabled = meshAgent.enabled;
        meshAgent.enabled = true;
        this.meshAgent.destination = transform.position;
        meshAgent.enabled = wasEnabled;
    }

    protected override void Update()
    {
        base.Update();

        if (TeamIndex >= 0)
        {
            productionText.text = "Building: " + unitPrefab.name;

            buildProgress += Time.deltaTime / buildTime;
            buildProgress = Mathf.Clamp01(buildProgress);

            if (OnBuildProgress != null)
            {
                OnBuildProgress.Invoke(buildProgress);
            }

            if (buildProgress >= 1.0f)
            {
                SpawnUnit();
                buildProgress = 0.0f;
            }
        }
        else
        {
            productionText.text = "";
        }
    }

    public void UpdateBuildProgress(float addVal)
    {
        buildProgress += addVal;
    }

    protected virtual void SpawnUnit()
    {
        Selectable spawned = Instantiate(unitPrefab, spawnLocation.position, Quaternion.identity).GetComponent<Selectable>();
        spawned.Start();
        ((UnitBase)spawned).homeBase = this;
        if(standingOrder != null)
            spawned.SetOrder(standingOrder);

        //if(Vector3.Distance(this.meshAgent.destination, transform.position) > minRallyDistance)
        //    spawned.SetOrder(new MoveOrder(this.meshAgent.destination));
    }

    public override void SetOrder(Order order)
    {
        standingOrder = order;
        foreach(var unit in SelectionManager.instance.GetUnits(TeamIndex))
        {
            if(((UnitBase)unit).homeBase == this)
            {
                unit.SetOrder(order);
            }
        }
    }
}
