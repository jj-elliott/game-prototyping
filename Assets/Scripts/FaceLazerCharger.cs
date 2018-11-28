using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

//[System.Serializable]
//public class OnPlayerCaptureProgressEvent : UnityEvent<float> { };

//[System.Serializable]
//public class OnEnemyCaptureProgressEvent : UnityEvent<float> { };
 
public class FaceLazerCharger : UnitProducer
{
    public OnPlayerCaptureProgressEvent OnPlayerCaptureProgress;
    public OnEnemyCaptureProgressEvent OnEnemyCaptureProgress;
    public Text captureText;
    public bool convertable;
    protected float playerCaptureProgress = 0.0f;
    protected float enemyCaptureProgress = 0.0f;
    protected bool lazerReady = false;
    public GameObject playerCaptureBar, enemyCaptureBar;
	// Use this for initialization
	public override void Start ()
    {
        base.Start();
        convertable = true;
        playerCaptureBar.SetActive(false);
        enemyCaptureBar.SetActive(false);
    }


    private void setLazerReady()
    {
        lazerReady = false;
        //need to update GUI to indicate lazer ready
    }

    // Update is called once per frame
    protected override void Update ()
    {
        //base.Update();
        if (TeamIndex >= 0)
        {
            productionText.text = "Charging " + unitPrefab.name;

            buildProgress += Time.deltaTime / buildTime;
            buildProgress = Mathf.Clamp01(buildProgress);

            if (OnBuildProgress != null)
            {
                OnBuildProgress.Invoke(buildProgress);
            }

            if (buildProgress >= 1.0f)
            {
                setLazerReady();
                buildProgress = 0.0f;
            }
        }
        else
        {
            productionText.text = unitPrefab.name;
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

        }
        else if (enemyCaptureProgress == 1.0f)
        {
            TeamIndex = 1;
            gameObject.GetComponent<Renderer>().material.color = SelectionManager.instance.teamColors[TeamIndex];
            captureText.text = "";
        }
        else
        {
            TeamIndex = -1;
            gameObject.GetComponent<Renderer>().material.color = Color.white;
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
}
