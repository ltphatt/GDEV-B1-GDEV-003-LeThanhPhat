using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour, IDamagable
{
    public int HP = 3;
    public float moveSpeed = 5f;
    public GameObject bulletPrefab;
    public float fireRate = 0.5f;
    float fireTimer = 0f;
    bool canFire = true;
    bool isAlive = true;
    int enemyCount = 0;

    public TextMeshProUGUI hpText;
    public TextMeshProUGUI enemyCountText;

    void Start()
    {
        hpText.text = "HP: " + HP;
        enemyCountText.text = "Enemies: " + enemyCount;
    }

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

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(1);
        }
    }

    public void TakeDamage(int dmg)
    {
        HP -= dmg;
        hpText.text = "HP: " + HP;
        if (HP <= 0)
        {
            Destroy(gameObject);
            isAlive = false;

            GameManager.Instance.ShowLosePopup();
        }
    }

}
