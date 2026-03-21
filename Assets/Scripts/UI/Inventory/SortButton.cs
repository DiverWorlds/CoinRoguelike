using UnityEngine;
using UnityEngine.UI;

public class SortButton : MonoBehaviour
{
    [SerializeField] private SideRecordsManager.SortType sortType;
    [SerializeField] private Button button;
    [SerializeField] private SideRecordsManager sideRecordsManager;
    void Start()
    {
        button.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        Logger.Log("SortButton clicked");
        sideRecordsManager.SortChildren(sortType, ascending: true);
    }
}