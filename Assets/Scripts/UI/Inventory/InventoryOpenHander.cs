using UnityEngine;

public class InventoryOpenHander : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        Logger.Log("Canvas enabled");
        InventoryManager.Instance.OnInventoryOpened();
    }
}
