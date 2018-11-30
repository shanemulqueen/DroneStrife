using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class bomb : MonoBehaviour {



    private Rigidbody rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            //count++;
            //gameControl.FirstLevelActions();

            //GameController.instance.FirstLevelActions();
            //etCountText(gameControl.getScore());

        }
        //this.gameObject.SetActive(false);
    }
}
