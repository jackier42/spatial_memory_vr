using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class MemoryObjects : MonoBehaviour
{
    public List <Text> memoryObjects;
    public HashSet<char> symbolsUsed;

    void Start()
    {
        var children = this.gameObject.GetComponentsInChildren<Text>();
        print(children.Length);
        symbolsUsed = new HashSet<char>();
        List <string> line = new List<string>();
        foreach (var child in children)
        {
            Text textComponent = child;
            memoryObjects.Add(textComponent);

            int randomCharIndex = UnityEngine.Random.Range(33, 91);
            char randomChar = (char)randomCharIndex;
            while (symbolsUsed.Contains(randomChar))
            {
                randomCharIndex = UnityEngine.Random.Range(33, 91);
                randomChar = (char)randomCharIndex;
            }

            symbolsUsed.Add(randomChar);
            textComponent.text = randomChar.ToString();
            

            line.Add(child.gameObject.transform.parent.parent.name + " " + child.text);
            
        }
        var path = "D:/data.txt";
        File.WriteAllLines(path, line);
    }

    public void SetObjectsBlank()
    {
        foreach (var obj in memoryObjects)
        {
            obj.gameObject.GetComponentInParent<Canvas>().enabled = false;
        }
    }
}
