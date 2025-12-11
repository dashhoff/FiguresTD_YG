/*
 *========================================================================
 *    https://github.com/dashhoff
 *    The game is made by prismatic hat studio
 *========================================================================
 */

using System.Collections;
using UnityEngine;

public enum WeaponType 
{
    Minigun,
    Shotgun,
    GrenadeLauncher,
    Laser
}

public class Weapon : MonoBehaviour
{
    public WeaponType WeaponType;
    
    public float Damage;
    public float FireRate;
    public float Radius;
    
    private void Attack()
    {
  
    }

    private IEnumerator AtackCoroutine()
    {
        yield return null;
    }
}
