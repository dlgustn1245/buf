using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    public float speed;

    float rotate = 0.0f;
    Rigidbody2D rb2d;
    
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
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
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, rotate));
        rotate += 3.0f;
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}