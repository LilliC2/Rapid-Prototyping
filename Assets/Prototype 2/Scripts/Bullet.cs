using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : GameBehaviour
{
    public Vector3 shootDir;
    public float bulletSpeed;
    Rigidbody rb;

    public void Setup(Vector3 _shootDir, float _bulletSpeed)
    {
        this.shootDir = _shootDir;
        this.bulletSpeed = _bulletSpeed;
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        rb.velocity = transform.forward* bulletSpeed;

        ExecuteAfterSeconds(2, () => Destroy(this));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Walls"))
        {
            Destroy(this);
        }
    }
}
