using UnityEngine;
using UnityEngine.UI;

public class CrosshairUIController : MonoBehaviour
{
    [SerializeField] private Image crosshairImage;
    private bool isWhite = true;

    public void TryChangeColor(bool isInteractableObject)
    {
        if (isWhite && isInteractableObject)
            crosshairImage.color = Color.yellow;
        else if (!isWhite && !isInteractableObject)
            crosshairImage.color = Color.white;
        else
            return;
        isWhite = !isWhite;
    }
}
