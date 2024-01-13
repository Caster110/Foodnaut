using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ActivateCraftStation : MonoBehaviour
{
    [SerializeField] private RaycastDetector raycastDetector;
    [SerializeField] private CraftStationUI craftStationUI;
    private GameObject craftStation;
    private bool isActivated = false;
    private void Update()
    {
        if (craftStation = raycastDetector.GetDetectedObjectWithTag("CraftStation"))
            TrySwitchMode(craftStation);
    }

    private void TrySwitchMode(GameObject target)
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Transform craftStationCameraPosition = target.transform.GetChild(0);
            isActivated = !isActivated;
            Debug.Log(craftStationCameraPosition.name);
            if (isActivated)
            {
                Debug.Log("это плита");
                craftStationUI.On(craftStationCameraPosition);
            }
            else
            {
                Debug.Log("это неплита");
                craftStationUI.Off();
            }
        } 
    }
}
