using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{

    //audio clips
    public AudioClip playerDamaged, youDied;
    AudioSource playerAS;

    //health amounts
    public float fullHealth;
    public float currentHealth;

    //end game text & hurt animation
    public SpriteRenderer playerRenderer;
    public Image deathScreen;
    public CanvasGroup endCG;
    public Text endGameUIText;
    public string endText = "You Win!";
    private bool dead = false;
    public GameObject shadow;
    private float makeDeadCounter = 0;
    public GameObject newDeathImage;

    //health bar
    public Image healthSlider, hurtSlider, damageIndicator;
    public Text healthNumber;
    public Sprite yellowSlider, redSlider, greenSlider;

    //flash when hit
    Color flashColor = new Color(255f, 255f, 2555f, 0.5f), normalColor = new Color(255f, 255f, 2555f, 1f), deadColor = new Color(255f, 255f, 2555f, 0f);
    public float flashTime = .25f;
    float flashTimer = 0f, indicatorSpeed = 5f;
    bool damaged;

    //player controller
    public PlayerController PC;

    private void Start()
    {
        currentHealth = fullHealth;
        playerAS = GetComponent<AudioSource>();
        healthSlider.fillAmount = 1f;
        healthNumber.text = currentHealth.ToString() + " HP";
        playerRenderer = GetComponent<SpriteRenderer>();
        PC = GetComponent<PlayerController>();
        dead = false;
    }

    private void Update()
    {
        print("dead = " + dead);
        if (damaged && !PC.invincible)
        {
            if(currentHealth > 0)
            {
                damageIndicator.color = flashColor;
            }
            
        }
        else
        {
            damageIndicator.color = Color.Lerp(damageIndicator.color, Color.clear, indicatorSpeed * Time.deltaTime);
        }
        damaged = false;

        if (dead)
        {
            if (Input.GetButton("Jump"))
            {
                print("Pressed Enter");
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }

            if (Input.GetButton("Dash"))
            {
                SceneManager.LoadScene("world_map");
            }
        }

        if(currentHealth <= 0)
        {
            PC.myRB.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
            PC.maxSpeed = 0;
            playerRenderer.color = deadColor;
            dead = true;
            shadow.SetActive(false);
            PC.holdingDash = true;
            PC.holdingJump = true;
        }

    }

    public void addDamage(float damage)
    {
        damaged = true;
        if (damage <= 0)
        {
            return;
        }
        StartCoroutine(gameObject.GetComponent<PlayerController>().playerHit(1f));
        if (!PC.invincible)
        {            
            if(currentHealth > 1)
            {
                playerAS.PlayOneShot(playerDamaged, .5f);
                currentHealth -= damage;
                healthSlider.fillAmount = 0 + currentHealth / fullHealth;
                healthNumber.text = currentHealth.ToString() + " HP";
            } else if(currentHealth <= 1 && makeDeadCounter <= 0)
            {
                playerAS.PlayOneShot(youDied, .5f);
                currentHealth -= damage;
                healthSlider.fillAmount = 0 + currentHealth / fullHealth;
                healthNumber.text = currentHealth.ToString() + " HP";
            }
            
        }
        
        if(currentHealth / fullHealth <= .67 && currentHealth / fullHealth > .34)
        {
            healthSlider.sprite = yellowSlider;
        }
        if(currentHealth / fullHealth <= .34)
        {            
            healthSlider.sprite = redSlider;

        }
        if(currentHealth == 0 && makeDeadCounter <= 0)
        {
            makeDeadCounter++;
            MakeDead();
        }
            
    }

    public void addHealth(float health)
    {
        currentHealth += health;
        healthSlider.fillAmount = 0 + currentHealth / fullHealth;
        healthNumber.text = currentHealth.ToString() + " HP";


        if (currentHealth / fullHealth > .67)
        {
            healthSlider.sprite = greenSlider;
        }
        if (currentHealth / fullHealth <= .67 && currentHealth / fullHealth >= .33)
        {
            healthSlider.sprite = yellowSlider;
        }
        if (currentHealth / fullHealth < .33)
        {
            healthSlider.sprite = redSlider;
        }

    }

    public void MakeDead()
    {        
        endText = "You Died";
        deathScreen.color = Color.red;
        damageIndicator.color = normalColor;
        EndGame();
        //Destroy(gameObject);
    }

    public void EndGame()
    {
        endGameUIText.text = endText;
        endCG.alpha = 1;
        newDeathImage.SetActive(true);
    }
}
