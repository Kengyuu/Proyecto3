using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOrbController : MonoBehaviour
{
    public int maxOrbHealth = 3;
    public int currentOrbHealth;

    void Start()
    {
        SetOrbHealth(maxOrbHealth);
    }

    public void SetOrbHealth(int health)
    {
        currentOrbHealth += health;
    }

    public int GetOrbHealth()
    {
        return currentOrbHealth;
    }

    public virtual void TakeDamage(int damage)
    {
        SetOrbHealth(damage);
        
    }
}
