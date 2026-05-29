using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    //[SerializeField] private TMP_Text _recollectionText;


    //sanity stuff
    [SerializeField] private TMP_Text _sanityMeterText;
    public float _sanityMeter;


    //journal stuff
    [SerializeField] private GameObject _parent;
    [SerializeField] private GameObject _journalPagePrefab;
    private AotList _journalPages;
    private int numPages = 0;
    private GameObject currentPage;
    public List<string> _recollections; // two per leaf, four per page


    //scene switching
    [SerializeField] private Camera _camera;
    [SerializeField] public Camera[] _cameraPositions; //puts camera in different scenes
    public int currentScene; //0 means no change, 1 is therapist's office


    // win/lose condition logic
    [SerializeField] private GameObject _loseScreen;

    void Start()
    {
        currentScene = 1;
        _sanityMeter = 75;

        _journalPages = (AotList)Variables.Object(gameObject).Get("journalPages");
        currentPage = (GameObject)_journalPages[0];
    }

    void Update()
    {
        if (_sanityMeter > 100)
        {
            _sanityMeter = 100;
        }

        _sanityMeterText.text = _sanityMeter.ToString();



        if (_sanityMeter < 50) // 50 is arbitrary
        {
            playerLose();
        }
    }

    public void checkForSceneChange(DialogueNode node)
    {
        // Debug.Log("old scene = " + currentScene + ", current = " + node._scene);

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


    public void addRecollection(RecollectionNode recollection, int selected)
    {
        // add title and proper description to list
        _recollections.Add(recollection._title + ": " + recollection._possibleDescriptions[selected]);

        string header =
            _recollections[_recollections.Count - 1].Substring(0, _recollections[_recollections.Count - 1].IndexOf(": "));
        string description =
            _recollections[_recollections.Count - 1].Substring(_recollections[_recollections.Count - 1].IndexOf(": ") + 1);



        // if we've filled current page
        if (_recollections.Count > 4 && _recollections.Count % 4 == 1)
        {
            // instantiate new page
            GameObject newPage = Instantiate(_journalPagePrefab, _parent.transform);
            newPage.GetComponentInChildren<TMP_Text>().text = "";
            numPages++;

            // put header and description into journal
            newPage.GetComponentInChildren<TMP_Text>().text += $"<b>{header}</b>\n{description}\n\n";            

            // update UI Controller
            _journalPages.Add(newPage);
            Variables.Object(gameObject).Set("journalPages", _journalPages);
            currentPage = newPage;
            newPage.SetActive(false);
        }
        else // if we're continuing on the same page
        {
            if ((_recollections.Count - 2) % 4 == 1 || (_recollections.Count - 2) % 4 == 2)
            {
                // right leaf
                currentPage.transform.GetChild(1).GetComponentInChildren<TMP_Text>().text += $"<b>{header}</b>\n{description}\n\n";
            }
            else
            {
                // left leaf
                currentPage.transform.GetChild(0).GetComponentInChildren<TMP_Text>().text += $"<b>{header}</b>\n{description}\n\n";
            }
        }


        if (recollection._isAlwaysCorrect)
        {
            modifySanity(recollection, 0);
        }
        else
        {
            modifySanity(recollection, selected);
        }
    }


    private void modifySanity(RecollectionNode recollection, int selected)
    {
        int correctSorting = recollection._real ? 0: 1; // real = 0 because of indexing reasons

        Debug.Log("correct sorting = " + correctSorting + ", selected = " + selected);

        if (selected == correctSorting) 
        {
            _sanityMeter += recollection._sanityScore;
        }
        else
        {
            _sanityMeter -= recollection._sanityScore;
        }

        Debug.Log(_sanityMeter);


    }

    
    private void playerLose()
    {
        _loseScreen.SetActive(true);
    }
}
