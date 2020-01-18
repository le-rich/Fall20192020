using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EZCameraShake;
using UnityEngine.Audio;

public class Player : MonoBehaviour
{
    [Space(5)]
    [Header("Gameplay")]
    public int speed = 4;
    public int health = 3;
    public GameObject laser;
    public Transform laserSpawnPoint;
    [Space(5)]
    [Header("Shakes")]
    public float magnitude;
    public float roughness;
    public float fadeIn;
    public float fadeOut;
   
    public float timeInvulnerable = 70f;

    private Text scoreText;
    private int score = 0;
    private AudioSource audioSource;
    private Slider hpbar;
    private SliderLerp hplerp;

    private bool isInvulnerable = false;
    private float timeLastBlinked = 0.0f;
    private float blinkDelay = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        scoreText = GameObject.FindGameObjectWithTag("Score").GetComponent<Text>();
        score = 0;
        scoreText.text = "Score: " + score;

        audioSource = GetComponent<AudioSource>();

        hpbar = GameObject.FindGameObjectWithTag("hpbar").GetComponent<Slider>();
        hplerp = hpbar.GetComponent<SliderLerp>();
        hpbar.maxValue = health;
        hpbar.value = health;
        hplerp.SetTargetValue(health);

    }

    // Update is called once per frame
    void Update()
    {
        CheckControls();
        CheckControllerControls();

        if (isInvulnerable)
        {
            Blink();
        }
    }

    private void TakeDamage(int damage)
    {
        health -= damage;
        //hplerp.StartCoroutine("LerpTo", health);
        hplerp.SetTargetValue(health);

        if (health <= 0)
        {
            GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>().StartRespawn();
            Destroy(gameObject);
        }
    }

    private void CheckControls()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector2.up * Time.deltaTime * speed);
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector2.down * Time.deltaTime * speed);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector2.left * Time.deltaTime * speed);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector2.right * Time.deltaTime * speed);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            FireLaser();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && !isInvulnerable)
        {
            Destroy(collision.gameObject);
            TakeDamage(1);
        }
    }

    public void AddScore(int score)
    {
        //this refers to the score contained on the script itself.
        this.score += score;
        scoreText.text = "Score: " + this.score;
    }
    

    public void StartBlink()
    {
        isInvulnerable = true;


        Invoke("ResetInvulnerable", timeInvulnerable);
    }

    private void ResetInvulnerable()
    {
        isInvulnerable = false;

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.enabled = true;
    }


    private void Blink()
    {
        if (Time.time > timeLastBlinked + blinkDelay)
        {
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            sr.enabled = !sr.enabled;

            timeLastBlinked = Time.time;
        }
    }

    private void CheckControllerControls()
    {
        if (Input.GetAxis("Y") > 0)
        {
            transform.Translate(Vector2.up * Time.deltaTime * speed * Input.GetAxis("Y"));
        }

        if (Input.GetAxis("Y") < 0)
        {
            transform.Translate(Vector2.down * Time.deltaTime * speed * Mathf.Abs(Input.GetAxis("Y")));
        }

        if (Input.GetAxis("X") > 0)
        {
            transform.Translate(Vector2.right * Time.deltaTime * speed * Input.GetAxis("X"));
        }

        if (Input.GetAxis("X") < 0)
        {
            transform.Translate(Vector2.left * Time.deltaTime * speed * Mathf.Abs(Input.GetAxis("X")));
        }

        if (Input.GetAxis("Shoot") > 0)
        {
            FireLaser();
        }
    }

    private void FireLaser()
    {
        Instantiate(laser, transform.position, transform.rotation);
        CameraShaker.Instance.ShakeOnce(magnitude, roughness, fadeIn, fadeOut);
        audioSource.pitch = 1 + Random.Range(-0.25f, 0.25f);
        audioSource.PlayOneShot(audioSource.clip);
    }
}
