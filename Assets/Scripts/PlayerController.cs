using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float tilt = 1;
	public float speed;
    public Rigidbody myRigidbody;

    public int health = 100;
    public float smoothing = 5;
    private Vector3 smoothDirection;

    public Cannon leftCannon;
    public Cannon rightCannon;
    public TextMeshProUGUI scoreTextMesh;

    public event Action OnCollision = delegate { };
    public event Action OnDie = delegate { };

    // Use this for initialization
    void Start ()
    {
		myRigidbody = GetComponent<Rigidbody>();
        UpdateHealth();
	}

    private void UpdateHealth()
    {
        scoreTextMesh.text = health.ToString();
    }

    // Update is called once per frame
    void Update ()
    {
        leftCannon.IsFiring = Input.GetButton("Fire1");
        rightCannon.IsFiring = Input.GetButton("Fire1");
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        if (moveHorizontal != 0 || moveVertical != 0)
        {
            myRigidbody.velocity = new Vector3(moveHorizontal, 0.0f, moveVertical) * speed;
        }

        myRigidbody.rotation = Quaternion.Euler(myRigidbody.velocity.z * tilt, 0, myRigidbody.velocity.x * -tilt);
    }

    void OnCollisionEnter(Collision collision)
    {
        OnCollision();
        health -= 10;

        if (health <= 0)
        {
            OnDie();
        }

        UpdateHealth();
    }
}
