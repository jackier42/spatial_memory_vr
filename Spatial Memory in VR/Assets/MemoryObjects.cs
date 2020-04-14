using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MemoryObjects : MonoBehaviour
{
    public List <Text> memoryObjects;

    void Start()
    {
        var children = this.gameObject.GetComponentsInChildren<Transform>();
        foreach (var child in children)
        {
            Text textComponent = child.GetComponentInChildren<Text>();
            memoryObjects.Add(textComponent);
            int randomCharIndex = Random.Range(65, 91);
            char randomChar = (char) randomCharIndex;
            textComponent.text = randomChar.ToString();
        }
    }

    void Update()
    {
        
    }
}
