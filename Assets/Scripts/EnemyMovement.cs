using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject[] saw;
    Rigidbody2D[] sawBody;
    float sawAngular;
    string newDirection1;
    [System.Serializable]

    public struct MaceController{
        public GameObject mace;
        public Rigidbody2D maceBody;
        public float horizontalVel;
        public float verticalVel;
        public string maceDirection;
        public Vector2 maceStartPos;
        public Vector2 maceFinalOffset;
    };

    public MaceController[] maceCtrls;
    void Start()
    {
        for(int j=0;j<maceCtrls.Length;j++){
            maceCtrls[j].maceBody = maceCtrls[j].mace.GetComponent<Rigidbody2D>();
        }
        newDirection1 = "left";
        sawAngular = 250f;
        saw = GameObject.FindGameObjectsWithTag("saw");
        sawBody = new Rigidbody2D[saw.Length];
        for(int i=0;i<saw.Length;i++){
            sawBody[i] = saw[i].GetComponent<Rigidbody2D>();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        for(int j=0;j<maceCtrls.Length;j++){
            if(maceCtrls[j].mace.transform.position.x<=maceCtrls[j].maceStartPos.x-maceCtrls[j].maceFinalOffset.x){
                maceCtrls[j].maceDirection = "right";
            }
            if(maceCtrls[j].mace.transform.position.x>=maceCtrls[j].maceStartPos.x+maceCtrls[j].maceFinalOffset.x){
                maceCtrls[j].maceDirection = "left";
                Debug.Log("Move left");
            }
            if(maceCtrls[j].maceDirection=="left"){
                maceCtrls[j].maceBody.velocity = new Vector2(-1*maceCtrls[j].horizontalVel*Time.deltaTime,maceCtrls[j].maceBody.velocity.y*Time.deltaTime);
            }
            else if(maceCtrls[j].maceDirection=="right"){
                maceCtrls[j].maceBody.velocity = new Vector2(maceCtrls[j].horizontalVel*Time.deltaTime,maceCtrls[j].maceBody.velocity.y*Time.deltaTime);
            }
        }
        // if(newDirection1=="left"){
        //     sawBody[0].angularVelocity = sawAngular;
        // }
        // else if(newDirection1=="right"){
        //     sawBody[0].angularVelocity = -1*sawAngular;
        // }
    }

    void OnCollisionEnter2D(Collision2D collision){
        if(this.gameObject.name=="saw1" && collision.gameObject.tag=="box"){
            string boxType = collision.gameObject.name.Substring(0,9);
            if(boxType=="leftCrate"){
                newDirection1 = "right";
            }
            else if(boxType=="rghtCrate"){
                newDirection1 = "left";
            }
        }
    }
    void OnCollisionStay2D(Collision2D collision){
        if(this.gameObject.tag=="mace" && collision.gameObject.tag=="ground"){
            if(collision.gameObject.name.Substring(0,8)=="platform"){
            this.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0,5000f),ForceMode2D.Impulse);
            }
        }
    }
    
}
