using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private PlayerController player1;
    [SerializeField]
    private PlayerController player2;

    private Dictionary<int, Attributes> dayAttributes;
    private Dictionary<int, Attributes> monthAttributes;
    private Dictionary<int, Attributes> yearAttributes;
    // Start is called before the first frame update

    private void LoadAttributes(){
        dayAttributes =   FileDataManager.GetAttributes(FileDataManager.DAY);
        monthAttributes = FileDataManager.GetAttributes(FileDataManager.MONTH);
        yearAttributes  = FileDataManager.GetAttributes(FileDataManager.YEAR);
    }

    void Start()
    {
        LoadAttributes();
        Attributes attributes;
        dayAttributes.TryGetValue(10, out attributes);
        Debug.Log(attributes.ToString());

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
