using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float projectileInterval;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("ShootProjectile", projectileInterval, projectileInterval);
    }

    private void ShootProjectile()
    {
        GameObject spell = Instantiate(projectilePrefab, transform.position, transform.rotation);
        spell.GetComponent<Rigidbody2D>().AddForce(transform.up * projectileSpeed, ForceMode2D.Impulse);
    }
}
