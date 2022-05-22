using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class policefollow : MonoBehaviour
{

    // Start is called before the first frame update
    public float speed = 4000;
    public Rigidbody policeRB;
    public GameObject player;
    private PlayerControl playerScript;
    private Animator policeAnm;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerScript = GameObject.Find("Player").GetComponent<PlayerControl>();
        policeRB = GetComponent<Rigidbody>();
        policeAnm = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        if (!playerScript.gameover && !playerScript.powerup)
        {
            policeRB.AddForce((new Vector3(player.transform.position.x, 10.67447f, player.transform.position.z) - transform.position) * speed);
            //policeRB.AddForce((player.transform.position - transform.position).normalized * speed);

            //to make police rotate with player
            // transform.rotation = player.transform.rotation;
        }
        // animation stop change from running static to idle
        if (playerScript.gameover)
        {
            policeAnm.SetFloat("Speed_f", 0);
        }
        AddSpeed(1);
    }
    //add difficulty to the game
    void AddSpeed(int score)
    {
        if (!playerScript.gameover)
        {
            for (int i = 0; i < score; i++)
            {
                //to increase police speed max to 7000
                if (!playerScript.powerup && speed != 7000)
                {
                    speed += 1;
                }
                //to increase player speed max to 12
                // if (playerScript.speed )
                // {
                playerScript.speed += 0.005f;
                //  }

            }
        }
    }
    //void AddSpeed(int num)
    //{
    //    for (int i = 0; i < num; i++)
    //    {
    //        if (playerScript.score == 500 || playerScript.score == 1000 || playerScript.score == 1800 && !playerScript.gameover)
    //        {
    //            speed += 600;
    //        }
    //    }

    //} void AddSpeed(int score)
    //{
    //    if (!playerScript.gameover && !playerScript.powerup)
    //    {
    //        for (int i = 0; i < score; i++)
    //        {
    //            if (speed !=6500)
    //            {
    //                speed += 2;
    //            }
    //            else speed =4500;
    //        }
    //    }
    //}
}

