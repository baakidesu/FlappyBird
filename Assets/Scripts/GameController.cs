using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    public GameObject Sky1;
    public GameObject Sky2;

    public float backgroundSpeed = -1.5f;

    public GameObject spike;
    public int howManySpike = 0;

    private float deltaTime = 0f;

    private GameObject[] spikes;

    private int counter;

    private Rigidbody2D rb1;
    private Rigidbody2D rb2;

    private float length = 0;

    private bool gameContinue = true;
    void Start()
    {
        rb1 = Sky1.GetComponent<Rigidbody2D>();
        rb2 = Sky2.GetComponent<Rigidbody2D>();

        rb1.velocity = new Vector2(backgroundSpeed, 0);
        rb2.velocity = new Vector2(backgroundSpeed, 0);

        length = Sky1.GetComponent<BoxCollider2D>().size.x;

        spikes = new GameObject[howManySpike];

        for (int i = 0; i < spikes.Length; i++)
        {
            spikes[i] = Instantiate(spike, new Vector3(-20, -20), quaternion.identity);
            Rigidbody2D rb = spikes[i].AddComponent<Rigidbody2D>();
            rb.gravityScale = 0;
            rb.velocity = new Vector2(backgroundSpeed, 0);
        }
    }

    void Update()
    {

        if (gameContinue)
        {
            #region Background Movement

            if (Sky1.transform.position.x <= -length)
            {
                Sky1.transform.position += new Vector3(length * 2, 0);
            }  
            if (Sky2.transform.position.x <= -length)
            {
                Sky2.transform.position += new Vector3(length * 2, 0);
            }

            #endregion

            #region Spike
            deltaTime += Time.deltaTime;
            if (deltaTime > 2f)
            {
                deltaTime = 0f;
                float yAxis = Random.Range(-1.29f, 1f);
                spikes[counter].transform.position = new Vector3(12, yAxis);
                counter++;
                if (counter >= spikes.Length)
                {
                    counter = 0;
                }
            }
            #endregion
        }
    }

    public void GameOver()
    {
        gameContinue = false;
        for (int i = 0; i < spikes.Length; i++)
        {
            spikes[i].GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            rb1.velocity = Vector2.zero;
            rb2.velocity = Vector2.zero;
        }
    }
}
