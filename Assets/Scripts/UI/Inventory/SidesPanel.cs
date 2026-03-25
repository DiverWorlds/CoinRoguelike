using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SidesPanel : MonoBehaviour
{
    [SerializeField] private CoinDetail coinDetail;
    [SerializeField] private SidePanel frontPanel;
    [SerializeField] private SidePanel backPanel;
    [SerializeField] private CoinFactory coinFactory;
    [SerializeField] private SidesServer sidesServer;
    private FrontAndBack currentOpenPanel = FrontAndBack.Front;
    public FrontAndBack FrontOrBack => currentOpenPanel;
    private List<SideRecord> selectedRecords = new List<SideRecord>();
    public SidesServer SidesServer => sidesServer;

    public void FlipPanel()
    {
        frontPanel.gameObject.SetActive(!frontPanel.gameObject.activeSelf);
        Logger.Log($"FlipPanel: frontPanel active={frontPanel.gameObject.activeSelf}");
        backPanel.gameObject.SetActive(!backPanel.gameObject.activeSelf);
        Logger.Log($"FlipPanel: backPanel active={backPanel.gameObject.activeSelf}");
        currentOpenPanel = currentOpenPanel == FrontAndBack.Front ? FrontAndBack.Back : FrontAndBack.Front;
    }
    public void AddSelectedRecord(SideRecord record)
    {
        Logger.Log($"AddSelectedRecord: {record.Side.EffectName}");

        selectedRecords.RemoveAll(r => r == null || r.Side == null);

        var sameSideRecord = selectedRecords.FirstOrDefault(r => r.Side.FrontOrBack == record.Side.FrontOrBack);
        if (sameSideRecord != null)
        {
            selectedRecords.Remove(sameSideRecord);
        }

        selectedRecords.Add(record);
        Logger.LogElements($"current selected records", selectedRecords.Select(r => r.Side.EffectName));
        Logger.Log($"SelectedRecords count: {selectedRecords.Count}");
        if (selectedRecords.Count == 2)
        {
            SetPreview();
        }
    }
    public SidePanel GetCurrentPanel()
    {
            return currentOpenPanel == FrontAndBack.Front ? frontPanel : backPanel;
    }
    private void SetPreview()
    {
        coinDetail.Coin = coinFactory.CreatePreviewCoin(selectedRecords[0], selectedRecords[1]);
    }
}
