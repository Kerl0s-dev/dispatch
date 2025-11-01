using UnityEngine;

public class Door : MonoBehaviour
{
    Animator Animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Animator = GetComponent<Animator>();
        Animator.Play("Door_Close");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Animator.Play("Door_Open");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Animator.Play("Door_Close");
        }
    }
}
