using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum Attributes {FOR, INT, VIT, AGI};
    
    [SerializeField]
    private int    m_nMinionsAlive;
    private string m_strNickname;
    private string m_strGeneratedName;
    private int    m_nFOR;
    private int    m_nINT;
    private int    m_nVIT;
    private int    m_nAGI;

    void Awake(){
        m_nFOR = 0;
        m_nINT = 0;
        m_nVIT = 0;
        m_nAGI = 0;  
    }

    public void AddAttribute(SOAttributes soAttribute){
        m_nFOR += soAttribute.FOR;
        m_nINT += soAttribute.INT;
        m_nVIT += soAttribute.VIT;
        m_nAGI += soAttribute.AGI;
    }

    public int MinionsAlive {
        get { return m_nMinionsAlive; }
    }

    public void KillMinion(){
        m_nMinionsAlive--;
    }

    public int GetAttribute(Attributes attribute){
        switch (attribute){
            case Attributes.FOR:
                return m_nFOR;
            case Attributes.INT:
                return m_nINT;
            case Attributes.VIT:
                return m_nVIT;
            case Attributes.AGI:
                return m_nAGI;
            default:
                return -1;
        }
    }

}
