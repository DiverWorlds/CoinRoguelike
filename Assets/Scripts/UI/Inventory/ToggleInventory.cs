using UnityEngine;
using UnityEngine.UI;

public class ToggleInventory : MonoBehaviour
{
    [SerializeField] private Canvas inventoryCanvas;
    [SerializeField] private Button button;
    [SerializeField] private Image buttonImage;
    [SerializeField] private bool isOpenButton = false;
    [SerializeField] private ToggleInventory closeToggleInventory;
    [SerializeField] private SideRecordsManager frontSideRecordsManager;
    [SerializeField] private SideRecordsManager backSideRecordsManager;
    private GameObject inventoryCanvasObject;

    void Start()
    {
        button.onClick.AddListener(OnClick);
        inventoryCanvasObject = inventoryCanvas.gameObject;
    }
    void Update()
    {
        if (!isOpenButton)
        {
            return;
        }

        if (inventoryCanvasObject == null)
        {
            inventoryCanvasObject = inventoryCanvas != null ? inventoryCanvas.gameObject : null;
        }

        if (inventoryCanvasObject == null)
        {
            return;
        }

        buttonImage.enabled = !inventoryCanvasObject.activeSelf;
    }

    public void OnClick()
    {
        if (inventoryCanvas == null)
        {
            return;
        }

        inventoryCanvas.gameObject.SetActive(isOpenButton);
        if (!isOpenButton)
        {
            frontSideRecordsManager.UnselectRecord();
            backSideRecordsManager.UnselectRecord();
        }
    }
}