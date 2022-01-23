using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    public enum EnumAttributes {FOR, INT, VIT, AGI};
    [SerializeField]
    private GameObject       m_minionPrefabBlue;
    [SerializeField]
    private GameObject       m_minionPrefabRed;
    [SerializeField]
    private GameObject m_GOPlayer1;
    [SerializeField]
    private GameObject m_GOPlayer2;

    private Dictionary<int, Attributes> m_dictDayAttributes;
    private Dictionary<int, Attributes> m_dictMonthAttributes;
    private Dictionary<int, Attributes> m_dictYearAttributes;
    
    private List<GameObject> m_lstMinionsPlayer1;
    private List<GameObject> m_lstMinionsPlayer2;
    private bool             m_bIsWaiting;
    private int              m_nCountRound;
    private bool             m_bGameEnd;

    public Text OverlayNickname;
    public Text OverlayTitle;
    public Text OverlayMinions;
    public GameObject UIScreen;
    public GameObject OverlayScreen;

    public int CountRound{
        get{ return m_nCountRound; }
    }

    private void LoadAttributes(){
        m_dictDayAttributes   = FileDataManager.GetAttributes(FileDataManager.DAY);
        m_dictMonthAttributes = FileDataManager.GetAttributes(FileDataManager.MONTH);
        m_dictYearAttributes  = FileDataManager.GetAttributes(FileDataManager.YEAR);
    }

    private EnumAttributes RandomizeAttribute(){
        var randomInt = Random.Range(0, 4);
        return (EnumAttributes)randomInt;
    }

    private void MockPlayerValues(PlayerController player){
        Attributes dayAttribute;
        Attributes monthAttribute;
        Attributes yearAttribute;
        m_dictDayAttributes.TryGetValue(Random.Range(1, 32), out dayAttribute);
        m_dictMonthAttributes.TryGetValue(Random.Range(1, 13), out monthAttribute);
        m_dictYearAttributes.TryGetValue(Random.Range(1984, 2017), out yearAttribute);
        
        player.AddAttribute(dayAttribute);
        player.AddAttribute(monthAttribute);
        player.AddAttribute(yearAttribute);
        Debug.Log("Generated " + player.GeneratedName);
    }

    private void GetPlayer1Values(PlayerController player){
        Attributes dayAttribute;
        Attributes monthAttribute;
        Attributes yearAttribute;

        m_dictDayAttributes.TryGetValue(PlayerPrefs.GetInt("dia1"), out dayAttribute);
        m_dictMonthAttributes.TryGetValue(PlayerPrefs.GetInt("mes1"), out monthAttribute);
        m_dictYearAttributes.TryGetValue(PlayerPrefs.GetInt("ano1"), out yearAttribute);
        player.Nickname = PlayerPrefs.GetString("nick1", "Player One");
        
        player.AddAttribute(dayAttribute);
        player.AddAttribute(monthAttribute);
        player.AddAttribute(yearAttribute);
        Debug.Log("Generated " + player.GeneratedName);
    }

    private void GetPlayer2Values(PlayerController player){
        Attributes dayAttribute;
        Attributes monthAttribute;
        Attributes yearAttribute;
        
        m_dictDayAttributes.TryGetValue(PlayerPrefs.GetInt("dia2"), out dayAttribute);
        m_dictMonthAttributes.TryGetValue(PlayerPrefs.GetInt("mes2"), out monthAttribute);
        m_dictYearAttributes.TryGetValue(PlayerPrefs.GetInt("ano2"), out yearAttribute);
        player.Nickname = PlayerPrefs.GetString("nick2", "Player Two");
        
        player.AddAttribute(dayAttribute);
        player.AddAttribute(monthAttribute);
        player.AddAttribute(yearAttribute);
        Debug.Log("Generated " + player.GeneratedName);
    }

    private List<GameObject> GenerateMinionsBlue(GameObject player, int quantMinions, float x_offset = 0){
        var res = new List<GameObject>();
        player.GetComponent<PlayerController>().MinionsAlive = quantMinions;

        for (int i=0; i<quantMinions; i++){
            var minionGO = Instantiate(m_minionPrefabBlue, new Vector2(x_offset + Random.Range(-2.5f, 5.0f), Random.Range(-7f, 4.0f)), Quaternion.identity);
            minionGO.SetActive(false);
            var minion   = minionGO.GetComponent<MinionManager>();
            minion.Player = player;
            res.Add(minionGO);
        }
        return res;
    }

    private List<GameObject> GenerateMinionsRed(GameObject player, int quantMinions, float x_offset = 0){
        var res = new List<GameObject>();
        player.GetComponent<PlayerController>().MinionsAlive = quantMinions;

        for (int i=0; i<quantMinions; i++){
            var minionGO = Instantiate(m_minionPrefabRed, new Vector2(x_offset + Random.Range(-2.5f, 5.0f), Random.Range(-7f, 4.0f)), Quaternion.identity);
            minionGO.SetActive(false);
            var minion   = minionGO.GetComponent<MinionManager>();
            minion.Player = player;
            res.Add(minionGO);
        }
        return res;
    }

    private void MatchMinions(){
        int maxLen = Mathf.Min(m_GOPlayer1.GetComponent<PlayerController>().MinionsAlive, 
                               m_GOPlayer2.GetComponent<PlayerController>().MinionsAlive);
        Debug.Log(maxLen);
        int matchCount = 0;
        for (int i=0, j=0; matchCount<=maxLen && i < m_lstMinionsPlayer1.Count && j < m_lstMinionsPlayer2.Count; i++, j++){
            var minion1GO = m_lstMinionsPlayer1[i];
            var minion2GO = m_lstMinionsPlayer2[j];
            var minion1   = minion1GO.GetComponent<MinionManager>();
            var minion2   = minion2GO.GetComponent<MinionManager>();

            if (!(minion1.IsDead) && minion2.IsDead)
                i--;
            else if (minion1.IsDead && !(minion2.IsDead))
                j--;
            else if (!(minion1.IsDead) && !(minion2.IsDead)){
                matchCount++;
                var selectedAttribute = RandomizeAttribute();

                minion1.EffectiveStatus = m_GOPlayer1.GetComponent<PlayerController>().GetAttribute(selectedAttribute);
                minion2.EffectiveStatus = m_GOPlayer2.GetComponent<PlayerController>().GetAttribute(selectedAttribute);

                minion1.EnemyMinion = minion2GO;
                minion2.EnemyMinion = minion1GO;
            }
        }
        m_bIsWaiting = false;
    }

    private void ShowMinions(){
        for (int i=0; i<m_lstMinionsPlayer1.Count; i++){
            var minion1GO = m_lstMinionsPlayer1[i];
            var minion2GO = m_lstMinionsPlayer2[i];
            var minion1   = minion1GO.GetComponent<MinionManager>();
            var minion2   = minion2GO.GetComponent<MinionManager>();
            minion1GO.SetActive(!(minion1.IsDead));
            minion2GO.SetActive(!(minion2.IsDead)); 
        }
    }

    void Start()
    {
        m_bIsWaiting  = false;
        m_bGameEnd    = false;
        m_nCountRound = 1;

        LoadAttributes();
        
        GetPlayer1Values(m_GOPlayer1.GetComponent<PlayerController>());
        GetPlayer2Values(m_GOPlayer2.GetComponent<PlayerController>());

        // MockPlayerValues(m_GOPlayer1.GetComponent<PlayerController>());
        // MockPlayerValues(m_GOPlayer2.GetComponent<PlayerController>());

        m_lstMinionsPlayer1 = GenerateMinionsBlue(m_GOPlayer1, 20, 5f);
        m_lstMinionsPlayer2 = GenerateMinionsRed(m_GOPlayer2, 20, -7f);

        ShowMinions();
        m_bIsWaiting = true;
        Invoke("MatchMinions", 1f);
    }

    bool EveryoneDueled(){
        for (int i=0; i<m_lstMinionsPlayer1.Count; i++){
            var minion1 = m_lstMinionsPlayer1[i].GetComponent<MinionManager>();
            var minion2 = m_lstMinionsPlayer2[i].GetComponent<MinionManager>();
            if((minion1.EnemyMinion != null && !(minion1.HasDueled)) || (minion2.EnemyMinion != null && !(minion2.HasDueled)))
                return false;
        }
        return true;
    }
    void PrepareNextRound(){
        var player1 = m_GOPlayer1.GetComponent<PlayerController>();
        var player2 = m_GOPlayer2.GetComponent<PlayerController>();

        if (player1.MinionsAlive==0){
            m_bGameEnd = true;
            OverlayNickname.text = player2.Nickname + " é o vencedor!";
            OverlayTitle.text =    " seu exército é " + player2.GeneratedName;
            OverlayMinions.text =  " e sobreviveram " + player2.MinionsAlive.ToString() + " soldados!";
        }else if(player2.MinionsAlive==0){
            m_bGameEnd = true;
            OverlayNickname.text = player1.Nickname + " é o vencedor!";
            OverlayTitle.text =    " seu exército é " + player1.GeneratedName;
            OverlayMinions.text =  " e sobreviveram " + player1.MinionsAlive.ToString() + " soldados";
        }else{
            m_nCountRound++;
            m_bIsWaiting = false;
            for (int i=0; i<m_lstMinionsPlayer1.Count; i++){
                var minion1GO = m_lstMinionsPlayer1[i];
                var minion2GO = m_lstMinionsPlayer2[i];
                var minion1 = minion1GO.GetComponent<MinionManager>();
                var minion2 = minion2GO.GetComponent<MinionManager>();

                minion1GO.transform.position = new Vector2(5 + Random.Range(-2.5f, 5.0f), Random.Range(-7f, 4.0f));
                minion2GO.transform.position = new Vector2(-7 + Random.Range(-2.5f, 5.0f), Random.Range(-7f, 4.0f));

                if (!(minion1.IsDead))
                    minion1.SetIdle();
                if (!(minion2.IsDead))
                    minion2.SetIdle();

                if (!(minion1.IsDead))
                    minion1.HasDueled = false;
                if (!(minion2.IsDead))
                    minion2.HasDueled = false;

                minion1.EnemyMinion = null;
                minion2.EnemyMinion = null;
            }
            m_bIsWaiting = true;
            Invoke("MatchMinions", 1f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_bGameEnd){
            UIScreen.SetActive(true);
            OverlayScreen.SetActive(false);
            if (!(m_bIsWaiting) && EveryoneDueled()){
                m_bIsWaiting = true;
                
                Invoke("PrepareNextRound", 3f);
            }
        }else{
            OverlayScreen.SetActive(true);
            UIScreen.SetActive(false);
        }
    }
}
