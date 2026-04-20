using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
{
    public int HP = 1;
    public float moveSpeed = 2;
    float baseSpeed = 0;
    Vector3 currentDirection;
    Rigidbody2D rb;
    public float changeDirectionInterval = 2f;
    float timer = 0f;
    public float detectionRadius = 5f;
    Player targetPlayer;
    bool isChasing = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        isChasing = false;
        baseSpeed = moveSpeed;
    }

    void Start()
    {
        currentDirection = GetRandomDirection();
        transform.rotation = Quaternion.LookRotation(Vector3.forward, currentDirection);
    }

    void Update()
    {
        rb.velocity = currentDirection * moveSpeed;

        timer += Time.deltaTime;
        if (timer >= changeDirectionInterval && !isChasing)
        {
            currentDirection = GetRandomDirection();
            transform.rotation = Quaternion.LookRotation(Vector3.forward, currentDirection);
            timer = 0f;
        }

        CheckNearbyPlayer();
    }

    Vector3 GetRandomDirection()
    {
        float angle = Random.Range(0f, 360f);
        return new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            TakeDamage(1);
        }
    }

    public void TakeDamage(int dmg)
    {
        HP -= dmg;
        if (HP <= 0)
        {
            Destroy(gameObject);
        }
    }

    void CheckNearbyPlayer()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, detectionRadius);
        foreach (Collider2D hit in hits)
        {
            if (hit.gameObject.CompareTag("Player"))
            {
                targetPlayer = hit.GetComponent<Player>();
                ChasePlayer();
                return;
            }
        }
        targetPlayer = null;
        isChasing = false;
        moveSpeed = baseSpeed;
    }

    void ChasePlayer()
    {
        if (targetPlayer != null)
        {
            isChasing = true;
            Vector3 directionToPlayer = (targetPlayer.transform.position - transform.position).normalized;
            currentDirection = directionToPlayer;
            transform.rotation = Quaternion.LookRotation(Vector3.forward, currentDirection);

            Player player = targetPlayer.GetComponent<Player>();
            moveSpeed = player.moveSpeed * 1.2f;
        }
    }
}
