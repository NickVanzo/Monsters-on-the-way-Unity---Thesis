using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISoundsHandler : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] AudioClip buttonClickedSound;

    private void Start()
    {
        audioSource = GameObject.Find("Player").GetComponent<AudioSource>();
    }

    public void PlaySoundOfButtonClick()
    {
        audioSource.PlayOneShot(buttonClickedSound);
    }
}
