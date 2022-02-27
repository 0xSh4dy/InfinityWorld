using System;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    Rigidbody2D player;
    string[] collisionPlatforms = {"stationaryPlatform","translatingPlatform","rotatingPlatform","groujnd","rock"};
    Camera mainCam;
    float motionDetector,vmax,playerSpeed,platformRotation,defaultJump;
    
    GameObject[] rotatingPlatforms,translatingPlatforms;
    int coinCount;
    AudioManager audioManager;    
    // Start is called before the first frame update
  
    void Start()
    {
        coinCount= 0;
        audioManager =  FindObjectOfType<AudioManager>();
        audioManager.PlaySound("MainAudio");
        player = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();
        motionDetector = player.position.x;
        vmax = 7f;
        playerSpeed = 10f;
        defaultJump = 200f;
        platformRotation = -70f;
        mainCam = Camera.main;
        mainCam.transform.position = new Vector3(player.position.x,mainCam.transform.position.y,-10);
        rotatingPlatforms = GameObject.FindGameObjectsWithTag("rotatingPlatform");
        translatingPlatforms = GameObject.FindGameObjectsWithTag("translatingPlatform");
    }

    void rotatePlatforms(GameObject[] rotatingPlatforms){
        foreach(GameObject platform in rotatingPlatforms){
            platform.GetComponent<Rigidbody2D>().angularVelocity = platformRotation;
        }
    }
    void translatePlatforms(GameObject[] translatingPlatforms){
        // Needs modification

        // for(int i=1;i<=translatingPlatforms.Length;i++){
        //     if(i==1){
        //         translatingPlatforms[0].transform.position = Vector2.Lerp(new Vector2(143.07f,0.78f),new Vector2(186.38f,0.78f),Mathf.PingPong(0.09f*Time.time,1));
        //     }
        //     else if(i==2){          
        //         translatingPlatforms[1].transform.position = Vector2.Lerp(new Vector2(186.38f,4.6f),new Vector2(143.07f,4.6f),Mathf.PingPong(0.09f*Time.time,1));
        //     }
        // }
    }
    void GameOver(){
        audioManager.PauseSound("MainAudio");
        audioManager.PlaySound("GameOver");
    }
    // // Update is called once per frame
    void Update()
    {

        rotatePlatforms(rotatingPlatforms);
        translatePlatforms(translatingPlatforms);
        float movePlayer = Input.GetAxis("Horizontal")*playerSpeed*Time.deltaTime*20;
        if(player.position.x>motionDetector){
            if(movePlayer<0){
                player.AddForce(new Vector2(-10f,0),ForceMode2D.Force);
            }
        }
        if(player.position.x<motionDetector){
            if(movePlayer>0){
                player.AddForce(new Vector2(10f,0),ForceMode2D.Force);
            }
        }
        if(player.velocity.magnitude<vmax){
            player.AddForce(new Vector2(movePlayer,0),ForceMode2D.Force);
        }
        
        motionDetector = player.position.x;
        mainCam.transform.position = new Vector3(player.position.x,mainCam.transform.position.y,-10);
    }
    
    void OnCollisionStay2D(Collision2D collision){
        if(this.gameObject.tag=="Player"){
        
        if(collision.gameObject.tag == "ground" || collision.gameObject.tag=="rock" || collision.gameObject.tag == "rotatingPlatform" || collision.gameObject.tag=="translatingPlatform" || collision.gameObject.tag=="stationaryPlatform" ){
            if(Input.GetKey(KeyCode.Space)){
            player.AddForce(new Vector2(0,defaultJump),ForceMode2D.Force);
            }
        }
        }
        if(collision.gameObject.tag=="saw" || collision.gameObject.tag=="enemy"){
            GameOver();
        }
        
    }
 
    void OnTriggerEnter2D(Collider2D collider){
        if(collider.name=="FallDetector"){
            GameOver();
        }
        if(collider.name.Substring(0,4)=="Coin"){
            coinCount++;
            Destroy(collider.gameObject);
        }
    }
}