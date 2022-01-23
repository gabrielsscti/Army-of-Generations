using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum EnumAttributes {FOR, INT, VIT, AGI};
    [SerializeField]
    private GameObject       m_minionPrefab;
    [SerializeField]
    private GameObject player1;
    [SerializeField]
    private GameObject player2;

    private Dictionary<int, Attributes> m_dictDayAttributes;
    private Dictionary<int, Attributes> m_dictMonthAttributes;
    private Dictionary<int, Attributes> m_dictYearAttributes;
    
    private List<GameObject> m_lstMinionsPlayer1;
    private List<GameObject> m_lstMinionsPlayer2;

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

    private List<GameObject> GenerateMinions(GameObject player, int quantMinions, float x_offset = 0){
        var res = new List<GameObject>();
        player.GetComponent<PlayerController>().MinionsAlive = quantMinions;

        for (int i=0; i<quantMinions; i++){
            var minionGO = Instantiate(m_minionPrefab, new Vector2(x_offset + Random.Range(0f, 1.5f), Random.Range(-3f, 3.0f)), Quaternion.identity);
            var minion   = minionGO.GetComponent<MinionManager>();
            minion.Player = player;
            res.Add(minionGO);
        }
        return res;
    }

    private void MatchMinions(){
        int maxLen = Mathf.Max(player1.GetComponent<PlayerController>().MinionsAlive, 
                               player2.GetComponent<PlayerController>().MinionsAlive);

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
            else if (!(minion1.IsDead && !(minion2.IsDead))){
                matchCount++;
                var selectedAttribute = RandomizeAttribute();

                minion1.EffectiveStatus = player1.GetComponent<PlayerController>().GetAttribute(selectedAttribute);
                minion2.EffectiveStatus = player2.GetComponent<PlayerController>().GetAttribute(selectedAttribute);

                minion1.EnemyMinion = minion2GO;
                minion2.EnemyMinion = minion1GO;
            }
        }
    }

    void Start()
    {
        LoadAttributes();
        
        MockPlayerValues(player1.GetComponent<PlayerController>());
        MockPlayerValues(player2.GetComponent<PlayerController>());

        m_lstMinionsPlayer1 = GenerateMinions(player1, 25, 5f);
        m_lstMinionsPlayer2 = GenerateMinions(player2, 25, -5f);

        MatchMinions();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
