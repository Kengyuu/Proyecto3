using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityController : MonoBehaviour
{
    [Header("Stats")]
    [Range(0, 3)] public int m_Life = 0;
    [Range(0, 3)] public int m_MaxLife = 3;

    public virtual void Die()
    {
        //Must (or not) be overriden by child classes
    }
}
