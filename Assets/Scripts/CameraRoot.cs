using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRoot : MonoBehaviour
{

    public Transform target;
    public float DefaultUpAngle = 30.0f;
    public float DefaultSideAngle = 0.0f;

    float xrotOffset = 0;
    float yrotOffset = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        xrotOffset = DefaultUpAngle;
        yrotOffset = DefaultSideAngle;

    }

    // Update is called once per frame
    void Update()
    {
        //set its neutral position and rotation
        transform.position = target.position;
        transform.rotation = target.rotation;

        transform.Rotate(Vector3.right, xrotOffset);
        transform.Rotate(Vector3.up, yrotOffset);

        //get child at index 0, which is the actual camera
        transform.GetChild(0).transform.LookAt(target);

        if(Input.GetMouseButton(0))
        {
           
                xrotOffset += Input.GetAxis("Mouse Y") * 100 * Time.deltaTime;
                yrotOffset += Input.GetAxis("Mouse X") * 100 * Time.deltaTime;

        }
        else
        {
            xrotOffset = Mathf.Lerp(xrotOffset, DefaultUpAngle, Time.deltaTime * 10);
            yrotOffset = Mathf.Lerp(yrotOffset, DefaultSideAngle, Time.deltaTime * 10);
        }
        
    }
}
