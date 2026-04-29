using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private TMP_Text _journalText;

    public float _santityMeter;
    public List<string> _facts;
    // [SerializeField] private Recollection[] _journal;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void addFact(RecollectionNode fact, int selected)
    {
        _facts.Add(fact._title + ": " + fact._possibleDescriptions[selected]); // add title and proper description to list
        
        _journalText.text = ""; // clear text
        foreach (string s in _facts)
        {
            Debug.Log(s);

            string header = s.Substring(0, s.IndexOf(": "));
            string description = s.Substring(s.IndexOf(": ") + 1);

            _journalText.text += $"<b>{header}</b>\n{description}\n\n"; // put header and description into journal
        }
    }
}
