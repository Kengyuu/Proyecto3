using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakPoint : MonoBehaviour
{
    // Start is called before the first frame update
    [HideInInspector]public int spawnPosition;
    public Enemy enemy;
    private GameManager GM;


    void Start()
    {
        GM = GameManager.Instance;

        //enemy = GameManager.Instance.GetEnemy().GetComponent<Enemy>();
        enemy = GM.GetEnemy().GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage()
    {
        if(GM.GetEnemy().GetComponent<HFSM_StunEnemy>().currentState != HFSM_StunEnemy.State.INVOKE)
        {
            enemy.TakeDamage(1);
            gameObject.SetActive(false);
        }
        
    }
}
