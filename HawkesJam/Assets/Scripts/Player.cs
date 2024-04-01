using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] List<GameObject> spells = new List<GameObject>();
    [SerializeField] TMP_Text DNA_countText;
    [SerializeField] AudioSource plopSound;
    [SerializeField] AudioSource damageSound;
    [SerializeField] GameObject healthBar;

    private float healthBarWidth = 500;
    [SerializeField] private float speed;
    [SerializeField] private float spellSpeed;
    private float DNA_count = 0;
    [SerializeField] private int maxHealth = 100;
    public int health;
    private bool letPlayerShoot = true;
    private GameObject selectedSpell;

    // Start is called before the first frame update
    void Start()
    {
        selectedSpell = spells[0];
        health = maxHealth;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedSpell = spells[0];
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            selectedSpell = spells[1];
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            selectedSpell = spells[2];
        }

        if (Input.GetMouseButton(0) && letPlayerShoot)
        {
            GameObject spell = Instantiate(selectedSpell, transform.position, transform.rotation);
            StartCoroutine(StartCooldown());
        }
    }

    private IEnumerator StartCooldown()
    {
        letPlayerShoot = false;
        yield return new WaitForSeconds(selectedSpell.GetComponent<Spell>().spellCooldown);
        letPlayerShoot = true;
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
        healthBarWidth -= damage * (500f / maxHealth);
        healthBar.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, healthBarWidth);
        damageSound.Play();
        if (health <= 0)
        {
            Debug.Log("game over");
        }
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
