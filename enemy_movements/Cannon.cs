using UnityEngine;
using System.Collections;

public class Cannon : MonoBehaviour
{
    public GameObject fireballPrefab;
    public GameObject explosionPrefab;
    public float fireRate = 3f;

    private float timeSinceLastFire = 0f;
    private Animator animator;
    public float cannonAnimTime = .419f;

    public float offsetLeft;
    public float offsetDown;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        timeSinceLastFire += Time.deltaTime;
        bool shoot = animator.GetBool("Shooting");
        print("shoot bool = " + shoot);

        if (timeSinceLastFire >= fireRate)
        {
            timeSinceLastFire = 0f;
            ShootFireball();
        }
    }

    public void ShootFireball()
    {
        StartCoroutine(ShootCoroutine());
        Vector3 position = transform.position + new Vector3(offsetLeft, offsetDown, 0f);
        Quaternion rotation = Quaternion.Euler(0f, 0f, 155f);
        GameObject fireball = Instantiate(fireballPrefab, position, rotation);
        Destroy(fireball, 3f);

        /*GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(explosion, .3f);*/
    }

    private IEnumerator ShootCoroutine()
    {
        animator.SetBool("Shooting", true);
        yield return new WaitForSeconds(cannonAnimTime);
        animator.SetBool("Shooting", false);
    }

    public void SetShootFalse()
    {
        animator.SetBool("Shooting", false);
    }

    /*private void OnDestroy()
    {
        if (fireball != null)
        {
            Destroy(fireball);
        }
    }*/
}
