/*
 *========================================================================
 *    https://github.com/dashhoff
 *    The game is made by prismatic hat studio
 *========================================================================
 */

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
        Vector3 _dir = (_targetTransform.position - transform.position).normalized;
        transform.position += _dir * _speed * Time.deltaTime;
        
        float _dist = Vector3.Distance(transform.position, _targetTransform.position);
        if (_dist < 2f)
        {
            _isMoving = false;
            Tower.Instance.TakeDamage(_damage);
            
            if (EnemyType == EnemyType.Kamikaze)
                Destroy(gameObject);
        }
    }
    
    private void RotationToTower()
    {
        Vector3 direction = (_targetTransform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 0.1f);
    }
    
    public void TakeDamage(float damage)
    {
        _hp -= Mathf.RoundToInt(damage);
        if (_hp <= 0) Death();
    }

    private void Death()
    {
        Debug.Log("DeathEnemy");
        
        EventBus.InvokeDeathEnemy();
        
        if (EnemyType == EnemyType.Boss)
            EventBus.InvokeDeathBoss();
    }
}
