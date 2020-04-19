using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MemoryObjectIndividual : MonoBehaviour
{
    public Material highlightMaterial;
    public Material generalMaterial;
    public Timer timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = GameObject.Find("TimerText").GetComponent<Timer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseOver()
    {
        this.gameObject.GetComponent<MeshRenderer>().material = highlightMaterial;
    }

    private void OnMouseExit()
    {
        this.gameObject.GetComponent<MeshRenderer>().material = generalMaterial;
    }

    private void OnMouseDown()
    {
        timer.letterIsClicked(this.GetComponentInChildren<Text>().text);
    }

}
