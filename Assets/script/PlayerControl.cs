using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerControl : MonoBehaviour
{
    public float speed = 5.0f;
    public float turnSpeed = 50;
    public float horizontalInput;
    public float verticalInput;
    public float grav = 1;

    private Rigidbody playerrg;

    public bool onGround = true;
    public bool gameover = false;
    public bool powerup = false;
    public bool power = false;

    public GameObject[] prefab;
    public GameObject prefabMoney;
    public GameObject[] prefabPowerUp;
    public GameObject PowerIndicator;

    private Animator playerAnm;

    private AudioSource playersound;
    public AudioClip jumpclip;
    public AudioClip crashclip;
    public AudioClip powerupclip;
    public AudioClip Moneyclip;

    public ParticleSystem ExplosionPlayer;
    public ParticleSystem fireworks;

    public TextMeshProUGUI scoreText;
    public int score = 0;
    public TextMeshProUGUI moneyText;
    public int money = 0;
    public TextMeshProUGUI gameoverText;
    public Button restartButton;


    // Start is called before the first frame update
    void Start()
    {
        playerAnm = GetComponent<Animator>();
        playerrg = GetComponent<Rigidbody>();
        Physics.gravity *= grav;
        InvokeRepeating("AddPrefab", 2, 2);
        InvokeRepeating("AddPrefabMoney", 1, 1);
        InvokeRepeating("AddPrefabPowerUp", 1, 15);
        playersound = GetComponent<AudioSource>();
        updateScore(0);
        updateMoney(0);
    }
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        //verticalInput = Input.GetAxis("Vertical");
        if (!gameover)
        {
            //transform.Translate(0, 0, Time.deltaTime * speed * verticalInput);
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
            transform.Rotate(Vector3.up * Time.deltaTime * turnSpeed * horizontalInput);
            PowerIndicator.transform.position = transform.position + new Vector3(0.3f, 0.1f, 0);
            updateScore(1);
        }
        //jump
        if (Input.GetKeyDown(KeyCode.Space) && onGround && !gameover)
        {
            playerrg.AddForce(Vector3.up * 800, ForceMode.Impulse);
            onGround = false;
            playerAnm.SetTrigger("Jump_trig");
            playersound.PlayOneShot(jumpclip, 1);
        }
        if (transform.position.x < -4.12f)
        {
            transform.position = new Vector3(-4.12f, transform.position.y, transform.position.z);
        }

        if (transform.position.x > 4.5f)
        {
            transform.position = new Vector3(4.5f, transform.position.y, transform.position.z);
        }
    }
    //obstcales prefab (3 obstacles)
    void AddPrefab()
    {
        //float xpos = transform.position.x ;
        float xpos = Random.Range(3.83f, -1.58f);
        float ypos = 10.67447f;
        float zpos = transform.position.z + 30;
        int index = Random.Range(0, prefab.Length);
        if (gameover == false && !power)
        {
            Instantiate(prefab[index], new Vector3(xpos, ypos, zpos), prefab[index].transform.rotation);
        }
    }
    // money prefab
    void AddPrefabMoney()
    {
        float xpos = Random.Range(4, -3); ;
        float ypos = 10.67447f;
        float zpos = transform.position.z + 20;
        if (gameover == false)
        {
            Instantiate(prefabMoney, new Vector3(xpos, ypos, zpos), prefabMoney.transform.rotation);
        }
    }
    //power up prefab
    void AddPrefabPowerUp()
    {
        float xpos = transform.position.x;
        float ypos = 15;
        float zpos = transform.position.z + 15;
        int index = Random.Range(0, prefabPowerUp.Length);
        if (gameover == false)
        {
            Instantiate(prefabPowerUp[index], new Vector3(xpos, ypos, zpos), prefabPowerUp[index].transform.rotation);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // bottle power up to make police stop 6 sec
        if (other.CompareTag("PowerUp") && !gameover)
        {
            powerup = true;
            fireworks.Play();
            playersound.PlayOneShot(powerupclip, 3);
            Destroy(other.gameObject);
            StartCoroutine(powerupcountdown());
            PowerIndicator.SetActive(true);
            Debug.Log("WOWWWWW !!!!!......You Got The Power Up");
        }
        //diamond power up to make obstcale stop 5 sec
        if (other.CompareTag("Power") && !gameover)
        {
            power = true;
            fireworks.Play();
            playersound.PlayOneShot(powerupclip, 3);
            Destroy(other.gameObject);
            StartCoroutine(powercountdown());
            PowerIndicator.SetActive(true);
            Debug.Log("WOWWWWW !!!!!......You Got The Power Up");
        }
        //collide with police
        if (other.CompareTag("Police") && !gameover)
        {
            gameover = true;
            // Debug.Log("Game Over");
            playerAnm.SetBool("Death_b", true);
            playerAnm.SetInteger("DeathType_int", 1);
            ExplosionPlayer.Play();
            playersound.PlayOneShot(crashclip, 1);
            GameOverrrr();
            Debug.Log("payer collides with  " + other.name);
            PowerIndicator.SetActive(false);
        }

        if (other.gameObject.CompareTag("Money"))
        {
            Destroy(other.gameObject);
            updateMoney(10);
            playersound.PlayOneShot(Moneyclip, 1);
        }
    }

    // bottle power up to make police stop 6 sec
    IEnumerator powerupcountdown()
    {
        yield return new WaitForSeconds(6);
        powerup = false;
        PowerIndicator.SetActive(false);
        Debug.Log("Power Up is Endedddd");
    }

    // diamond power up to make obstcale stop 5 sec
    IEnumerator powercountdown()
    {
        yield return new WaitForSeconds(5);
        power = false;
        PowerIndicator.SetActive(false);
        Debug.Log("Power Up is Endedddd");
    }
    //collide with obstacle
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle") && !gameover)
        {
            gameover = true;
            // Debug.Log("Game Over");
            ExplosionPlayer.Play();
            playerAnm.SetBool("Death_b", true);
            playerAnm.SetInteger("DeathType_int", 1);
            playersound.PlayOneShot(crashclip, 1);
            GameOverrrr();
            Debug.Log("payer collides with  " + collision.gameObject.name);
            PowerIndicator.SetActive(false);
        }
        else
        {
            onGround = true;
        }
    }

    public void updateScore(int scoreVal)
    {
        score += scoreVal;
        scoreText.text = "Score : " + score;

    }

    public void updateMoney(int MoneyVal)
    {
        money += MoneyVal;
        moneyText.text = "Money : " + money;
    }

    //function to make display gameover message and restart buttom
    void GameOverrrr()
    {
        gameoverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
