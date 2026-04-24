using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Assertions;

using TMPro;

public class DialogueUI : MonoBehaviour
{
    // fonts
    [SerializeField] private TMP_FontAsset _georgia;
    [SerializeField] private TMP_FontAsset _studyNight;

    [SerializeField] private GameObject _dialogueBox;
    [SerializeField] private TMP_Text _npcText;
    [SerializeField] private GameObject _playerOptions;
    [SerializeField] private TMP_Text _option1;
    [SerializeField] private TMP_Text _option2;
    [SerializeField] private TMP_Text _option3;

    public void ShowDialogue(string speaker, string dialogue)
    {
        _dialogueBox.SetActive(true);

        _npcText.enabled = true;
        _playerOptions.SetActive(false);


        // set text style depending on speaker
        if (speaker == "player")
        {
            _npcText.font = _studyNight;
        }
        else
        {
            _npcText.font = _georgia;
        }

            _npcText.text = dialogue;
    }

    // note: this only works for up to 3 dialogue options at a time currently
    // if you want to make more possible, you may have to get crafty with the UI... :)
    public void ShowPlayerOptions(string[] options)
    {
        _dialogueBox.SetActive(true);

        _npcText.enabled = false;
        _playerOptions.SetActive(true);

        _option1.text = options[0];

        if (options.Length >= 2)
        {
            _option2.transform.parent.gameObject.SetActive(true);
            _option2.text = options[1];
        }
        else
        {
            _option2.transform.parent.gameObject.SetActive(false);
        }

        if (options.Length >= 3)
        {
            _option3.transform.parent.gameObject.SetActive(true);
            _option3.text = options[2];
        }
        else
        {
            _option3.transform.parent.gameObject.SetActive(false);
        }
    }

    public void HideDialogue()
    {
        _dialogueBox.SetActive(false);
        _playerOptions.SetActive(false);
        _npcText.enabled = false;
    }
}