using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public int learningPhaseTime = 4;
    public int testingPhaseTime = 50;
    private double currentPhaseTime;
    private int currentPhase = 0;
    private double endOfPastPhaseTime = 0.0f;
    public Text displayText;
    public StimulusObject stimulusObject;
    public string letterClicked;

    public float trialStartTime;

    // Start is called before the first frame update
    void Start()
    {
        stimulusObject.gameObject.SetActive(false);
    }

    public void letterIsClicked(string letter)
    {
        letterClicked = letter;
        if (currentPhase == 3)
        {
            if (letterClicked == stimulusObject.stimulusText.text)
            {
                float timeTaken = Time.time - trialStartTime;
                print("correct! " + timeTaken.ToString());
                
                trialStartTime = Time.time;
                stimulusObject.AssignRandomLetter();
            }
            else
            {
                print("incorrect :(");
            }
            
        }
    }

    void RunTestingStimulus()
    {
        stimulusObject.gameObject.SetActive(true);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentPhase++;
            endOfPastPhaseTime = Time.time;
            if (currentPhase == 3)
                trialStartTime = Time.time;
        }

        switch (currentPhase) {
            case 0:
                //intro
                displayText.text = "Introduction";
                break;
            case 1:
                //learning
                currentPhaseTime = System.Math.Round(learningPhaseTime - (Time.time - endOfPastPhaseTime), 2);
                displayText.text = "Learning Phase\n" + (currentPhaseTime).ToString();
                if (currentPhaseTime <= 0)
                {
                    // finishing learning phase
                    currentPhase++;
                }
                break;
            case 2:
                //break
                displayText.text = "Break between phases";
                break;
            case 3:
                currentPhaseTime = System.Math.Round(testingPhaseTime - (Time.time - endOfPastPhaseTime), 2);
                displayText.text = "Testing Phase\n" + currentPhaseTime.ToString();
                if (currentPhaseTime <= 0)
                {
                    // finishing learning phase
                    currentPhase++;
                }
                RunTestingStimulus();
                break;
            case 4:
                displayText.text = "Finished";
                break;
        }

    }
}
