using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dialogueManager : MonoBehaviour
{

    private Queue<string> sentences;
    private Queue<string> names;
    private Queue<Sprite> tSprites;

    public Text nameText;
    public Text dialogueText;
    public Image spriteImage;

    public GameObject Stratus;
    public GameObject Coco;
    public GameObject Coco2;
    public GameObject Coco3;

    public bool inConvo = false;

    // Start is called before the first frame update
    void Start()
    {
        // new sentence
        sentences = new Queue<string>();

        // new name?
        names = new Queue<string>();

        // new talksprites
        tSprites = new Queue<Sprite>();
        spriteImage = spriteImage.GetComponent<Image>();
    }

    public void StartDialogue (dialogue dialogue)
    {
        // Debug.Log("Starting conversation with" + dialogue.name);

        inConvo = true;

        // clear existing sentences
        sentences.Clear();
        names.Clear();
        tSprites.Clear();

        // display each sentence
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        foreach (string name in dialogue.names)
        {
            names.Enqueue(name);
        }

        foreach (Sprite sprite in dialogue.talksprites)
        {
            tSprites.Enqueue(sprite);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        // if no more sentences then end convo
        if (sentences.Count == 0)
        {
            inConvo = false;
            return;
        }

        Sprite sprite = tSprites.Dequeue();
        string name = names.Dequeue();
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        nameText.text = name;
        spriteImage.sprite = sprite;
        StartCoroutine(TypeSentence(sentence));
    }

    // type the sentence out
    IEnumerator TypeSentence (string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    public void stratusInteraction1()
    {
        StartDialogue(Stratus.GetComponent<dialogueTrigger>().dialogue);
    }

    public void cocoInteraction1()
    {
        StartDialogue(Coco.GetComponent<dialogueTrigger>().dialogue);
    }

    public void cocoInteraction2()
    {
        StartDialogue(Coco2.GetComponent<dialogueTrigger>().dialogue);
    }

    public void cocoInteraction3()
    {
        StartDialogue(Coco3.GetComponent<dialogueTrigger>().dialogue);
    }
}
