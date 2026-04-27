using UnityEngine;

[CreateAssetMenu(fileName = "RecollectionName", menuName = "ScriptableObjects/Recollection", order = 2)]
public class RecollectionNode : ScriptableObject
{
    public string _title;

    public string[] _possibleDescriptions;

    public bool _real;
}