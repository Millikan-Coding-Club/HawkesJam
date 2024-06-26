using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject DNAPrefab;
    private Transform player;
    private Rigidbody2D rb;
    [SerializeField] private GameObject splittingEnemyPrefab;
    [SerializeField] private GameObject virusDNA;
    [SerializeField] private GameObject explodingDNA;
    [SerializeField] private GameObject splittingDNA;
    private GameObject selectedDNA;

    [SerializeField] private float speed;
    [SerializeField] private int DNA_amount;
    [SerializeField] private float DNA_maxSpeed;
    public int playerDamage = 20;
    [SerializeField] private int maxHealth = 100;
    public int health;
    
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player").GetComponent<Transform>();
    }

    private void Awake()
    {
        health = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            for (int i = 0; i < DNA_amount; i++) // Spawn DNA
            {
                selectedDNA = DNAPrefab;
                if (Random.Range(0, 2) == 0)
                {
                    if (name.Contains("Exploding"))
                    {
                        selectedDNA = explodingDNA;
                    }
                    if (name.Contains("Splitting"))
                    {
                        selectedDNA = splittingDNA;
                    }
                    if (name.Contains("Virus"))
                    {
                        selectedDNA = virusDNA;
                    }
                }
                GameObject DNA = Instantiate(selectedDNA, transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
                float angle = 360 / DNA_amount * i;
                float force = (Random.value * DNA_maxSpeed - 25) + 25;
                Vector2 direction = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
                DNA.GetComponent<Rigidbody2D>().AddForce(direction * force);
            }
            if (name.Contains("Splitting") && maxHealth > 20)
            {
                for (int i = 0; i < 2; i++)
                {
                    GameObject clone = Instantiate(splittingEnemyPrefab, transform.position + transform.right * (i == 1 ? -0.2f : 0.2f), transform.rotation);
                    Enemy cloneScript = clone.GetComponent<Enemy>();
                    cloneScript.maxHealth -= 20;
                    cloneScript.health = cloneScript.maxHealth;
                    cloneScript.playerDamage -= 5;
                    cloneScript.DNA_amount -= 1;
                    clone.transform.localScale = transform.localScale * 0.8f;
                }
            }
            Destroy(gameObject);
        }
    }

    public void InflictPoison(float interval, int damage)
    {
        StartCoroutine(Poison(interval, damage));
    }

    private IEnumerator Poison(float interval, int damage)
    {
        while (health > 0)
        {
            TakeDamage(damage);
            yield return new WaitForSeconds(interval);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            collision.GetComponent<Player>().TakeDamage(playerDamage);
        }
    }

    private void FixedUpdate()
    {
        float rotation = Mathf.LerpAngle(transform.rotation.eulerAngles.z, -Mathf.Atan2(player.position.x - transform.position.x, player.position.y - transform.position.y) * Mathf.Rad2Deg - 90f, 0.05f);
        rb.rotation = rotation;
        rb.velocity = (player.position - transform.position).normalized * speed * Time.deltaTime;
    }
}
