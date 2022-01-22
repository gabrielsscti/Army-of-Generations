using UnityEngine;

[CreateAssetMenu(fileName = "PlayerAttribute", menuName = "ScriptableObjects/Player Attribute", order = 1)]
public class SOAttributes : ScriptableObject
{
    public string AttributeName;
    public int    FOR;
    public int    INT;
    public int    VIT;
    public int    AGI;
}
