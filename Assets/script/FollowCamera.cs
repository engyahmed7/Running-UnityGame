using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{

    public GameObject player;
    private Vector3 offset;
    public AudioSource Sound;
    public PlayerControl playerSound;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - player.transform.position;
        Sound = GetComponent<AudioSource>();
        playerSound = GameObject.Find("Player").GetComponent<PlayerControl>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + offset;
        //to make background sound stop
        if (playerSound.gameover)
        {
            Sound.Stop();
        }
    }

}

