using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public int learningPhaseTime = 4;
    public int testingPhaseTime = 5;
    private double currentPhaseTime;
    private int currentPhase = 0;
    private double endOfPastPhaseTime = 0.0f;
    public Text displayText;

    // Start is called before the first frame update
    void Start()
    {

    }

    void RunTestingStimulus()
    {


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentPhase++;
            endOfPastPhaseTime = Time.time;
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
