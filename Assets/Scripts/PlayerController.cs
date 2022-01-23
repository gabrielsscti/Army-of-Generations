using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum EnumAttributes {FOR, INT, VIT, AGI};
    
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

    public void AddAttribute(Attributes soAttribute){
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

    public int GetAttribute(EnumAttributes attribute){
        switch (attribute){
            case EnumAttributes.FOR:
                return m_nFOR;
            case EnumAttributes.INT:
                return m_nINT;
            case EnumAttributes.VIT:
                return m_nVIT;
            case EnumAttributes.AGI:
                return m_nAGI;
            default:
                return -1;
        }
    }

}
