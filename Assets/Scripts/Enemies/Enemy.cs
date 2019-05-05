using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 100;
    public float maxHealth = 100;

    public float speed;
    public Rigidbody myRigidbody;
    public RectTransform foregroundHealthBar;

    private bool canBeDestroyed;
    private float fullHealthBarSize;

    public event System.Action<float> OnDamaged = delegate { };
    public event System.Action OnDie = delegate { };

    public void TakeDamage(float amount)
    {
        health -= amount;
        UpdateHealthBar();

        if (health <= 0)
        {
            Die();
        }

        OnDamaged(health);
    }

    void Start()
    {
        speed = speed - Random.Range(0f, 4f);
        GetComponent<Rigidbody>().velocity = transform.forward * speed;
        fullHealthBarSize = foregroundHealthBar.sizeDelta.x;
    }

    void Update()
    {
        
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    void OnBecameVisible()
    {
        canBeDestroyed = true;
    }

    void Die()
    {
        OnDie();
        Destroy(gameObject);
    }

    private void UpdateHealthBar()
    {
        var size = health * fullHealthBarSize / maxHealth;
        foregroundHealthBar.sizeDelta = new Vector2(size, foregroundHealthBar.sizeDelta.y);
    }
}
