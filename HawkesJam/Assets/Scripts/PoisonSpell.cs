using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonSpell : MonoBehaviour
{
    [SerializeField] private float poisonInterval;
    [SerializeField] private int poisonDamage;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" && !collision.name.Contains("Virus"))
        {
            collision.gameObject.GetComponent<Enemy>().InflictPoison(poisonInterval, poisonDamage);
        }
    }
}
