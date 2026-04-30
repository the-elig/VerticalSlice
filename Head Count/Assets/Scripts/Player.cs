using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private TMP_Text _factText;
    [SerializeField] private TMP_Text _recollectionText;
    [SerializeField] private TMP_Text _sanityMeterText;

    [SerializeField] private Camera _camera;


public float _sanityMeter;

    //journal stuff
    public List<string> _facts;
    public List<string> _recollections;


    //scene switching
    [SerializeField] public Camera[] _cameraPositions; //puts camera in different scenes
    public int currentScene; //0 means no change, 1 is therapist's office


    // Start is called before the first frame update
    void Start()
    {
        currentScene = 1;
        _sanityMeter = 75;
    }

    // Update is called once per frame
    void Update()
    {
        if (_sanityMeter > 100)
        {
            _sanityMeter = 100;
        }

        _sanityMeterText.text = _sanityMeter.ToString();
    }

    public void checkForSceneChange(DialogueNode node)
    {
        Debug.Log("old scene = " + currentScene + ", current = " + node._scene);

        if (!(node._scene == 0 || node._scene == currentScene)) // if scene changes
        {
            Debug.Log("scene switch triggered");

            _cameraPositions[currentScene].tag = "Untagged";
            _cameraPositions[currentScene].gameObject.SetActive(false);

            currentScene = node._scene;
            _cameraPositions[currentScene].tag = "MainCamera";
            _cameraPositions[currentScene].gameObject.SetActive(true);

        }
    }

    public void addFact(RecollectionNode fact, int selected)
    {
        _facts.Add(fact._title + ": " + fact._possibleDescriptions[selected]); // add title and proper description to list
        
        _factText.text = ""; // clear text
        foreach (string s in _facts)
        {
            Debug.Log(s);

            string header = s.Substring(0, s.IndexOf(": "));
            string description = s.Substring(s.IndexOf(": ") + 1);

            _factText.text += $"<b>{header}</b>\n{description}\n\n"; // put header and description into journal
        }

        modifySanity(fact, 0); //always selects true
    }


    public void addRecollection(RecollectionNode recollection, int selected)
    {
        _recollections.Add(recollection._title + ": " + recollection._possibleDescriptions[selected]); // add title and proper description to list

        _recollectionText.text = ""; // clear text
        foreach (string s in _recollections)
        {
            Debug.Log(s);

            string header = s.Substring(0, s.IndexOf(": "));
            string description = s.Substring(s.IndexOf(": ") + 1);

            _recollectionText.text += $"<b>{header}</b>\n{description}\n\n"; // put header and description into journal

            // later i will implement instantiating prefabs for the journal but not yet
        }

        modifySanity(recollection, selected);
    }


    private void modifySanity(RecollectionNode recollection, int selected)
    {
        int correctSorting = recollection._real ? 0: 1; // real = 0 because of indexing reasons

        Debug.Log("correct sorting = " + correctSorting + ", selected = " + selected);

        if (selected == correctSorting) 
        {
            _sanityMeter += recollection.sanityScore;
        }
        else
        {
            _sanityMeter -= recollection.sanityScore;
        }

        Debug.Log(_sanityMeter);


    }
}
