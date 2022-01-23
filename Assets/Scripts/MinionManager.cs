using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionManager : MonoBehaviour
{
    private GameObject m_Player;
    private GameObject m_GOEnemyMinion;
    [SerializeField]
    private float      m_sSpeed;
    [SerializeField]
    private float      m_sAttackingDistance;
    private int        m_nEffectiveStatus;
    private bool       m_bHasDueled;
    private bool       m_bIsDead;

    public GameObject EnemyMinion{
        set { m_GOEnemyMinion = value; }
    }

    public int EffectiveStatus{
        get { return m_nEffectiveStatus; }
        set { m_nEffectiveStatus = value; }
    }

    public bool HasDueled{
        get { return m_bHasDueled; }
    }

    public bool IsDead{
        get { return m_bIsDead; }
    }

    public GameObject Player{
        get { return m_Player; }
        set { m_Player = value; }
    }

    void MoveTo(Transform target){
        transform.position = Vector2.MoveTowards(transform.position, m_GOEnemyMinion.transform.position, m_sSpeed * Time.deltaTime);
    }

    bool IsInAttackingDistance(Transform target){
        var distance = Vector2.Distance(transform.position, target.position);
        return (distance < m_sAttackingDistance);
    }

    void Duel(MinionManager EnemyMinion){
        m_bHasDueled = true;
        if (m_nEffectiveStatus < EnemyMinion.EffectiveStatus){
            m_bIsDead = true;
            Player.GetComponent<PlayerController>().KillMinion();
            var player1 = Player.GetComponent<PlayerController>();
            var player2 = m_GOEnemyMinion.GetComponent<MinionManager>().Player.GetComponent<PlayerController>();
        }
    }

    void Update(){
        if (!m_bIsDead){
            if (IsInAttackingDistance(m_GOEnemyMinion.transform))
                Duel(m_GOEnemyMinion.GetComponent<MinionManager>());
            else
                MoveTo(m_GOEnemyMinion.transform);
        }
    }

}
