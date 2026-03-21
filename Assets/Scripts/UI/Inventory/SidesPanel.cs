using UnityEngine;

public class SidesPanel : MonoBehaviour
{
    [SerializeField] private SidePanel frontPanel;
    [SerializeField] private SidePanel backPanel;
    private FrontAndBack currentOpenPanel = FrontAndBack.Front;
    public FrontAndBack FrontOrBack => currentOpenPanel;

    public void FlipPanel()
    {
        frontPanel.gameObject.SetActive(!frontPanel.gameObject.activeSelf);
        Logger.Log($"FlipPanel: frontPanel active={frontPanel.gameObject.activeSelf}");
        backPanel.gameObject.SetActive(!backPanel.gameObject.activeSelf);
        Logger.Log($"FlipPanel: backPanel active={backPanel.gameObject.activeSelf}");
        currentOpenPanel = currentOpenPanel == FrontAndBack.Front ? FrontAndBack.Back : FrontAndBack.Front;
    }
    public SidePanel GetCurrentPanel()
    {
            return currentOpenPanel == FrontAndBack.Front ? frontPanel : backPanel;
    }
}
