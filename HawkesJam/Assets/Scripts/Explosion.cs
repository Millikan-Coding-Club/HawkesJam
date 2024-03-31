using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private int explosionDamage;
    [SerializeField] private float explosionSpeed;
    [SerializeField] private float maxSize;

    private void Start()
    {
        GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
        transform.localScale = Vector3.zero;
    }

    private void Update()
    {
        transform.localScale += Vector3.one * explosionSpeed * Time.deltaTime;
        if (transform.localScale.magnitude > maxSize)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(explosionDamage);
        }
        if (collision.gameObject.name == "Player")
        {
            collision.gameObject.GetComponent<Player>().TakeDamage(explosionDamage);
        }
    }
}
