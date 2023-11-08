using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public GameObject door; // Объект двери
    public float slideDistance = -3.15f; // Дистанция, на которую должна сдвинуться дверь
    public float slideSpeed = 1.0f; // Скорость сдвига двери

    private Vector3 _closedPosition; // Закрытое положение двери
    private Vector3 _openPosition; // Открытое положение двери
    private bool _isOpen = false; // Состояние двери

    private void Start()
    {
        _closedPosition = door.transform.position;
        _openPosition = _closedPosition + new Vector3(0, 0, slideDistance);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!_isOpen && other.CompareTag("Player"))
        {
            _isOpen = true;
            StartCoroutine(SlideDoor(_openPosition));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (_isOpen && other.CompareTag("Player"))
        {
            _isOpen = false;
            StartCoroutine(SlideDoor(_closedPosition));
        }
    }

    private IEnumerator SlideDoor(Vector3 targetPosition)
    {
        while (Vector3.Distance(door.transform.position, targetPosition) > 0.01f)
        {
            door.transform.position = Vector3.MoveTowards(door.transform.position, targetPosition, slideSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
