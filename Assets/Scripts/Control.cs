  using System;
  using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Control : MonoBehaviour
{
    [Header("Animation")]
    public Sprite[] bird;
   
    [Header("Movement")]
    public float addForceY = 200f;

    [Header("UI")] 
    public TMP_Text pointText;

    private AudioSource[] sounds;
    
    private SpriteRenderer spriteRenderer;
    private bool forwardChecker = true;
    private int birdCounter = 0;
    private float birdAnimationTime = 0;
    private Rigidbody2D rb;

    private bool canMove = true;

    private GameController controlGame;

    private bool soundChecker = true;
    private int point = 0;
    private int maxPoint = 0;
    void Start()
    {
        maxPoint = PlayerPrefs.GetInt("maxPoint");
        sounds = GetComponents<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        controlGame = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canMove)
        {
            sounds[2].Play();
            rb.velocity = new Vector2(0, 0);
            rb.AddForce(new Vector2(0, addForceY));
        }

        if (rb.velocity.y > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 45);
        }else if (rb.velocity.y < 0)
        {
            transform.eulerAngles = new Vector3(0,0,-45);
        }
        
        Animation();
    }
    
    void Animation()
    {
        birdAnimationTime += Time.deltaTime;
        if (birdAnimationTime > 0.2f)
        {
            birdAnimationTime = 0;
            if (forwardChecker)
            {
                spriteRenderer.sprite = bird[birdCounter];
                birdCounter++;
                if (birdCounter == bird.Length)
                {
                    birdCounter--;
                    forwardChecker = false;
                }

            }
            else
            {
                birdCounter--;
                spriteRenderer.sprite = bird[birdCounter];
                if (birdCounter == 0)
                {
                    birdCounter++;
                    forwardChecker = true;
                }

            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag=="Point")
        {
            sounds[1].Play();
            point++;
            pointText.text = "Point = " + point;
            Debug.Log(point);
        }else if (other.tag == "Spike")
        {
            if (soundChecker)
            {
                sounds[0].Play();
                canMove = false; 
                controlGame.GameOver();
                soundChecker = false;
                Invoke("returnMainMenu", 2);
            }
        }

        if (point > maxPoint)
        {
            maxPoint = point;
            PlayerPrefs.SetInt("maxPoint", maxPoint);
        }
    }

    void returnMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}

  
