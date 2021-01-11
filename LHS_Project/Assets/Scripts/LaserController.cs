using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour
{
    public float speed;
    public float yMax;
    public GameObject flame;
    Rigidbody2D rb2d;
    
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
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
        position.y += speed * Time.deltaTime;
        rb2d.MovePosition(position);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Instantiate(flame, rb2d.position, Quaternion.identity);
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Bomb"))
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Asteroid"))
        {
            Destroy(gameObject);
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
