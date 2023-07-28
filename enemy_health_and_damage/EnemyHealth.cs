using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    //main
    Color flashColor = new Color(255f, 255f, 2555f, 0.5f), normalColor = new Color(255f, 255f, 2555f, 1f);
    SpriteRenderer enemyRenderer;
    Animator enemyAnim;
    AudioSource enemyAS;
    Transform enemyTransform;
    Rigidbody2D myRB;
    public AudioClip deathSound;
    public GameObject explosion;

    //hurt actions
    public float flashTime = 1f, pushBackForce;
    float flashTimer = 0f;

    //health
    public int maxHealth = 100;
    int currentHealth;

    float explosionCount = 0;

    //public Canvas canvas;
  //  public Slider Slider;
  //  public Color low;
   // public Color high;
    //public Vector3 offset;

    void Start()
    {
        currentHealth = maxHealth;
        enemyRenderer = GetComponent<SpriteRenderer>();
        enemyAnim = GetComponent<Animator>();
        enemyAS = GetComponent<AudioSource>();
        enemyTransform = GetComponent<Transform>();
        myRB = GetComponent<Rigidbody2D>();
        //enemyRenderer.color = normalColor;
    }

    // Update is called once per frame
    void Update()
    {
        flashTimer += Time.deltaTime;
        if (flashTimer > flashTime && currentHealth - 2 >= 0)
        {
            enemyRenderer.color = normalColor;
        }

       // Slider.transform.position = Camera.main.WorldToScreenPoint(enemyTransform.position + offset);
        //canvas.transform.position = enemyTransform.position + offset;
        SetHealth();
    }

    public void SetHealth()
    {
       /* Slider.gameObject.SetActive(currentHealth < maxHealth);
        Slider.value = currentHealth;
        Slider.maxValue = maxHealth;

        Slider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(low, high, Slider.normalizedValue);*/
    }


    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
       // enemyRenderer.color = new Color(1f, 1f, 1f, 0.5f);

        if (currentHealth > 0)
        {
            enemyRenderer.color = flashColor;
            flashTimer = 0f;
        }

        //play hurt animation

        /*if (currentHealth - 2 < 0)
        {
            enemyAS.PlayOneShot(deathSound);
        }*/

        if (currentHealth <= 0)
        {
            Die();
        }

    }


    void Die()
    {

        
        if (explosionCount < 1)
        {
            if (gameObject.tag == "frog")
            {
                enemyAS.PlayOneShot(deathSound, 0.2f);
                enemyRenderer.color = new Color(0f, 0f, 0f, 0f);
                GameObject explosionClone = (GameObject)Instantiate(explosion, transform.position, Quaternion.identity);
                Destroy(explosionClone, .79f);
                GetComponent<CircleCollider2D>().enabled = false;
                GetComponent<BoxCollider2D>().enabled = false;
                Destroy(gameObject, .5f);
                explosionCount += 1;
            }

            if (transform.parent is null)
            {
                enemyAS.PlayOneShot(deathSound, 0.2f);
                enemyRenderer.color = new Color(1f, 1f, 1f, 0f);
                GameObject explosionClone = (GameObject)Instantiate(explosion, transform.position, Quaternion.identity);
                Destroy(explosionClone, .79f);
                GetComponent<Collider2D>().enabled = false;
                Destroy(gameObject, .5f);
                explosionCount += 1;

            }
            else
            {
                enemyAS.PlayOneShot(deathSound, 0.2f);
                enemyRenderer.color = new Color(0f, 0f, 0f, 0f);
                GameObject explosionClone = (GameObject)Instantiate(explosion, transform.position, Quaternion.identity);
                Destroy(explosionClone, .79f);
                GetComponent<Collider2D>().enabled = false;
                GetComponentInParent<Collider2D>().enabled = false;
                Destroy(gameObject, .5f);
                explosionCount += 1;
            }
        }
    
    }
}
