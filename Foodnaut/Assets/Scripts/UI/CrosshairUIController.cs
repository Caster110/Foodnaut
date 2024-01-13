using UnityEngine;
using UnityEngine.UI;

public class CrosshairUIController : MonoBehaviour
{
    [SerializeField] private Image crosshairImage;
    private bool isWhite = true;

    public void TryChangeColor(bool triggerIsInteractableObject)
    {
        if (isWhite && triggerIsInteractableObject)
            crosshairImage.color = Color.yellow;
        else if (!isWhite && !triggerIsInteractableObject)
            crosshairImage.color = Color.white;
        else
            return;
        isWhite = !isWhite;
    }
}
