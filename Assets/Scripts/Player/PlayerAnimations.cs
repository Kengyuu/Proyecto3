using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    [Header("Animator")]
    public Animator m_Animator;

    [Header("References")]
    private PlayerShoot m_PlayerShoot;
    private PlayerSpecialAbilities m_PlayerAbility;


    private string m_ShootTarget;
    private float m_CurrentDistance;
    private RaycastHit m_hit;

    private void Start()
    {
        m_PlayerShoot = GameManager.Instance.GetPlayer().GetComponent<PlayerShoot>();
        m_PlayerAbility = GameManager.Instance.GetPlayer().GetComponent<PlayerSpecialAbilities>();
    }

    public void StartShoot(float l_CurrentDistance, string tag, RaycastHit hit)
    {
        m_Animator.SetTrigger("Shoot");
        m_ShootTarget = tag;
        m_CurrentDistance = l_CurrentDistance;
        m_hit = hit;
    }



    public void ShootBeam()
    {
        m_PlayerShoot.ShootBeam(m_CurrentDistance, m_ShootTarget, m_hit);

    }

    public void ResetShoot()
    {
        Debug.Log("Llamo al resetshoot");
        /*m_ShootTarget = "";
        m_CurrentDistance = 0f;
        hit =;*/
        m_PlayerShoot.ResetShoot();
    }

    public void StopAbsorb()
    {
        m_Animator.SetBool("Absorb", false);
        m_PlayerShoot.CorpseAbsorbEnd();
        ResetShoot();
    }

    public void StartAbsorb()
    {
        m_Animator.SetBool("Absorb", true);
    }

    public void StartStealth()
    {
        m_Animator.SetTrigger("Invisible");
    }


}
