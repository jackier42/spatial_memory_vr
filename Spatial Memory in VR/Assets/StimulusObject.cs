using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StimulusObject : MonoBehaviour
{
    public Text stimulusText;
    // Start is called before the first frame update
    void Start()
    {
        AssignRandomLetter();
        stimulusText = this.GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AssignRandomLetter()
    {
        int randomCharIndex = Random.Range(33, 91);
        char randomChar = (char)randomCharIndex;
        stimulusText.text = randomChar.ToString();
    }
}
