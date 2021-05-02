using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entities : MonoBehaviour
{
    protected int m_Life;
    protected int maxLife = 3;
    protected float stunTime;
    public virtual void TakeDamage(int dmg)
    {

    }
    protected virtual void RestoreLife()
    {
        
    }
    public virtual void GetStunned()
    {

    }

    public virtual int GetLife()
    {
        return m_Life;
    }
}
