using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[System.Serializable]
public class OnPlayerCaptureProgressEvent : UnityEvent<float> { };

[System.Serializable]
public class OnEnemyCaptureProgressEvent : UnityEvent<float> { };

public class CaptureableUnitProducer : UnitProducer
{
    public OnPlayerCaptureProgressEvent OnPlayerCaptureProgress;
    public OnEnemyCaptureProgressEvent OnEnemyCaptureProgress;
    public Text captureText;
    [SerializeField]
    public Material playerUnitMaterial;
    [SerializeField]
    public Material enemyUnitMaterial;
    [SerializeField]
    public Material neutralUnitMaterial;
    protected float playerCaptureProgress = 0.0f;
    protected float enemyCaptureProgress = 0.0f;
    [SerializeField]
    public GameObject enemyPrefab;

	// Use this for initialization
	public override void Start ()
    {
        base.Start();
        convertable = true;   
	}
	
	// Update is called once per frame
	protected override void Update ()
    {
        base.Update();

        if (OnPlayerCaptureProgress != null)
        {
            OnPlayerCaptureProgress.Invoke(playerCaptureProgress);
        }

        if (OnEnemyCaptureProgress != null)
        {
            OnEnemyCaptureProgress.Invoke(enemyCaptureProgress);
        }

        if (playerCaptureProgress == 1.0f)
        {
            TeamIndex = 0;
            gameObject.GetComponent<Renderer>().material = playerUnitMaterial;
            captureText.text = "";

        }
        else if (enemyCaptureProgress == 1.0f)
        {
            TeamIndex = 1;
            gameObject.GetComponent<Renderer>().material = enemyUnitMaterial;
            captureText.text = "";
        }
        else
        {
            TeamIndex = -1;
            gameObject.GetComponent<Renderer>().material = neutralUnitMaterial;
        }
    }

    protected override void SpawnUnit()
    {
        if (TeamIndex == 0)
        {
            Selectable spawned = Instantiate(unitPrefab, spawnLocation.position, Quaternion.identity).GetComponent<Selectable>();
            spawned.Start();
            ((UnitBase)spawned).homeBase = this;
            if (standingOrder != null)
                spawned.SetOrder(standingOrder);

        }
        else if (TeamIndex == 1)
        {
            Selectable spawned = Instantiate(enemyPrefab, spawnLocation.position, Quaternion.identity).GetComponent<Selectable>();
            spawned.Start();
            ((UnitBase)spawned).homeBase = this;
            if (standingOrder != null)
                spawned.SetOrder(standingOrder);

        }
    }

    public void UpdatePlayerCaptureProgress(float captureValue)
    {
        if (enemyCaptureProgress > 0.0f)
        {
            enemyCaptureProgress -= captureValue;
            enemyCaptureProgress = Mathf.Clamp01(enemyCaptureProgress);
            captureText.text = "Player is capturing factory";
        }
        else if (playerCaptureProgress < 1.0f)
        {
            playerCaptureProgress += captureValue;
            playerCaptureProgress = Mathf.Clamp01(playerCaptureProgress);
            captureText.text = "Player is capturing factory";
        }
    }

    public void UpdateEnemyCaptureProgress(float captureValue)
    {
        if (playerCaptureProgress > 0.0f)
        {
            playerCaptureProgress -= captureValue;
            playerCaptureProgress = Mathf.Clamp01(playerCaptureProgress);
            captureText.text = "Enemy is capturing factory";
        }
        else if (enemyCaptureProgress < 1.0f)
        {
            enemyCaptureProgress += captureValue;
            enemyCaptureProgress = Mathf.Clamp01(enemyCaptureProgress);
            captureText.text = "Enemy is capturing factory";
        }
    }
}
