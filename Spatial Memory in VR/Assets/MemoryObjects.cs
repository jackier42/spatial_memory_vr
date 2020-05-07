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
            //var texture = Resources.Load<Texture>("EmojiImages/1.png");
            Texture2D tex = (Texture2D)Resources.Load("EmojiImages/1.png", typeof(Texture2D));
            //memoryObject.GetComponentInChildren<RawImage>().texture = tex;
            

            line.Add(child.gameObject.transform.name + " " + memoryObject.GetComponentInChildren<Text>().text);
            
        }

        for (int i = 0; i < 20; i++)
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
}
