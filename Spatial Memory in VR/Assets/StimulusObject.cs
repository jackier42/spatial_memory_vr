using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StimulusObject : MonoBehaviour
{
    public Text stimulusText;
    public MemoryObjects memoryObjects;
    private int lastRandomCharIndex = 0;
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
        int randomCharIndex = Random.Range(0, 19);
        while (lastRandomCharIndex == randomCharIndex)
            randomCharIndex = Random.Range(0, 19);
        lastRandomCharIndex = randomCharIndex;
        string randomChar = memoryObjects.memoryObjects[memoryObjects.letterStimuliPositions[randomCharIndex]].GetComponentInChildren<Text>().text;
        stimulusText.text = randomChar.ToString();
    }

    public void AssignRandomEmoji()
    {


    }
}
