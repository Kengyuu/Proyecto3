using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOrbController : MonoBehaviour
{
    public int maxOrbHealth = 3;
    public int currentOrbHealth;

    // Start is called before the first frame update
    void Start()
    {
        SetOrbHealth(maxOrbHealth);
    }

    // Update is called once per frame
    void Update()
    {
        
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
