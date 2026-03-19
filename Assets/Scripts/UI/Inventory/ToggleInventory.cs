using UnityEngine;
using UnityEngine.UI;

public class ToggleInventory : MonoBehaviour
{
    [SerializeField] private Canvas inventoryCanvas;
    [SerializeField] private Button button;
    [SerializeField] private Image buttonImage;
    [SerializeField] private bool isOpenButton = false;

    void Start()
    {
        button.onClick.AddListener(OnClick);
    }
    void Update()
    {
        if (isOpenButton) buttonImage.enabled = !inventoryCanvas.gameObject.activeSelf;
    }

    public void OnClick()
    {
        inventoryCanvas.gameObject.SetActive(isOpenButton);
    }
}