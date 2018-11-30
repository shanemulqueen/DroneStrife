using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class PlayerController : MonoBehaviour {

    public float speed;
    public float speedX;
    public float speedY;
    public float speedZ;
    public Text countText;
    public Text winText;
    //public GameObject gameController;
    public GameObject bombPrefab;
    public GameController gameControl;
    private Rigidbody rb;
    private Transform tf;
    private int count;
    private bool gameStarted = false;
    private bool gameOver;

    private GameObject bomb;



    private Vector3 objectPoolPosition = new Vector3(5, 15,-1);
    // Called on first frame the script is actice
    void Start () {
        rb = GetComponent<Rigidbody>();
        tf = GetComponent<Transform>();
        count = 0;
        //SetCountText(0);

	}

    // Update is called before rendering a frame. will contain most game codes
    private void Update()
    {
        //tf.position + new Vector3(0, 4, 0);
        //transform.Translate(Input.acceleration.x, 0, -Input.acceleration.z);
        if (Input.GetMouseButtonDown(0))
        {
            if (gameStarted == false) {
                gameStarted = true;
                rb.useGravity = true;
                winText.text = "";

            }
            else if(GameController.instance.getBombCount() < 3 & gameOver == false){
                spawnBomb();
                GameController.instance.addBomb();
            }

        }
    }


    //fixed update is called before doing any physics calculations
    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");



        //Vector3 movement = new Vector3(Input.acceleration.x, 0.0f, Input.acceleration.y);
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.AddForce(movement*speed);
        
    }

    void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.CompareTag("Next Terrain"))
        {
            GameController.instance.NextRound();
        }
        else if (other.gameObject.CompareTag("Gravity"))
        {
            rb.useGravity = false;
            //rb.velocity = Vector3.zero;
            rb.velocity = new Vector3(4*speedZ, 0.0f, 3*speedX);
            other.gameObject.SetActive(false);
        }
    }

    public void stop(){
        rb.velocity = Vector3.zero;
        gameOver = true;
    }

    void spawnBomb(){

        //  tf.position + new Vector3(0, 3f, 0f);
        GameObject bomb = Instantiate(bombPrefab, tf.position + new Vector3(-2.6f, -0.7f, -4.5f), Quaternion.identity);
        bomb.GetComponent<Rigidbody>().velocity = new Vector3(2f * speedZ, 0.0f, 1.5f * speedX);
        bomb.SetActive(true);
        //bombPrefab = (GameObject)Instantiate(bombPrefab, tf.position + new Vector3(0, -0.8f, -6.0f), Quaternion.identity);
    }


}
