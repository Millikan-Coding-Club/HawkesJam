using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingEnemy : MonoBehaviour
{
    [SerializeField] GameObject explosionPrefab;

    private void OnDestroy()
    {
        Explode();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player") { Explode(); }
    }

    private void Explode()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
    }
}
