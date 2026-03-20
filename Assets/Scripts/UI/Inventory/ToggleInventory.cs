using UnityEngine;
using UnityEngine.UI;

public class ToggleInventory : MonoBehaviour
{
    [SerializeField] private Canvas inventoryCanvas;
    [SerializeField] private Button button;
    [SerializeField] private Image buttonImage;
    [SerializeField] private bool isOpenButton = false;
    [SerializeField] private ToggleInventory closeToggleInventory;
    private GameObject inventoryCanvasObject;

    void Start()
    {
        button.onClick.AddListener(OnClick);
        inventoryCanvasObject = inventoryCanvas.gameObject;
    }
    void Update()
    {
        if (isOpenButton) buttonImage.enabled = !inventoryCanvasObject.activeSelf;
    }

    public void OnClick()
    {
        inventoryCanvas.gameObject.SetActive(isOpenButton);
        // if (isOpenButton)
        // {
        //     buttonImage.enabled = false;
        // }
    }
}