using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusEnemy : MonoBehaviour
{
    [SerializeField] private int poisonDamage;
    [SerializeField] private int poisonAmount;
    [SerializeField] private int poisonInterval;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            StartCoroutine(PoisonPlayer(collision.gameObject, poisonDamage));
        }
    }

    private IEnumerator PoisonPlayer(GameObject player, int damage)
    {
        for (int i = 0; i < poisonAmount; i++)
        {
            yield return new WaitForSeconds(poisonInterval);
            player.GetComponent<Player>().TakeDamage(damage);
        }
    }
}
