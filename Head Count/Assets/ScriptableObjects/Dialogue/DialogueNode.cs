using UnityEngine;

[CreateAssetMenu(fileName = "DialogueLine", menuName = "ScriptableObjects/DialogueLine", order = 1)]
public class DialogueNode : ScriptableObject
{
    // who is saying this line?
    public string _speaker;

    //what scene does the node take place in?
    public int _scene;

    // is this information being saved to the Journal, and what information is being saved
    public bool _isFact;
    public RecollectionNode _recollection;

    // the lines of dialogue the NPC says for this node
    public string[] _lines;

    // the potential replies from the player
    public string[] _playerReplyOptions;

    // each index in _playerReplyOptions corresponds to an NPC reply in _nextNodes
    // so, for example, if the player chooses the first option (= index 0) from _playerReplyOptions,
    // the next DialogueLine that should be shown is index 0 in _nextNodes
    public DialogueNode[] _nextNodes;
}