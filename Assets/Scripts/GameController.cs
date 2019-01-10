using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {


    public static GameController instance;         //A reference to our game control script so we can access it statically.
    public Text scoreText;                      //A reference to the UI text component that displays the player's score.
    public Text gameOvertext;             //A reference to the object that displays the text which appears when the player dies.
    public GameObject hotDesert;                //A reference to some of the hottest dunes out there man.
    public GameObject coolSea;              // These waves sure play it cool- can you?
    public GameObject activeRound;
    public GameObject nextRound;
    public GameObject player;
    public Text highScoreText;


   
    public bool gameOver = false;               //Is the game over?
    public float scrollSpeed = -1.5f;


    private int score = 0;                      //The player's score.
    private int highScore;
    private float score_add = 0f;
    private float accuracy = 0.7f;
    private int bombCount = 0;
    private int terrainCount = 1;
    private int roundCount = 1;
    private Terrain1 activeTerrain;
    private Terrain1 nextTerrain;
    private PlayerController playerController;
    private Vector3 round1Spawn = new Vector3(76.2f, 0, 100);
    private Vector3 round2Spawn = new Vector3(304.8f, 0, 400);
    //private Vector3 round2Spawn = new Vector3(152.4f, 0, 200);

    private int round1End = 4;

    private int lastSum = 0;
    private int last2 = 300;
    private int last1 = 300;
    private int cRound = 100;

    void Awake()
    {
        //If we don't currently have a game control...
        if (instance == null)
            //...set this one to be it...
            instance = this;
        //...otherwise...
        else if (instance != this)
            //...destroy this one because it is a duplicate.
            Destroy(gameObject);
    }


    // Use this for initialization
    void Start () {
        activeTerrain = activeRound.GetComponent<Terrain1>();
        nextTerrain = nextRound.GetComponent<Terrain1>();
        playerController = player.GetComponent<PlayerController>();
        //PlayerPrefs.SetInt("highScore", 0);
        highScore = PlayerPrefs.GetInt("highScore");
        gameOvertext.text = "Tap to Drop Bombs" + System.Environment.NewLine + "High Score: " + highScore.ToString();

    }
	
	// Update is called once per frame
	void Update () {
        if (gameOver && (Input.GetMouseButtonDown(0)))
        {
            //...reload the current scene.
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void AddScore(float score_add)
    {
        //score_add = (5.0f - distance) / 5.0f * 100.0f;
        score += (int)score_add;
        cRound += (int)score_add;
        scoreText.text = "Score: "+ score.ToString();
    }

    public void gameOn(string input_text)
    {
        gameOvertext.text = input_text;
    }

    public void gameStop(string reason){
        playerController.stop();
        gameOver = true;
        gameOvertext.text = reason;

        if(score > highScore){
            PlayerPrefs.SetInt("highScore", score);
            highScore = PlayerPrefs.GetInt("highScore");
            highScoreText.text = "New High Score!";
            

        }
    }



    public void NextRound(){
        lastSum = last2 + last1 + cRound;
        last2 = last1;
        last1 = cRound;
        cRound = 0;
        //if (terrainCount %3 ==0 & score< terrainCount*300f*accuracy){
        if (lastSum<= 75){
        
            gameStop("You suck!" + System.Environment.NewLine + "Tap to Retry");
        }
        else {
            terrainCount++;
            if (terrainCount<round1End){
                Vector3 nextSpawn = terrainCount * round1Spawn;
                activeTerrain.retire();
                activeTerrain = nextTerrain;
                nextRound = Instantiate(hotDesert, nextSpawn, Quaternion.identity);
                nextTerrain = nextRound.GetComponent<Terrain1>();
                nextTerrain.selectBystanders(terrainCount);
            }
            else if (terrainCount >=round1End){
                roundCount += 1;
                playerController.enableSteering();
                Vector3 nextSpawn = round2Spawn * (terrainCount-round1End+1);
                nextRound = Instantiate(coolSea, nextSpawn, Quaternion.identity);
                activeTerrain.retire();
                activeTerrain = nextTerrain;
            }
            else {

            }
        }

    }

    public int getScore()
    {
        return score;
    }

    public void addBomb(){
        bombCount += 1;
    }

    public void removeBomb(){
        bombCount -= 1;
    }

    public int getBombCount(){
        return bombCount;
    }
    public int getRound(){
        return roundCount;
    }
}
