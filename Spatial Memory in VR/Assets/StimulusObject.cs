using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StimulusObject : MonoBehaviour
{
    public Text stimulusText;
    public RawImage stimulusImage;
    public MemoryObjects memoryObjects;
    private int lastRandomCharIndex = 0;
    private int lastRandomEmojiIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        stimulusText = this.GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AssignRandomLetter()
    {
        int randomCharIndex = UnityEngine.Random.Range(0, 20);
        while (lastRandomCharIndex == randomCharIndex)
            randomCharIndex = UnityEngine.Random.Range(0, 20);
        lastRandomCharIndex = randomCharIndex;
        AssignLetter(randomCharIndex);
    }

    public void AssignRandomEmoji()
    {
        int randomEmojiIndex = UnityEngine.Random.Range(0, 20);
        while (lastRandomEmojiIndex == randomEmojiIndex)
            randomEmojiIndex = UnityEngine.Random.Range(0, 20);
        lastRandomEmojiIndex = randomEmojiIndex;
        AssignEmoji(randomEmojiIndex);
    }

    public void AssignLetter(int letterPosition)
    {
        string assignedChar = memoryObjects.memoryObjects[letterPosition].GetComponentInChildren<Text>().text;
        stimulusText.text = assignedChar.ToString();
    }

    public void AssignEmoji(int emojiPosition)
    {
        Texture2D tex = Resources.Load<Texture2D>("EmojiImages/" + memoryObjects.memoryObjects[emojiPosition].GetComponentInChildren<RawImage>().texture.name);
        RawImage image = this.GetComponentInChildren<RawImage>();
        image.texture = tex;
    }
}
