using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityController : MonoBehaviour
{
    [Header("Stats")]
    public float m_Life = 0.0f;
    public float m_MaxLife = 0.0f;
    public float m_Shield = 0.0f;
    public float m_MaxShield = 0.0f;
    public float m_Ammo = 0.0f;
    public float m_MaxAmmo = 0.0f;

    public virtual void Die()
    {
        //Must (or not) be overriden by child classes
    }
}
