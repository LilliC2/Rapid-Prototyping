using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : GameBehaviour
{
    public Vector3 shootDir;
    float bulletSpeed;

    public void Setup(Vector3 _shootDir, float _bulletSpeed)
    {
        this.shootDir = _shootDir;
        this.bulletSpeed = _bulletSpeed;
        
    }

    private void Update()
    {
        transform.position += shootDir * bulletSpeed *Time.deltaTime;

        ExecuteAfterSeconds(2, () => Destroy(this));
    }
}
