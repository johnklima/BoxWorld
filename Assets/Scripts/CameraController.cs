using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float height = 3.0f;
    public float distance = 6.0f;

    public bool positionCamera = false;

    public Renderer[] rends;
    public Color[] colors;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //ANYTHING THAT WAS TRANSPARENT LAST FRAME, RESTORE TO IT´S PREVIOUS VALUE
        for (int i = 0; i < rends.Length; i++)
        {
            rends[i].material.color = colors[i];
        }


        //position the camera
        if (positionCamera)
        {
            Vector3 pos = target.position;
            Vector3 fwd = target.forward;

            transform.position = pos - (fwd * distance) + (Vector3.up * height);
            transform.LookAt(pos);

        }

        //DO THE RAYCAST
        float dist = Vector3.Magnitude(transform.position - target.position) - 1.0f;

        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position, transform.forward, dist);

        //buffer the render component, and the color of the material prior
        //to changing it, because I want to change it back one it is no longer
        //in the way
        rends = new Renderer[hits.Length];
        colors = new Color[hits.Length];

        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];
            Renderer rend = hit.transform.GetComponent<Renderer>();            

            //Debug.Log("raycast hit" + hit.transform.name);

            if (rend)
            {
                //buffer which renderer components I have changed
                rends[i] = rend;
                colors[i] = rend.material.color;

                // Change the material of all hit colliders
                // to use a transparent shader.
                rend.material.shader = Shader.Find("Transparent/Diffuse");
                Color tempColor = rend.material.color;
                tempColor.a = 0.3f;
                rend.material.color = tempColor;
            }
        }

    }
}
