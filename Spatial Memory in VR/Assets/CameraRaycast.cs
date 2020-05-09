using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRaycast : MonoBehaviour
{
    private Camera m_Camera;

    // Start is called before the first frame update
    void Start()
    {
        m_Camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckCameraHit();
    }

    private void CheckCameraHit()
    {
        RaycastHit hit;
        
        var cameraCenter = m_Camera.ScreenToWorldPoint(new Vector3(Screen.width / 2f, Screen.height / 2f, m_Camera.nearClipPlane));
        if (Physics.Raycast(cameraCenter, m_Camera.transform.forward, out hit, 1000))
        {
            var obj = hit.transform.gameObject;
            
            obj.GetComponentInChildren<MemoryObjectIndividual>()?.Highlight();
            
            if (Input.GetMouseButtonDown(0))
            {
                print("HIT! " + obj.name);
                obj.GetComponentInChildren<MemoryObjectIndividual>()?.ObjectHit();
            }
        }
    }
}
