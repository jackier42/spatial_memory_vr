using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class MemoryObjects : MonoBehaviour
{
    public List <GameObject> memoryObjects;
    public HashSet <char> symbolsUsed;
    public HashSet <int> emojisUsed;
    public List<int> letterStimuliPositions;
    public List<int> emojiStimuliPositions;
    public List<string> line;
    public int numberOfStimuli;

    void Start()
    {
        symbolsUsed = new HashSet<char>();
        emojisUsed = new HashSet<int>();
        line = new List<string>();
        foreach (Transform child in transform)
        {
            GameObject memoryObject = child.gameObject;
            memoryObjects.Add(memoryObject);

            int randomCharIndex = UnityEngine.Random.Range(33, 91);
            char randomChar = (char)randomCharIndex;
            while (symbolsUsed.Contains(randomChar))
            {
                randomCharIndex = UnityEngine.Random.Range(33, 91);
                randomChar = (char)randomCharIndex;
            }

            symbolsUsed.Add(randomChar);
            memoryObject.GetComponentInChildren<Text>().text = randomChar.ToString();

            string texturePath = "EmojiImages/" + (randomCharIndex - 33).ToString();
            Texture2D tex = Resources.Load <Texture2D>(texturePath);
            RawImage image = memoryObject.GetComponentInChildren<RawImage>();
            image.texture = tex;
            //image.enabled = false;

            line.Add(child.gameObject.transform.name + " " + memoryObject.GetComponentInChildren<Text>().text);
            
        }

        for (int i = 0; i < numberOfStimuli; i++)
        {
            var randomNumber = UnityEngine.Random.Range(0, memoryObjects.Count);
            while (letterStimuliPositions.Contains(randomNumber))
            {
                randomNumber = UnityEngine.Random.Range(0, memoryObjects.Count);
            }
            letterStimuliPositions.Add(randomNumber);

            randomNumber = UnityEngine.Random.Range(0, memoryObjects.Count);
            while (emojiStimuliPositions.Contains(randomNumber))
            {
                randomNumber = UnityEngine.Random.Range(0, memoryObjects.Count);
            }
            emojiStimuliPositions.Add(randomNumber);
        }
    }

    public void SetObjectsBlank()
    {
        foreach (var obj in memoryObjects)
        {
            obj.gameObject.GetComponentInChildren<Canvas>().enabled = false;
        }
    }

    public void SetObjectsToDisplayLetters()
    {
        foreach (var obj in memoryObjects)
        {
            obj.gameObject.GetComponentInChildren<RawImage>().enabled = false;
            obj.gameObject.GetComponentInChildren<Text>().enabled = true;
        }
    }

    public void SetObjectsToDisplayEmojis()
    {
        foreach (var obj in memoryObjects)
        {
            obj.gameObject.GetComponentInChildren<RawImage>().enabled = true;
            obj.gameObject.GetComponentInChildren<Text>().enabled = false;
        }
    }

}
