using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SpatialTracking;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

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
    public MemoryObjects memoryObjects;
    public TrackedPoseDriver camera;

    public float trialStartTime;

    private void Awake()
    {
        UnityEngine.XR.XRSettings.enabled = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        stimulusObject.gameObject.SetActive(false);
    }

    public void letterIsClicked(string letter)
    {
        letterClicked = letter;
        string printout = "";
        if (currentPhase == 3)
        {
            if (letterClicked == stimulusObject.stimulusText.text)
            {
                printout += "correct ";
            }
            else
            {
                printout += "incorrect ";
            }
            float timeTaken = Time.time - trialStartTime;
            printout += stimulusObject.gameObject.name + " time taken " + timeTaken.ToString() + " global timer " + Time.time.ToString();
            print(printout);

            trialStartTime = Time.time;
            stimulusObject.AssignRandomLetter();

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
            print("transition to " + (currentPhase + 1) + " at " + Time.time);
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
                    print("transition to " + (currentPhase + 1) + " at " + Time.time);
                    currentPhase++;
                }
                break;
            case 2:
                //break
                displayText.text = "Break between phases";
                break;
            case 3:
                //UnityEngine.XR.XRSettings.enabled = true;
                camera.enabled = true;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                //camera.gameObject.GetComponentInParent<FirstPersonController>().enabled = false;
                currentPhaseTime = System.Math.Round(testingPhaseTime - (Time.time - endOfPastPhaseTime), 2);
                displayText.text = "Testing Phase\n" + currentPhaseTime.ToString();
                if (currentPhaseTime <= 0)
                {
                    // finishing learning phase
                    print("transition to " + (currentPhase + 1) + " at " + Time.time);
                    currentPhase++;
                }
                memoryObjects.SetObjectsBlank();
                RunTestingStimulus();
                break;
            case 4:
                displayText.text = "Finished";
                break;
        }

    }
}
