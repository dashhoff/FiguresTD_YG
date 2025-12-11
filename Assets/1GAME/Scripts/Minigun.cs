/*
 *========================================================================
 *    https://github.com/dashhoff
 *    The game is made by prismatic hat studio
 *========================================================================
 */

using System.Collections;
using UnityEngine;

public class Minigun : Weapon
{
    public Bullet BulletPrefab;
    public float BulletSpeed;
    [SerializeField] private Transform _shootPoint;
    
    private Transform _target;
    private bool _isShooting;
    
    private void Update()
    {
        if (!_isShooting)
            StartCoroutine(AttackCoroutine());
    }

    private IEnumerator AttackCoroutine()
    {
        _isShooting = true;

        FindTarget();

        if (_target != null)
            Shoot();

        yield return new WaitForSeconds(FireRate);
        _isShooting = false;
    }

    private void FindTarget()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();

        float minDist = Mathf.Infinity;
        Transform closest = null;

        foreach (var enemy in enemies)
        {
            if (!enemy.Alive) 
                continue;

            float dist = Vector3.Distance(transform.position, enemy.transform.position);

            if (dist <= Radius && dist < minDist)
            {
                minDist = dist;
                closest = enemy.transform;
            }
        }

        _target = closest;
    }

    private void Shoot()
    {
        Bullet bullet = Instantiate(BulletPrefab, _shootPoint.position, Quaternion.identity);
        bullet.Init(_target, Damage, BulletSpeed);
    }
}
