using UnityEngine;
using UnityEngine.UI;

public class RoundTextScript : MonoBehaviour
{
    void Update()
    {
        GetComponent<Text>().text = GameManager.Instance.CountRound.ToString();
    }
}
