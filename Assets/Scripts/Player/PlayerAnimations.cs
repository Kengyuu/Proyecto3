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

    private void Start()
    {
        m_PlayerShoot = GameManager.Instance.GetPlayer().GetComponent<PlayerShoot>();
        m_PlayerAbility = GameManager.Instance.GetPlayer().GetComponent<PlayerSpecialAbilities>();
    }

    public void StartShoot()
    {
        m_Animator.SetTrigger("Shoot");
    }



    public void ShootBeam()
    {
        m_PlayerShoot.ShootBeam();

    }

    public void ResetShoot()
    {
        //Debug.Log("Llamo al resetshoot");
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
