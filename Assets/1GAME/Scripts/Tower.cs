/*
 *========================================================================
 *    https://github.com/dashhoff
 *    The game is made by prismatic hat studio
 *========================================================================
 */

using System;
using UnityEngine;

public class Tower : MonoBehaviour, IDamageable
{
    public static Tower Instance;

    [Header("Main")] 
    [SerializeField] private bool _alive = true;
    
    [Header("HP")]
    [SerializeField] private float _hp;
    [SerializeField] private float _maxHp;
    
    [SerializeField] private float _regenerationStrength;
    [SerializeField] private float _regenerationRate;
    
    [Header("3D")]
    public GameObject Model;
    
    [Header("Particles")]
    [SerializeField] private ParticleSystem _deathParticle;
    
    public static event Action OnTakeDamage;
    
    private float regenerationTimer;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void Init()
    {
        
    }
    
    private void Update()
    {
        regenerationTimer += Time.deltaTime;
        if (regenerationTimer >= 1 / _regenerationRate) Regeneration();
    }
    
    public void TakeDamage(float damage)
    {
        _hp -= Mathf.RoundToInt(damage);
        
        OnTakeDamage?.Invoke();
        
        if (_hp <= 0 && _alive) Death();
        
        Debug.Log("WaveController: TakeDamage");
    }

    private void Regeneration()
    {
        _hp += _regenerationStrength;
        if (_hp > _maxHp) _hp = _maxHp;
    }

    private void Death()
    {
        if (_deathParticle != null) _deathParticle.Play();

        _alive = false;
        
        EventBus.InvokeDeath();
        
        Debug.Log("Tower: Death");
    }
}
