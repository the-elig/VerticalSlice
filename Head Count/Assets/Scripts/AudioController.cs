using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    private AudioSource _audSource;

    [SerializeField] private AudioClip[] _turnPageSounds;

    private void Start()
    {
        _audSource = GetComponent<AudioSource>();
    }

    public void playPageTurnSound()
    {
        int ran = Random.Range(0, _turnPageSounds.Length);

        _audSource.PlayOneShot(_turnPageSounds[ran], 1f);
    }
    
    
}
