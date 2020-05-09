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
    private int currentStimuliCount = 0;
    private System.Random rng = new System.Random();
    private List <int> positionsToTest;
    private List <int> positionsForTestingPhase;
    private int desktopFirst;
    private int lettersFirst;
    private bool changingPhase;

    public float trialStartTime;
    public string outputFilename;
    public int stimuliRepetitions = 3;

    private void Awake()
    {
        //UnityEngine.XR.XRSettings.enabled = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        stimulusObject.gameObject.SetActive(false);
        outputFilename = "Assets/Resources/Data/output-" + System.DateTime.Now.ToString("MM_dd-HH_mm_ss") + ".txt";
        File.WriteAllLines(outputFilename, memoryObjects.line);

        desktopFirst = Random.Range(0, 2);
        lettersFirst = Random.Range(0, 2);

        if (lettersFirst == 1)
        {
            memoryObjects.SetObjectsToDisplayLetters();
            stimulusObject.GetComponentInChildren<RawImage>().enabled = false;
        }
        else
        {
            memoryObjects.SetObjectsToDisplayEmojis();
            stimulusObject.GetComponentInChildren<Text>().enabled = false;
        }
        if (desktopFirst == 0)
        {
            trackedPoseDriver.enabled = true;
            trackedPoseDriver.gameObject.GetComponentInParent<FirstPersonController>().enabled = false;
        }

        changingPhase = false;
    }

    public void Shuffle (IList list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            var value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
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

    public void LetterIsClicked (string letter)
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
            printout += letterClicked + " time taken " + timeTaken.ToString() + " global timer " + Time.time.ToString() + " aiming for " + stimulusObject.stimulusText.text;
            print(printout);
            WriteStringToFile(printout);

            trialStartTime = Time.time;
            //stimulusObject.AssignRandomLetter();
            if (positionsToTest.Count > 0)
            {
                int letterPosition = positionsToTest[0];
                positionsToTest.RemoveAt(0);
                print("letterPosition " + letterPosition);
                stimulusObject.AssignLetter(letterPosition);
            }
            else
            {
                changingPhase = true;
            }
        }
    }

    public void EmojiIsClicked (string indexName)
    {
        string printout = "";
        if (currentPhase == TRAINING_FIRST || currentPhase == TESTING_FIRST || currentPhase == TRAINING_SECOND || currentPhase == TESTING_SECOND)
        {
            //print(indexName + " vs " + stimulusObject.GetComponentInChildren<RawImage>().texture.name);
            if (indexName == stimulusObject.GetComponentInChildren<RawImage>().texture.name)
            {
                printout += "correct ";
            }
            else
            {
                printout += "incorrect ";
            }
            float timeTaken = Time.time - trialStartTime;
            printout += indexName + " time taken " + timeTaken.ToString() + " global timer " + Time.time.ToString() + " aiming for " + stimulusObject.GetComponentInChildren<RawImage>().texture.name;
            print(printout);
            WriteStringToFile(printout);

            trialStartTime = Time.time;
            //stimulusObject.AssignRandomLetter();
            if (positionsToTest.Count > 0)
            {
                int emojiPosition = positionsToTest[0];
                positionsToTest.RemoveAt(0);
                print("emojiPosition " + emojiPosition);
                stimulusObject.AssignEmoji(emojiPosition);
            }
            else
            {
                changingPhase = true;
            }
        }
    }

    private List<int> CreateStimuliList(bool firstRound)
    {
        var listToAddFrom = new List<int>();
        if (firstRound)
        {
            if (lettersFirst == 1)
            {
                listToAddFrom = memoryObjects.letterStimuliPositions;
            }
            else
            {
                listToAddFrom = memoryObjects.emojiStimuliPositions;
            }
        }
        else
        {
            if (lettersFirst == 0)
            {
                listToAddFrom = memoryObjects.letterStimuliPositions;
            }
            else
            {
                listToAddFrom = memoryObjects.emojiStimuliPositions;
            }
        }
        var returnList = new List<int>();

        for (int i = 0; i < stimuliRepetitions; i++)
        {
            for (int j = 0; j < listToAddFrom.Count; j++)
            {
                returnList.Add(listToAddFrom[j]);
                print("adding " + listToAddFrom[j] + " to posititonsToTEST");
            }
        }

        Shuffle(returnList);

        return returnList;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || currentPhaseTime < 0 || changingPhase)
        {
            changingPhase = false;
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

                    positionsToTest = CreateStimuliList(true);
                    positionsForTestingPhase = new List<int>(positionsToTest);

                    stimulusObject.AssignLetter(positionsToTest[0]);
                    stimulusObject.AssignEmoji(positionsToTest[0]);
                    positionsToTest.RemoveAt(0);

                    setupCurrentPhase = false;
                }

                currentPhaseTime = System.Math.Round(learningPhaseTime - (Time.time - endOfPastPhaseTime), 2);
                displayText.text = "Learning Phase\n";// + (currentPhaseTime).ToString();

                
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
                    positionsToTest = new List<int>(positionsForTestingPhase);
                    Shuffle(positionsToTest);

                    stimulusObject.AssignLetter(positionsToTest[0]);
                    positionsToTest.RemoveAt(0);

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
                displayText.text = "Testing Phase\n";// + currentPhaseTime.ToString();
                //memoryObjects.SetObjectsBlank();
                break;
            case BREAK_SECOND:
                if (setupCurrentPhase)
                {
                    stimulusObject.gameObject.SetActive(false);
                    displayText.text = "Second break";

                    if (desktopFirst == 1)
                    {
                        trackedPoseDriver.enabled = true;
                        trackedPoseDriver.gameObject.GetComponentInParent<FirstPersonController>().enabled = false;
                    }
                    else
                    {
                        trackedPoseDriver.enabled = false;
                        trackedPoseDriver.gameObject.GetComponentInParent<FirstPersonController>().enabled = true;
                    }

                    setupCurrentPhase = false;
                }
                break;
            case TRAINING_SECOND:
                if (setupCurrentPhase)
                {
                    stimulusObject.gameObject.SetActive(true);

                    positionsToTest = CreateStimuliList(false);
                    positionsForTestingPhase = new List<int>(positionsToTest);

                    stimulusObject.AssignLetter(positionsToTest[0]);
                    stimulusObject.AssignEmoji(positionsToTest[0]);
                    positionsToTest.RemoveAt(0);

                    if (lettersFirst == 0)
                    {
                        memoryObjects.SetObjectsToDisplayLetters();
                        stimulusObject.GetComponentInChildren<RawImage>().enabled = false;
                        stimulusObject.GetComponentInChildren<Text>().enabled = true;
                    }
                    else
                    {
                        memoryObjects.SetObjectsToDisplayEmojis();
                        stimulusObject.GetComponentInChildren<Text>().enabled = false;
                        stimulusObject.GetComponentInChildren<RawImage>().enabled = true;
                    }

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
                    positionsToTest = new List<int>(positionsForTestingPhase);
                    Shuffle(positionsToTest);

                    stimulusObject.AssignLetter(positionsToTest[0]);
                    positionsToTest.RemoveAt(0);

                    displayText.text = "Second testing";
                    setupCurrentPhase = false;
                }

                break;
            default:
                displayText.text = "Finished";
                stimulusObject.gameObject.SetActive(false);
                break;
        }

    }
}
