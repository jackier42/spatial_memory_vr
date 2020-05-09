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
        DeHighlight();
    }

    private void OnMouseOver()
    {
        Highlight();
    }

    public void Highlight()
    {
        this.gameObject.GetComponent<MeshRenderer>().material = highlightMaterial;
    }

    private void OnMouseExit()
    {
        DeHighlight();
    }

    public void DeHighlight()
    {
        this.gameObject.GetComponent<MeshRenderer>().material = generalMaterial;
    }

    private void OnMouseDown()
    {
        ObjectHit();
    }

    public void ObjectHit()
    {
        if (this.GetComponentInChildren<Text>().enabled)
        {
            timer.LetterIsClicked(this.GetComponentInChildren<Text>().text);
        }

        if (this.GetComponentInChildren<RawImage>().enabled)
        {
            timer.EmojiIsClicked(this.GetComponentInChildren<RawImage>().texture.name);
        }
    }

}
