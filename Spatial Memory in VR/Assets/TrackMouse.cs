using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackMouse : MonoBehaviour
{
    public Camera.MonoOrStereoscopicEye rightEye;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    /*void Update()
    {
        this.gameObject.transform.position = Input.mousePosition;
        print(Input.mousePosition);
    }*/

    //public float ZValue = 5;
    public Vector3 offset = new Vector3 (1f, 5f, 5f);

    void FixedUpdate()
    {
        Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f) + offset;
        Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorPoint, rightEye);
        transform.position = cursorPosition;
        transform.LookAt(Camera.main.transform);
        //print(transform.position);
    }
}
