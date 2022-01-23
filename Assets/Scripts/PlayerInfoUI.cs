using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoUI : MonoBehaviour
{
    [SerializeField]
    private PlayerController m_Player;
    [SerializeField]
    private Text m_TextNickname;
    [SerializeField]
    private Text m_TitleNickname;
    [SerializeField]
    private Text m_nMinionsAlive;

    private void Start(){
        m_TextNickname.text  = m_Player.Nickname;
    }

    private void Update()
    {
        m_TitleNickname.text = m_Player.GeneratedName;
        m_nMinionsAlive.text = "Minions Alive: " + m_Player.MinionsAlive.ToString();
    }
}
