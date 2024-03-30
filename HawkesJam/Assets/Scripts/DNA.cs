using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class DNA : MonoBehaviour
{
    private Transform player;

    [SerializeField] private float collectDistance;
    [SerializeField] private float collectSpeed;

    private void Start()
    {
        GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
        player = GameObject.Find("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, player.position) < collectDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, Time.deltaTime * collectSpeed);
        }
    }
}
