using UnityEngine;

[CreateAssetMenu(fileName = "PlayerAttribute", menuName = "ScriptableObjects/Player Attribute", order = 1)]
public class SOAttributes : ScriptableObject
{
    public string m_strAttributeName;
    public int    m_nFOR;
    public int    m_nINT;
    public int    m_nVIT;
    public int    m_nAGI;
}
