using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject explosionPrefab;
    public GameObject[] items; //AttackDamageItem, AttackSpeedItem, Bomb, HeartItem

    public float speed;

    public int score;
    public int maxStamina;

    int currentStamina;

    Rigidbody2D rb2d;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        currentStamina = maxStamina;
    }

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

                int randNum = Random.Range(0, 13); //0~12
                if (randNum < items.Length)
                {
                    Instantiate(items[randNum], rb2d.position, Quaternion.identity);
                }

                Destroy(gameObject);
            }
            Destroy(collision.gameObject);
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
