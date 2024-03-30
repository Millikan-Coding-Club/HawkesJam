using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject DNAPrefab;
    private Transform player;
    private Rigidbody2D rb;

    [SerializeField] private float speed;
    [SerializeField] private int DNA_amount;
    [SerializeField] private float DNA_maxSpeed;
    public int playerDamage = 20;
    public int health = 100;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            for (int i = 0; i < DNA_amount; i++)
            {
                GameObject DNA = Instantiate(DNAPrefab, transform.position, Quaternion.identity);
                float angle = 360 / DNA_amount * i;
                float force = (Random.value * DNA_maxSpeed - 25) + 25;
                Vector2 direction = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
                DNA.GetComponent<Rigidbody2D>().AddForce(direction * force);
            }
            Destroy(gameObject);
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
        float rotation = Mathf.LerpAngle(transform.rotation.eulerAngles.z, -Mathf.Atan2(player.position.x - transform.position.x, player.position.y - transform.position.y) * Mathf.Rad2Deg, 0.05f);
        rb.rotation = rotation;
        rb.velocity = (player.position - transform.position).normalized * speed * Time.deltaTime;
    }
}
