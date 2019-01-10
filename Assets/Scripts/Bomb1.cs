using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Bomb1 : MonoBehaviour {



    public GameObject Explosion;
    private float radius = 5.0F;
    private float power = 100.0F;

    private Target target;
    private PlayerController player;
    private Transform tf;
    private Collider coll;
    // Use this for initialization
    void Start () {
        coll = GetComponent<Collider>();
        tf = GetComponent<Transform>();
        //coll.isTrigger = true;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        GameObject explosion = Instantiate(Explosion, tf.position, Quaternion.identity);
        Destroy(this.gameObject, 0.3f);
        Destroy(explosion, 2.0f);

    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject explosion = Instantiate(Explosion, tf.position, Quaternion.identity);
        MakeExplosion(collision.contacts[0].point);
        Destroy(this.gameObject, 0.05f);
        Destroy(explosion, 2.0f);

    }

    void MakeExplosion(Vector3 explosionPoint)
    {
        Collider[] colliders = Physics.OverlapSphere(explosionPoint, radius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
                rb.AddExplosionForce(power, explosionPoint, radius, 3.0F);
            GameObject item = hit.gameObject;
            if (item != null & item.CompareTag("Pick Up"))
            {
                Vector3 score_point = explosionPoint - rb.position;
                score_point.y = 0;
                float distance  = 50.0f;
                target = item.GetComponent<Target>();
                target.UpdateClosest(score_point.magnitude);
                //GameController.instance.BombHit(score_point.magnitude.ToString());
                Destroy(item, 1.4f);

            }
            else if (item != null & item.CompareTag("Player"))
            {
                GameController.instance.gameStop("You blew yourself up idiot!");

            }
        }
    }

    private void OnDestroy()
    {
        GameController.instance.removeBomb();
    }
}
