using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionManager : MonoBehaviour
{
    private GameObject m_Player;
    private Animator animator;
    private GameObject m_GOEnemyMinion;
    [SerializeField]
    private float      m_sSpeed;
    [SerializeField]
    private float      m_sAttackingDistance;
    private int        m_nEffectiveStatus;
    private bool       m_bHasDueled;
    private bool       m_bIsDead;
    [SerializeField]
    private AudioClip[] atkSounds;

    public GameObject EnemyMinion{
        get { return m_GOEnemyMinion; }
        set { m_GOEnemyMinion = value; }
    }

    public int EffectiveStatus{
        get { return m_nEffectiveStatus; }
        set { m_nEffectiveStatus = value; }
    }

    public bool HasDueled{
        get { return m_bHasDueled; }
        set { m_bHasDueled = value; }
    }

    public bool IsDead{
        get { return m_bIsDead; }
    }

    public GameObject Player{
        get { return m_Player; }
        set { m_Player = value; }
    }

    public bool DoesWin(MinionManager EnemyMinion){
        if(m_nEffectiveStatus == EnemyMinion.EffectiveStatus){
            var coin_toss = Random.Range(1, 3);
            if (coin_toss == 1)
                m_nEffectiveStatus++;
            else
                EnemyMinion.EffectiveStatus++;
        }

        return (m_nEffectiveStatus < EnemyMinion.EffectiveStatus);
    }

    void MoveTo(Transform target){
        animator.SetTrigger("Walk");
        transform.position = Vector2.MoveTowards(transform.position, m_GOEnemyMinion.transform.position, m_sSpeed * Time.deltaTime);
    }

    bool IsInAttackingDistance(Transform target){
        var distance = Vector2.Distance(transform.position, target.position);
        return (distance < m_sAttackingDistance);
    }

    void DoHurtAnimation(){
        animator.SetTrigger("Hurt");
    }

    void DoDeathAnimation(){
        animator.SetTrigger("Die");
    }

    void Duel(MinionManager EnemyMinion){
        if(!m_bHasDueled){
            animator.SetTrigger("Attack");
            PlayAttackSound();

            Invoke("DoHurtAnimation", 0.24f);
        }
        m_bHasDueled = true;

        if(!DoesWin(EnemyMinion))
            Die();
        
        
    }

    public void SetIdle(){
        animator.SetTrigger("Idle");
    }

    void Die(){
        if (!m_bIsDead)
            Invoke("DoDeathAnimation", 0.24f+0.21f);
        m_bIsDead = true;
        Player.GetComponent<PlayerController>().KillMinion();
    }

    void Start(){
        animator = GetComponent<Animator>();
        animator.SetTrigger("Idle");
    }

    void Update(){
        if (!m_bIsDead){
            if (m_GOEnemyMinion != null){
                if (IsInAttackingDistance(m_GOEnemyMinion.transform))
                    Duel(m_GOEnemyMinion.GetComponent<MinionManager>());
                else
                MoveTo(m_GOEnemyMinion.transform);
            }
        }
    }

    void PlayAttackSound(){
        TocarSom.Tocar(this.gameObject, atkSounds[Random.Range(0, atkSounds.Length-1)]);
    }

}
