using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int HP = 3;
    public int moveSpeed = 5;
    public GameObject bulletPrefab;

    void Update()
    {
        Move();
        Shoot();
    }

    void Move()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            transform.position = Vector3.MoveTowards(transform.position, mousePosition, moveSpeed * Time.deltaTime);
        }
    }

    void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(bulletPrefab, transform.position, Quaternion.identity);

            Rigidbody2D rb = bulletPrefab.GetComponent<Rigidbody2D>();
            rb.AddForce(Vector2.up * 500);
        }
    }

}
