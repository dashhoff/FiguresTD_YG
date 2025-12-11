/*
 *========================================================================
 *    https://github.com/dashhoff
 *    The game is made by prismatic hat studio
 *========================================================================
 */

using System.Collections;
using DG.Tweening;
using UnityEngine;

public enum EnemyType
{
    Kamikaze,
    Normal,
    Fast,
    Tank,
    Boss
}

public class Enemy : MonoBehaviour, IDamageable
{
    [Header("Type")]
    public EnemyType EnemyType;
    
    [Header("HP")]
    public bool Alive = true;
    [SerializeField] private int _hp;
    [SerializeField] private int _maxHp;

    [Header("3D")]
    [SerializeField] private GameObject _model;

    [Header("Main")]
    [SerializeField] private float _damage;
    [SerializeField] private float _speed;

    [Header("Other")]
    [SerializeField] private Transform _targetTransform;

    [SerializeField] private bool _isMoving = true;

    public void Init()
    {
        _targetTransform = Tower.Instance.Model.transform;
        
        RotationToTower();
    }

    private void Update()
    {
        if (_isMoving)
            Movement();
    }

    private void Movement()
    {
        if (!Alive) return;
        
        Vector3 _dir = (_targetTransform.position - transform.position).normalized;
        transform.position += _dir * _speed * Time.deltaTime;
        
        float _dist = Vector3.Distance(transform.position, _targetTransform.position);
        if (_dist < 2f)
        {
            _isMoving = false;
            Attack();
        }
    }
    
    private void RotationToTower()
    {
        if (!Alive) return;
        
        Vector3 direction = (_targetTransform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 0.1f);
    }
    
    public void TakeDamage(float damage)
    {
        if (!Alive) return;
        
        _hp -= Mathf.RoundToInt(damage);
        if (_hp <= 0) Death();
    }

    private void Attack()
    {
        if (!Alive) return;

        StartCoroutine(AttackCoroutine());
    }

    private IEnumerator AttackCoroutine()
    {
        while (Alive)
        {
            Tower.Instance.TakeDamage(_damage);

            if (EnemyType == EnemyType.Kamikaze)
            {
                Death();
                yield break;
            }
            
            yield return new WaitForSecondsRealtime(2f);
        }
    }

    private void Death()
    {
        if (!Alive) return;
        
        Alive = false;
        
        Debug.Log("DeathEnemy");
        
        EventBus.InvokeDeathEnemy();
        
        if (EnemyType == EnemyType.Boss)
            EventBus.InvokeDeathBoss();
        
        DeathAnimation();
    }

    private void DeathAnimation()
    {
        Sequence deathSequence = DOTween.Sequence();

        deathSequence
            .Append(gameObject.transform.DOScale(new Vector3(
                gameObject.transform.localScale.x * 1.1f,
                gameObject.transform.localScale.y * 1.1f,
                gameObject.transform.localScale.z * 1.1f
            ), 0.2f))
            .Append(gameObject.transform.DOScale(Vector3.zero, 0.1f))
            .OnComplete(() =>
            {
                Destroy(gameObject);
            });
    }
}
