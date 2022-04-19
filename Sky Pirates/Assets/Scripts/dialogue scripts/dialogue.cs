using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class dialogue
{
    public string[] names;
    public Sprite[] talksprites;

    [TextArea(3, 10)]
    
    public string[] sentences;
}
