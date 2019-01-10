using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {

    private string worked = "Method called";
    private bool bombed = false;

    private float closestImpact = 3.0f;
    private float scoreCalc = 5.0f;


    // Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UpdateClosest(float distance)
    {
        bombed = true;
        if (distance< 1.0f)
        {
            closestImpact = 0;
        }
        else if (distance< closestImpact)
        {
            closestImpact = distance-0.5f;
        }
    }

    public void SendWorked()
    {
        GameController.instance.gameOn(worked);
    }

    private void OnDestroy()
    {
        float score_float =(scoreCalc - closestImpact) / scoreCalc * 100f;
        GameController.instance.AddScore( score_float);
    }
}
