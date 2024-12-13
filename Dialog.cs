using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    public Text dialogueText;
    private Queue<string> sentences;

    public Dialogue dialogue;

    public GameObject hinding;
    
// Use this for initialization
    void Start () {
        sentences = new Queue<string>();
         FindObjectOfType<Dialog>().StartDialogue(dialogue);
    }

    public void StartDialogue (Dialogue dialogue)
    {
        
        sentences. Clear();
        foreach (string sentence in dialogue.sentences)
            {
                sentences.Enqueue(sentence);
            }
        DisplayNextSentence();
    }


public void DisplayNextSentence ()
{
    if (sentences.Count == 0)
{
EndDialogue();
return;
}
string sentence = sentences. Dequeue ();
dialogueText. text = sentence;
}


void EndDialogue ()
{
 hinding.SetActive(true);
 this.gameObject.SetActive(false);
}


}

