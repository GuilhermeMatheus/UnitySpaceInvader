using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public float rotationSpeed = 5f;
    public float maxRotation = 45f;

    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate;
    public float delay;

    private Transform tx;
    public LineRenderer laserRenderer;

    private float leftMaxRotation;
    private float rightMaxRotation;

    public bool IsFiring { get; set; }

    void Start()
    {
        tx = transform;
        leftMaxRotation = 360 - maxRotation;
        rightMaxRotation = maxRotation;
    }

    void Update()
    {
        var wasLookingRight = tx.localEulerAngles.y >= 0 && tx.localEulerAngles.y < leftMaxRotation;
        var target = MouseTarget();

        tx.LookAt(target);

        var outOfBounds = tx.localEulerAngles.y < leftMaxRotation && tx.localEulerAngles.y > rightMaxRotation;

        if (outOfBounds && wasLookingRight)
            tx.localEulerAngles = new Vector3(0, rightMaxRotation, 0);
        else if (outOfBounds && !wasLookingRight)
            tx.localEulerAngles = new Vector3(0, leftMaxRotation, 0);

        if (IsFiring)
        {
            Fire();
        }
        else
        {
            laserRenderer.enabled = false;
        }
    }

    public void Fire()
    {
        //var bullet = Instantiate(shot, shotSpawn.transform.position, new Quaternion()).GetComponent<Rigidbody>();
        //if (bullet == null)
        //    return;

        //bullet.velocity = shotSpawn.rotation * shotSpawn.transform.position;
        laserRenderer.enabled = true;
        var target = shotSpawn.transform.forward; // MouseTarget();
        Debug.Log($"Mouse: {target}, x100: {target*100}");
        var origin = shotSpawn.transform.position;
        Debug.DrawLine(origin, target);

        laserRenderer.SetPosition(0, origin);
        var somethingFired =
            Physics.Raycast(
                origin: origin,
                direction: target,
                hitInfo: out var hit,
                maxDistance: float.PositiveInfinity);

        if (somethingFired)
        {
            laserRenderer.SetPosition(1, hit.point);
            var enemy = hit.transform.GetComponent<Enemy>();
            if (enemy == null)
                return;

            enemy.TakeDamage(1);
        }
        else
        {
            laserRenderer.SetPosition(1, target * 5000);
        }
    }

    private Vector3 MouseTarget()
    {
        var mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        var position = shotSpawn.transform.position;
        var midPoint = (position - Camera.main.transform.position).magnitude;

        var mousePos = mouseRay.origin;
        var target = mouseRay.origin + mouseRay.direction * midPoint;

        return new Vector3(target.x, position.y, target.z);
    }
}
