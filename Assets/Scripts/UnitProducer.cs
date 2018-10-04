using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[System.Serializable]
public class OnBuildProgressEvent : UnityEvent<float> { };
public class UnitProducer : UnitBase {

    [SerializeField]
    public float buildTime;
    private float buildProgress;
    public Transform spawnLocation;
    [SerializeField]
    public GameObject unitPrefab;
    public OnBuildProgressEvent OnBuildProgress;
    float minRallyDistance = 5;
    public Text productionText;
    public Order standingOrder;


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

        float buildBonus = 1.0f;
        foreach (ControlTower tower in ControlTower.Towers)
        {
            if (tower.TeamIndex == TeamIndex)
            {
                buildBonus *= tower.BuildBonus;
            }
        }

        buildProgress += Time.deltaTime / buildTime * buildBonus;
        buildProgress = Mathf.Clamp01(buildProgress);
        productionText.text = "Building: " + unitPrefab.name;
        if (OnBuildProgress != null)
        {
            OnBuildProgress.Invoke(buildProgress);
        }

        if(buildProgress == 1)
        {
            buildProgress = 0;
            SpawnUnit();
        }

        if(TeamIndex == SelectionManager.instance.TeamIndex)
        {
            //if (Input.GetKeyDown(KeyCode.Alpha0))
            //{
            //    activeUnit = 0;
            //    buildProgress = 0;
            //}
            //else if (Input.GetKeyDown(KeyCode.Alpha1))
            //{
            //    activeUnit = 1;
            //    buildProgress = 0;
            //}
            //else if (Input.GetKeyDown(KeyCode.Alpha2))
            //{
            //    activeUnit = 2;
            //    buildProgress = 0;
            //}
        }



    }
    public void UpdateProgress(float addVal)
    {
        buildProgress += addVal;
    }
    void SpawnUnit()
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
