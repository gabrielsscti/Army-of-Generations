using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveBirthdayPlayerPrefs : MonoBehaviour
{
    public Dropdown dropdownDay;
    public Dropdown dropdownMonth;
    public Dropdown dropdownYear;
    public InputField inputName;
    public int playerId = 1;

    public void Execute()
    {
        PlayerPrefs.SetInt("dia" + playerId, dropdownDay.value + 1);
        PlayerPrefs.SetInt("mes" + playerId, dropdownMonth.value + 1);
        PlayerPrefs.SetInt("ano" + playerId, dropdownYear.value + 1984);
        PlayerPrefs.SetString("nick" + playerId, inputName.text);
    }
}
