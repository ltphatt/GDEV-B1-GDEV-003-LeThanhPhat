using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int HP = 3;
    public int moveSpeed = 5;
    public GameObject bulletPrefab;

    public float fireRate = 0.5f;
    float fireTimer = 0f;
    bool canFire = true;

    bool isAlive = true;

    void Update()
    {
        if (!isAlive)
        {
            return;
        }

        Move();
        Shoot();

        fireTimer += Time.deltaTime;
        if (fireTimer >= fireRate)
        {
            canFire = true;
            fireTimer = 0f;
        }
    }

    void Move()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            transform.rotation = Quaternion.LookRotation(Vector3.forward, mousePosition - transform.position);
            transform.position = Vector3.MoveTowards(transform.position, mousePosition, moveSpeed * Time.deltaTime);
        }
    }

    void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canFire)
        {
            Instantiate(bulletPrefab, transform.position, transform.rotation);
            canFire = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);

        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(1);

            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(1);
            }
        }
    }

    public void TakeDamage(int dmg)
    {
        HP -= dmg;
        if (HP <= 0)
        {
            Destroy(gameObject);
            isAlive = false;
        }
    }

}
