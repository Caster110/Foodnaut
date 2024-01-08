using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField] private GameObject doorObject;
    [SerializeField] private float slideDistance = -3.15f;
    [SerializeField] private float slideSpeed = 1.2f; 
    
    private bool isOpen = false;
    private Vector3 closedPosition;
    private Vector3 openPosition;

    private void Start()
    {
        closedPosition = doorObject.transform.position;
        openPosition = closedPosition + new Vector3(0, 0, slideDistance);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isOpen == false && other.CompareTag("Player"))
        {
            isOpen = true;
            StartCoroutine(SlideDoor(openPosition));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (isOpen && other.CompareTag("Player"))
        {
            isOpen = false;
            StartCoroutine(SlideDoor(closedPosition));
        }
    }

    private IEnumerator SlideDoor(Vector3 targetPosition)
    {
        while (Vector3.Distance(doorObject.transform.position, targetPosition) > 0.01f)
        {
            doorObject.transform.position = Vector3.MoveTowards(doorObject.transform.position, targetPosition, slideSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
