using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTracking : MonoBehaviour
{

    Camera mycam;
    // Start is called before the first frame update
    void Start()
    {
       mycam  = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(mycam.ScreenToViewportPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, mycam.nearClipPlane)), Vector3.up);
        RaycastHit hit;
        Camera camera = this.gameObject.GetComponent<Camera>();
        var cameraCenter = camera.ScreenToWorldPoint(new Vector3(Screen.width / 2f, Screen.height / 2f, camera.nearClipPlane));
        if (Physics.Raycast(cameraCenter, this.transform.forward, out hit, 1000))
        {
            var obj = hit.transform.gameObject;
            print(obj.name);
        }
    }

    private void RotateView()
    {
       // m_MouseLook.LookRotation(transform, m_Camera.transform);
    }
}
