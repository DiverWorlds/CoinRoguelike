using UnityEngine;

public class InventoryOpenHander : MonoBehaviour
{
    private bool isFirstEnable = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        if (isFirstEnable)
        {
            isFirstEnable = false;
            return;
        }
        Logger.Log("Canvas enabled");
        InventoryManager.Instance.OnInventoryOpened();
    }
    void Start()
    {
        Logger.Log("Canvas enabled");
        InventoryManager.Instance.OnInventoryOpened();   
    }
}
