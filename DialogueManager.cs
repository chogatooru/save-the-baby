using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public Dialogue dialogue;
    public void TriggerDialogue (){
        FindObjectOfType<Dialog>() .StartDialogue(dialogue);

    }
    

}
