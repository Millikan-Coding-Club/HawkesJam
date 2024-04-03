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
    [SerializeField] TMP_Text VirusDNA_countText;
    [SerializeField] TMP_Text ExplodingDNA_countText;
    [SerializeField] TMP_Text SplittingDNA_countText;
    [SerializeField] GameObject DefaultOutline;
    [SerializeField] GameObject ExplodingOutline;
    [SerializeField] GameObject VirusOutline;
    [SerializeField] GameObject SplittingOutline;
    [SerializeField] AudioSource plopSound;
    [SerializeField] AudioSource damageSound;
    [SerializeField] GameObject healthBar;

    private float healthBarWidth = 500;
    [SerializeField] private float speed;
    [SerializeField] private float spellSpeed;
    private float DNA_count = 0;
    private float VirusDNA_count = 0;
    private float ExplodingDNA_count = 0;
    private float SplittingDNA_count = 0;
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

    private void DisableOutlines()
    {
        DefaultOutline.SetActive(false);
        SplittingOutline.SetActive(false);
        VirusOutline.SetActive(false);
        ExplodingOutline.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            DisableOutlines();
            selectedSpell = spells[0];
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            DisableOutlines();
            selectedSpell = spells[1];
            ExplodingOutline.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            DisableOutlines();
            selectedSpell = spells[2];
            VirusOutline.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            DisableOutlines();
            selectedSpell = spells[3];
            SplittingOutline.SetActive(true);
        }

        if (Input.GetMouseButton(0) && letPlayerShoot)
        {
            switch(selectedSpell.name)
            {
                case "DefaultSpell":
                    Shoot();
                    break;
                case "FireSpell":
                    if (ExplodingDNA_count > 0)
                    {
                        ExplodingDNA_count--;
                        ExplodingDNA_countText.text = ExplodingDNA_count.ToString();
                        Shoot();
                    }
                    break;
                case "PoisonSpell":
                    if (VirusDNA_count > 0)
                    {
                        VirusDNA_count--;
                        VirusDNA_countText.text = VirusDNA_count.ToString();
                        Shoot();
                    }
                    break;
                case "SplittingSpell":
                    if (SplittingDNA_count > 0)
                    {
                        SplittingDNA_count--;
                        SplittingDNA_countText.text = SplittingDNA_count.ToString();
                        Shoot();
                    }
                    break;

            }
        }
    }

    private void Shoot()
    {
        Vector2 aimDirection = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - rb.position;
        Instantiate(selectedSpell, transform.position, Quaternion.Euler(0, 0, Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f));
        if (selectedSpell == spells[3])
        {
            float spellSpread = 15f;
            Instantiate(selectedSpell, transform.position, Quaternion.Euler(0, 0, Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f + spellSpread));
            Instantiate(selectedSpell, transform.position, Quaternion.Euler(0, 0, Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f - spellSpread));
        }
        StartCoroutine(StartCooldown());
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
        if (rb.velocity.x > 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        } else if (rb.velocity.x < 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
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
            plopSound.Play();
            if (collision.name.Contains("Default"))
            {
                DNA_count++;
                DNA_countText.text = DNA_count.ToString();
            }
            if (collision.name.Contains("Fire"))
            {
                ExplodingDNA_count++;
                ExplodingDNA_countText.text = ExplodingDNA_count.ToString();
            }
            if (collision.name.Contains("Splitting"))
            {
                SplittingDNA_count++;
                SplittingDNA_countText.text = SplittingDNA_count.ToString();
            }
            if (collision.name.Contains("Virus"))
            {
                VirusDNA_count++;
                VirusDNA_countText.text = VirusDNA_count.ToString();
            }
        }
    }
}
