using UnityEngine;

[CreateAssetMenu(fileName = "RecollectionName", menuName = "ScriptableObjects/Recollection", order = 2)]
public class RecollectionNode : ScriptableObject
{
    public string _title;

    public string[] _possibleDescriptions;

    public int _sanityScore;
    public bool _isAlwaysCorrect; //if always correct, add sanity
    public bool _real;
}