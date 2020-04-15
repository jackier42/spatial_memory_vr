using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryObjectIndividual : MonoBehaviour
{
    public Material highlightMaterial;
    public Material generalMaterial;

    // Start is called before the first frame update
    void Start()
    {
        
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

}
