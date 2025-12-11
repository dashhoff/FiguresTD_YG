/*
 *========================================================================
 *    https://github.com/dashhoff
 *    The game is made by prismatic hat studio
 *========================================================================
 */

using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform _target;
    private float _damage;

    private float _speed = 40f;

    public void Init(Transform target, float damage, float speed)
    {
        _target = target;
        _damage = damage;
        _speed = speed;
    }

    private void Update()
    {
        if (_target == null)
        {
            Destroy(gameObject);
            return;
        }

        transform.position = Vector3.MoveTowards(
            transform.position,
            _target.position,
            _speed * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, _target.position) < 0.05f)
        {
            if (_target.TryGetComponent<IDamageable>(out IDamageable dmg))
                dmg.TakeDamage(_damage);

            Destroy(gameObject);
        }
    }
}
