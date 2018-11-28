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
    UnitBase activeUnit;
    protected float playerCaptureProgress = 0.0f;
    protected float enemyCaptureProgress = 0.0f;
    public GameObject playerCaptureBar, enemyCaptureBar;
	// Use this for initialization
	public override void Start ()
    {
        base.Start();
        convertable = true;
        playerCaptureBar.SetActive(false);
        enemyCaptureBar.SetActive(false);
    }
	
	// Update is called once per frame
	protected override void Update ()
    {
        base.Update();
        if(activeUnit == null)
        {
            canBuild = true;
        }

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
            gameObject.GetComponent<Renderer>().material.color = SelectionManager.instance.teamColors[TeamIndex];
            captureText.text = "";
            if (activeUnit)
            {
                activeUnit.enabled = true;
                activeUnit.SetTeam(0);
            }
        }
        else if (enemyCaptureProgress == 1.0f)
        {
            TeamIndex = 1;
            gameObject.GetComponent<Renderer>().material.color = SelectionManager.instance.teamColors[TeamIndex];
            captureText.text = "";
            if (activeUnit)
            {
                activeUnit.enabled = true;
                activeUnit.SetTeam(1);
            }
        }
        else if (enemyCaptureProgress > 0 && TeamIndex == 0)
        {
            TeamIndex = -1;
            gameObject.GetComponent<Renderer>().material.color = Color.white;
            if (activeUnit)
            {
                activeUnit.enabled = false;
                activeUnit.SetTeam(-1);
            }
        }
        else if(playerCaptureProgress > 0 && TeamIndex == 1)
        {
            TeamIndex = -1;
            gameObject.GetComponent<Renderer>().material.color = Color.white;
            if (activeUnit)
            {
                activeUnit.enabled = false;
                activeUnit.SetTeam(-1);
            }
        }
    }

    public void UpdatePlayerCaptureProgress(float captureValue)
    {
        if (enemyCaptureProgress > 0.0f)
        {
            enemyCaptureProgress -= captureValue;
            enemyCaptureProgress = Mathf.Clamp01(enemyCaptureProgress);
            captureText.text = "Player is capturing factory";
            playerCaptureBar.SetActive(false);
            enemyCaptureBar.SetActive(true);

        }
        else if (playerCaptureProgress < 1.0f)
        {
            playerCaptureProgress += captureValue;
            playerCaptureProgress = Mathf.Clamp01(playerCaptureProgress);
            captureText.text = "Player is capturing factory";
            playerCaptureBar.SetActive(true);
            enemyCaptureBar.SetActive(false);
        }
    }

    public void UpdateEnemyCaptureProgress(float captureValue)
    {
        if (playerCaptureProgress > 0.0f)
        {
            playerCaptureProgress -= captureValue;
            playerCaptureProgress = Mathf.Clamp01(playerCaptureProgress);
            captureText.text = "Enemy is capturing factory";
            playerCaptureBar.SetActive(true);
            enemyCaptureBar.SetActive(false);
        }
        else if (enemyCaptureProgress < 1.0f)
        {
            enemyCaptureProgress += captureValue;
            enemyCaptureProgress = Mathf.Clamp01(enemyCaptureProgress);
            captureText.text = "Enemy is capturing factory";
            playerCaptureBar.SetActive(false);
            enemyCaptureBar.SetActive(true);
        }
    }

    protected override void SpawnUnit()
    {

        Selectable spawned = Instantiate(unitPrefab, spawnLocation.position, Quaternion.identity).GetComponent<Selectable>();
        spawned.SetTeam(TeamIndex);
        spawned.Start();
        ((UnitBase)spawned).homeBase = this;
        if (standingOrder != null)
            spawned.SetOrder(standingOrder);
        canBuild = false;
    }
}
