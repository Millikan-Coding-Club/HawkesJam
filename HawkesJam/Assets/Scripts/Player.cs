using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] GameObject spellPrefab;
    [SerializeField] TMP_Text DNA_countText;
    [SerializeField] AudioSource plopSound;
    [SerializeField] AudioSource damageSound;

    [SerializeField] private float speed;
    [SerializeField] private float spellSpeed;
    private float DNA_count = 0;
    public int health = 100;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (health <= 0)
        {
            Debug.Log("game over");
        }
        if (Input.GetMouseButtonDown(0))
        {
            GameObject spell = Instantiate(spellPrefab, transform.position, transform.rotation);
            spell.GetComponent<Rigidbody2D>().AddForce(transform.up * spellSpeed, ForceMode2D.Impulse);
        }
    }

    void FixedUpdate()
    {
            rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized * Time.deltaTime * speed;
        Vector2 aimDirection = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - rb.position;
        rb.rotation = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        damageSound.Play();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "DNA")
        {
            Destroy(collision.gameObject);
            DNA_count++;
            DNA_countText.text = DNA_count.ToString();
            plopSound.Play();
        }
    }
}
