using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Transform player;
    [SerializeField] private float speed;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float rotation = Mathf.LerpAngle(transform.rotation.eulerAngles.z, -Mathf.Atan2(player.position.x - transform.position.x, player.position.y - transform.position.y) * Mathf.Rad2Deg, 0.05f);
        transform.rotation = Quaternion.Euler(0, 0, rotation);
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
    }
}
