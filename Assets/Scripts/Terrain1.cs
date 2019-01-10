using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terrain1 : MonoBehaviour {

    public GameObject bystanders1;
    public GameObject bystanders2;
    public GameObject bystanders3;
    public GameObject bahGoat;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void retire(){
        Destroy(this.gameObject, 7.0f);
    }

    public void selectBystanders(int level){
        switch (level %3){
            case 0:
                bystanders1.SetActive(false);
                bystanders2.SetActive(true);
                bystanders3.SetActive(true);
                break;
            case 1:
                bystanders1.SetActive(true);
                bystanders1.SetActive(true);
                bystanders1.SetActive(false);
                break;
            case 2:
                bystanders1.SetActive(true);
                break;
            default:
                break;
        }
        if (level % 2 == 0){
            bahGoat.SetActive(true);
        } 
    }
}
