using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakPoint : MonoBehaviour
{
    [HideInInspector]public int spawnPosition;
    public Enemy enemy;
    private GameManager GM;
    public GameObject particles;

    [Header("FMOD Events")]
    public string banishEvent;

    void Start()
    {
        GM = GameManager.Instance;
        enemy = GM.GetEnemy().GetComponent<Enemy>();
    }
    public void TakeDamage()
    {
        if(GM.GetEnemy().GetComponent<HFSM_StunEnemy>().currentState != HFSM_StunEnemy.State.INVOKE)
        {
            SoundManager.Instance.PlaySound(banishEvent, transform.position);
            enemy.TakeDamage(1);
            gameObject.SetActive(false);
            particles.SetActive(false);
        }
        
    }
}
