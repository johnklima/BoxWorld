using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardDoor : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject TheDoorToOpen;
    private Animator animator;

    void Start()
    {
        animator = TheDoorToOpen.GetComponent<Animator>();
    }


    private void OnTriggerEnter(Collider other)
    {
        bool isOpen = animator.GetBool("OpenDoor");
        bool isClosed = animator.GetBool("CloseDoor");

        Debug.Log(other.name);

        if (isOpen == true && isClosed == false)
            animator.SetBool("CloseDoor", true);

        if (isOpen == false)
            animator.SetBool("OpenDoor", true);

    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log(other.name);

        bool isOpen = animator.GetBool("OpenDoor");
        bool isClosed = animator.GetBool("CloseDoor");

        if(isOpen && isClosed)
        {
            animator.SetBool("CloseDoor", false);
            animator.SetBool("OpenDoor", false);

        }

    }
}
