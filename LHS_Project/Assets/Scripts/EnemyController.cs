using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject explosionPrefab;
    public GameObject bomb;
    public GameObject heartItem;
    public GameObject attackSpeedItem;
    public GameObject attackDamageItem;

    public int score;
    public int maxStamina;

    int currentStamina;

    public float speed;

    Rigidbody2D rb2d;

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

                GameObject explosion = Instantiate(explosionPrefab, rb2d.position, Quaternion.identity) as GameObject;
                Destroy(explosion, 0.8f);

                int randDrop = Random.Range(0, 25); //0~24
                if (randDrop < 3) //0, 1, 2
                {
                    Instantiate(bomb, rb2d.position, Quaternion.identity);
                }
                else if (randDrop >= 3 && randDrop < 6) //3, 4, 5
                {
                    Instantiate(heartItem, rb2d.position, Quaternion.identity);
                }
                else if (randDrop >= 6 && randDrop < 9) //6, 7, 8
                {
                    Instantiate(attackSpeedItem, rb2d.position, Quaternion.identity);
                }
                else if(randDrop >=9 && randDrop < 12)
                {
                    Instantiate(attackDamageItem, rb2d.position, Quaternion.identity);
                }
                Destroy(gameObject);
            }
            Destroy(collision.gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
