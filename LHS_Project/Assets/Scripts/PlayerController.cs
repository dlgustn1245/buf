using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static int currentDamage = 1;
    public float xMin, xMax, yMin, yMax;
    public float timeInvincible = 2.0f;
    public float shootDelay = 0.5f;
    public float speed;
    public int maxDamage = 3;
    public GameObject laser;
    public GameObject explosion;
    public Text healthText;
    float horizontal, vertical;
    float shootTimer;
    float invincibleTimer;
    bool canShoot = true;
    bool isInvincible = false;
    int maxHealth = 3;
    int currentHealth;
    Rigidbody2D rb2d;
    
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if(invincibleTimer < 0)
            {
                isInvincible = false;
            }
        }
        ShootLaser();
    }

    void FixedUpdate()
    {
        Vector2 position = rb2d.position;
        position.x += speed * horizontal * Time.deltaTime;
        position.y += speed * vertical * Time.deltaTime;

        position.x = Mathf.Clamp(position.x, xMin, xMax);
        position.y = Mathf.Clamp(position.y, yMin, yMax);

        rb2d.MovePosition(position);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Bomb") || collision.gameObject.CompareTag("Asteroid"))
        {
            if (isInvincible) return;

            ChangeHealth(-1);
            isInvincible = true;
            invincibleTimer = timeInvincible;

            Instantiate(explosion, rb2d.position, Quaternion.identity);

            if (collision.gameObject.CompareTag("Asteroid")) return;
            Destroy(collision.gameObject);
        }
        else if(collision.gameObject.CompareTag("Health"))
        {
            if(currentHealth < maxHealth)
            {
                ChangeHealth(1);
                Destroy(collision.gameObject);
            }
        }
        else if (collision.gameObject.CompareTag("AttackSpeed"))
        {
            if(shootDelay >= 0.3f)
            {
                shootDelay -= 0.05f;
                Destroy(collision.gameObject);
            }
        }
        else if (collision.gameObject.CompareTag("AttackDamage"))
        {
            if (currentDamage < maxDamage)
            {
                ++currentDamage;
                Destroy(collision.gameObject);
            }
        }
    }

    void ShootLaser()
    {
        if (canShoot)
        {
            if(shootTimer >= shootDelay)
            {
                Instantiate(laser, rb2d.position + Vector2.up * 1.2f, Quaternion.identity);
                shootTimer = 0;
            }
        }
        shootTimer += Time.deltaTime;
    }

    void ChangeHealth(int amount)
    {
        if(amount < 0)
        {
            if (isInvincible) return;
            invincibleTimer = timeInvincible;
        }
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);

        healthText.text = "Health : ";
        for(int i = 0; i < currentHealth; i++)
        {
            healthText.text += "♥ ";
        }

        if(currentHealth == 0)
        {
            GameController.Instance.FighterDead();
            Destroy(gameObject);
        }
    }
}
