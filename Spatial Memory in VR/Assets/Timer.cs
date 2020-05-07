using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.SpatialTracking;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class Timer : MonoBehaviour
{
    public const int INTRODUCTION = 0;
    public const int TRAINING_FIRST = 1;
    public const int BREAK_FIRST = 2;
    public const int TESTING_FIRST = 3;
    public const int BREAK_SECOND = 4;
    public const int TRAINING_SECOND = 5;
    public const int BREAK_THIRD = 6;
    public const int TESTING_SECOND = 7;

    public int learningPhaseTime = 4;
    public int testingPhaseTime = 50;
    private double currentPhaseTime;
    private int currentPhase = 0;
    private double endOfPastPhaseTime = 0.0f;
    public Text displayText;
    public StimulusObject stimulusObject;
    public string letterClicked;
    public MemoryObjects memoryObjects;
    public TrackedPoseDriver trackedPoseDriver;
    private FirstPersonController firstPersonController;
    private bool setupCurrentPhase = false;

    public float trialStartTime;

    public string outputFilename;

    private void Awake()
    {
        UnityEngine.XR.XRSettings.enabled = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        stimulusObject.gameObject.SetActive(false);
        outputFilename = "Assets/Resources/Data/output-" + System.DateTime.Now.ToString("MM_dd-HH_mm_ss") + ".txt";
        //WriteStringToFile(memoryObjects.line.ToString());
        File.WriteAllLines(outputFilename, memoryObjects.line);
    }

    private void WriteStringToFile(string output)
    {
        StreamWriter writer = new StreamWriter(outputFilename, true);
        writer.WriteLine(output);
        writer.Close();

        //Re-import the file to update the reference in the editor
        AssetDatabase.ImportAsset(outputFilename);
        //TextAsset asset = (TextAsset) Resources.Load("Data/output.txt");

        //Print the text from the file
        //Debug.Log(asset.text);
    }

    public void letterIsClicked (string letter)
    {
        letterClicked = letter;
        string printout = "";
        if (currentPhase == TRAINING_FIRST || currentPhase == TESTING_FIRST || currentPhase == TRAINING_SECOND || currentPhase == TESTING_SECOND)
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
            printout += letterClicked + " time taken " + timeTaken.ToString() + " global timer " + Time.time.ToString();
            print(printout);
            WriteStringToFile(printout);

            trialStartTime = Time.time;
            stimulusObject.AssignRandomLetter();

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || currentPhaseTime < 0)
        {
            print("transition to " + (currentPhase + 1) + " at " + Time.time);
            WriteStringToFile("transition to " + (currentPhase + 1) + " at " + Time.time);
            currentPhase++;
            endOfPastPhaseTime = Time.time;
            if (currentPhase == 3)
                trialStartTime = Time.time;
            setupCurrentPhase = true;
        }

        switch (currentPhase) {
            case INTRODUCTION:
                //intro
                displayText.text = "Introduction";
                break;
            case TRAINING_FIRST:
                //learning

                if (setupCurrentPhase)
                {
                    stimulusObject.gameObject.SetActive(true);
                    stimulusObject.AssignRandomLetter();
                    setupCurrentPhase = false;
                }

                currentPhaseTime = System.Math.Round(learningPhaseTime - (Time.time - endOfPastPhaseTime), 2);
                displayText.text = "Learning Phase\n" + (currentPhaseTime).ToString();

                
                break;
            case BREAK_FIRST:
                //break

                if (setupCurrentPhase)
                {
                    stimulusObject.gameObject.SetActive(false);
                    setupCurrentPhase = false;
                }

                currentPhaseTime = 0f;
                displayText.text = "Break between phases";
                break;
            case TESTING_FIRST:

                if (setupCurrentPhase)
                {
                    stimulusObject.gameObject.SetActive(true);
                    stimulusObject.AssignRandomLetter();
                    displayText.text = "First testing phase";
                    setupCurrentPhase = false;
                }

                //UnityEngine.XR.XRSettings.enabled = true;
                //trackedPoseDriver.enabled = true;
                //Cursor.visible = true;
                //Cursor.lockState = CursorLockMode.None;
                //trackedPoseDriver.gameObject.GetComponentInParent<FirstPersonController>().enabled = false;
                //firstPersonController.ChangeCamera(trackedPoseDriver);
                currentPhaseTime = System.Math.Round(testingPhaseTime - (Time.time - endOfPastPhaseTime), 2);
                displayText.text = "Testing Phase\n" + currentPhaseTime.ToString();
                //memoryObjects.SetObjectsBlank();
                break;
            case BREAK_SECOND:
                if (setupCurrentPhase)
                {
                    stimulusObject.gameObject.SetActive(false);
                    displayText.text = "Second break";
                    setupCurrentPhase = false;
                }
                break;
            case TRAINING_SECOND:
                if (setupCurrentPhase)
                {
                    stimulusObject.gameObject.SetActive(true);
                    stimulusObject.AssignRandomLetter();
                    displayText.text = "Second training";
                    setupCurrentPhase = false;
                }
                break;
            case BREAK_THIRD:
                if (setupCurrentPhase)
                {
                    stimulusObject.gameObject.SetActive(false);
                    displayText.text = "Third break";
                    setupCurrentPhase = false;
                }

                break;
            case TESTING_SECOND:
                if (setupCurrentPhase)
                {
                    stimulusObject.gameObject.SetActive(true);
                    stimulusObject.AssignRandomLetter();
                    displayText.text = "Second testing";
                    setupCurrentPhase = false;
                }

                break;
            default:
                displayText.text = "Finished";
                break;
        }

    }
}
