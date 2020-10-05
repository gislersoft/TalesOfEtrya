using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXLockedDoor : Interactable
{

    public AudioSource lockedDoorAudio;

    private void Awake()
    {
        
    }

    public override void Interact()
    {
        base.Interact();
        playLockedDoor();
            
    }

    public void playLockedDoor()
    {
        if (lockedDoorAudio != null && !lockedDoorAudio.isPlaying)
        {
            lockedDoorAudio.Play();
        }
    }

    
}
