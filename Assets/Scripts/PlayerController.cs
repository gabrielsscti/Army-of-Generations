using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    private int    m_nMinionsAlive;
    [SerializeField]
    private string m_strNickname;
    private string m_strGeneratedName;
    private int    m_nFOR;
    private int    m_nINT;
    private int    m_nVIT;
    private int    m_nAGI;

    public string Nickname{
        get{ return m_strNickname; }
    }

    public string GeneratedName{
        get{ return m_strGeneratedName; }
    }

    public int MinionsAlive{
        get { return m_nMinionsAlive; }
        set { m_nMinionsAlive = value; }
    }

    void Awake(){
        m_nFOR          = 0;
        m_nINT          = 0;
        m_nVIT          = 0;
        m_nAGI          = 0;  
        m_nMinionsAlive = 10;
    }

    public void AddAttribute(Attributes soAttribute){
        m_strGeneratedName += " " + soAttribute.AttributeName;
        m_nFOR += soAttribute.FOR;
        m_nINT += soAttribute.INT;
        m_nVIT += soAttribute.VIT;
        m_nAGI += soAttribute.AGI;
    }

    public void KillMinion(){
        m_nMinionsAlive--;
        Debug.Log(Nickname + " lost one");
    }

    public int GetAttribute(GameManager.EnumAttributes attribute){
        switch (attribute){
            case GameManager.EnumAttributes.FOR:
                return m_nFOR;
            case GameManager.EnumAttributes.INT:
                return m_nINT;
            case GameManager.EnumAttributes.VIT:
                return m_nVIT;
            case GameManager.EnumAttributes.AGI:
                return m_nAGI;
            default:
                return -1;
        }
    }

    void Start(){
        m_strGeneratedName = "";
    }

}
