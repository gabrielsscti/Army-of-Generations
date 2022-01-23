using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject       m_minionPrefab;
    [SerializeField]
    private GameObject player1;
    [SerializeField]
    private GameObject player2;

    private Dictionary<int, Attributes> m_dictDayAttributes;
    private Dictionary<int, Attributes> m_dictMonthAttributes;
    private Dictionary<int, Attributes> m_dictYearAttributes;
    // Start is called before the first frame update

    private void LoadAttributes(){
        m_dictDayAttributes   = FileDataManager.GetAttributes(FileDataManager.DAY);
        m_dictMonthAttributes = FileDataManager.GetAttributes(FileDataManager.MONTH);
        m_dictYearAttributes  = FileDataManager.GetAttributes(FileDataManager.YEAR);
    }

    void Start()
    {
        LoadAttributes();
        float[] y_pos = {-2f, -1f, 0f, 1f, 2f};
        foreach (float y in y_pos){
            var minion1go = Instantiate(m_minionPrefab, new Vector3(5, y, 0), Quaternion.identity);
            var minion2go = Instantiate(m_minionPrefab, new Vector3(-5, y, 0), Quaternion.identity);
            var minion1   = minion1go.GetComponent<MinionManager>();
            var minion2   = minion2go.GetComponent<MinionManager>();

            minion1.EffectiveStatus = 10;
            minion2.EffectiveStatus = 5;

            minion1.Player      = player1;
            minion1.EnemyMinion = minion2go;
            minion2.Player      = player2;
            minion2.EnemyMinion = minion1go;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
