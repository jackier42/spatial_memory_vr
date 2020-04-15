using System.Collections;
using System.Collections.Generic;
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
        foreach (var child in children)
        {
            Text textComponent = child;
            memoryObjects.Add(textComponent);

            int randomCharIndex = Random.Range(33, 91);
            char randomChar = (char)randomCharIndex;
            while (symbolsUsed.Contains(randomChar))
            {
                randomCharIndex = Random.Range(33, 91);
                randomChar = (char)randomCharIndex;
            }

            symbolsUsed.Add(randomChar);
            textComponent.text = randomChar.ToString();
        }
    }
}
