using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private DialogueUI _dialogue;
    [SerializeField] private DialogueNode _dialogueStartNode;

    private DialogueNode _currentNode;
    private int _currentLine = 0;
    private bool _runningDialogue;
    private bool _waitingForPlayerResponse;

    private void Start()
    {
        _currentNode = _dialogueStartNode;
    }

    private void Update()
    {
        // clicking the journal doesn't forward dialogue
        bool mouseClickable = (Input.mousePosition.x < 845 || Input.mousePosition.y < 500)
            && !(bool)Variables.Scene(gameObject).Get("journalIsOpen");
            
            //.Get("journalIsOpen");

        if (!_waitingForPlayerResponse && 
            (Input.GetKeyDown(KeyCode.Space) || (Input.GetMouseButtonDown(0) && mouseClickable)))
        {
            AdvanceDialogue();
        }
    }

    private void AdvanceDialogue()
    {
        _runningDialogue = true;

        if (_currentLine < _currentNode._lines.Length)
        {
            // if we still have NPC lines left, keep playing NPC lines
            _dialogue.ShowDialogue(_currentNode._speaker, _currentNode._lines[_currentLine]);
            _currentLine++;
        }
        else if (_currentNode._playerReplyOptions != null && _currentNode._playerReplyOptions.Length > 0)
        {
            // show player dialogue options, if there are any
            _waitingForPlayerResponse = true;
            _dialogue.ShowPlayerOptions(_currentNode._playerReplyOptions);
        }
        else if (_currentNode._playerReplyOptions.Length == 0 && _currentNode._nextNodes.Length == 1)
        {
            // if there are no reply options, but the speaker switches, go to next node
            Debug.Log("switch speaker triggered");
            SelectedOption(0);
        }
        else
        {
            // if there are no NPC or player lines left, close dialogue UI
            EndDialogue();
        }
    }

    private void EndDialogue()
    {
        _runningDialogue = false;
        _waitingForPlayerResponse = false;
        _currentNode = _dialogueStartNode;
        _currentLine = 0;
        _dialogue.HideDialogue();
    }

    public void SelectedOption(int option)
    {
        if (_currentNode._saveToJournal)
        {
            // add fact to player facts if applicable
            _player._facts.Add(_currentNode._playerReplyOptions[option]);
        }
        

        // go to next node
        _currentLine = 0;
        _waitingForPlayerResponse = false;

        _currentNode = _currentNode._nextNodes[option];
        AdvanceDialogue();
    }
}

