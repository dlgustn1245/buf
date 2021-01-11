using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed;
    public int score;
    public int maxStamina;
    public GameObject explosion;
    public GameObject bomb;
    public GameObject heart;
    public GameObject attackSpeed;
    public GameObject attackDamage;
    Rigidbody2D rb2d;
    int currentStamina;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        currentStamina = maxStamina;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.Instance.gameOver)
        {
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        Vector2 position = rb2d.position;
        position.y -= speed * Time.deltaTime;

        rb2d.MovePosition(position);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Laser"))
        {
            currentStamina -= PlayerController.currentDamage;
            if (currentStamina <= 0)
            {
                GameController.Instance.FighterScored(score);
                Instantiate(explosion, rb2d.position, Quaternion.identity);

                int randDrop = Random.Range(0, 25); //0~24
                if (randDrop < 3) //0, 1, 2
                {
                    //Debug.Log("Drop bomb");
                    Instantiate(bomb, rb2d.position, Quaternion.identity);
                }
                else if (randDrop >= 3 && randDrop < 6) //3, 4, 5
                {
                    //Debug.Log("Drop heart");
                    Instantiate(heart, rb2d.position, Quaternion.identity);
                }
                else if (randDrop >= 6 && randDrop < 9) //6, 7, 8
                {
                    //Debug.Log("Drop attackSpeed");
                    Instantiate(attackSpeed, rb2d.position, Quaternion.identity);
                }
                else if(randDrop >=9 && randDrop < 12)
                {
                    //Debug.Log("Drop attackDamage");
                    Instantiate(attackDamage, rb2d.position, Quaternion.identity);
                }
                Destroy(gameObject);
                Destroy(collision.gameObject);
            }
            Destroy(collision.gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
