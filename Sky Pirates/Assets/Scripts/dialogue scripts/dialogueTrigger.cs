using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dialogueTrigger : MonoBehaviour
{
    public dialogue dialogue;
    public bool inConvo = false;

    public void TriggerDialogue ()
    {
        FindObjectOfType<dialogueManager>().StartDialogue(dialogue);
        inConvo = true;
    }
}
