using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SideRecordsManager : MonoBehaviour
{
    [SerializeField] private SideRecord sideRecordPrefab;
    [SerializeField] private SidePanel sidePanel;
    [SerializeField] private SidesPanel sidesPanel;
    private SideRecord selectedRecord;
    public enum SortType { Strength, Weight }
    private List<SideRecord> records = new List<SideRecord>();
    public SideRecord SelectedRecord { get; set; }

    void Start()
    {
        Logger.Log("SideRecordsManager Start");
        //TODO: Debug
        for (int i = 0; i < 10; i++)
        {
            BaseSide newSide = Instantiate(sidesPanel.SidesServer.GetRandomSide(1), InventoryManager.Instance.SideInventory.transform).GetComponent<BaseSide>();
            newSide.Initialize(1, true);
            Logger.Log($"side: {newSide.EffectName}");
            InventoryManager.Instance.SideInventory.Add(newSide);
        }
        CreateAllRecords();
    }

    //TODO: 呼ぶ。Removeも
    public void CreateRecord(BaseSide side)
    {
        Logger.Log($"CreateRecord: {side.EffectName}");
        var record = Instantiate(sideRecordPrefab, transform, false);
        record.transform.localScale = sideRecordPrefab.transform.localScale;
        record.Initialize(side, this, sidesPanel);
        records.Add(record);
    }
    public void CreateAllRecords()
    {
        Logger.Log("CreateAllRecords");
        Logger.LogElements(InventoryManager.Instance.SideInventory.GetByFrontOrBack(sidePanel.FrontOrBack).Select(s => s.EffectName));
        foreach (var side in InventoryManager.Instance.SideInventory.GetByFrontOrBack(sidePanel.FrontOrBack))
        {
            CreateRecord(side);
        }
    }
    public void SelectRecord(SideRecord record)
    {
        Logger.Log($"SelectRecord: {record.Side.EffectName}");
        selectedRecord?.ToggleImageVisualize(false);
        selectedRecord = record;
        selectedRecord.ToggleImageVisualize(true);
    }
    public void SortChildren(SortType type, bool ascending = true)
    {
        // 1. 子要素の Transform と、そこについているデータ用コンポーネントをペアで取得
        var children = transform.Cast<Transform>()
            .Select(t => new { Trans = t, Data = t.GetComponent<SideRecord>() })
            .Where(x => x.Data != null); // ItemDataがないものは除外

        // 2. EffectName を常に昇順で優先し、同値時に指定タイプで並び替え
        IEnumerable<Transform> sorted;
        if (type == SortType.Strength)
        {
            sorted = ascending
            ? children.OrderBy(x => x.Data.Side.EffectName).ThenBy(x => x.Data.Side.Strength).Select(x => x.Trans)
            : children.OrderBy(x => x.Data.Side.EffectName).ThenByDescending(x => x.Data.Side.Strength).Select(x => x.Trans);
        }
        else
        {
            sorted = ascending
            ? children.OrderBy(x => x.Data.Side.EffectName).ThenBy(x => x.Data.Side.Weight).Select(x => x.Trans)
            : children.OrderBy(x => x.Data.Side.EffectName).ThenByDescending(x => x.Data.Side.Weight).Select(x => x.Trans);
        }

        // 3. Hierarchy上のインデックスを再設定
        var sortedList = sorted.ToList();
        for (int i = 0; i < sortedList.Count; i++)
        {
            sortedList[i].SetSiblingIndex(i);
        }

        Debug.Log($"{type} でソートしました（{(ascending ? "昇順" : "降順")}）");
    }
    public void RemoveRecord(SideRecord record)
    {
        Logger.Log($"RemoveRecord: {record.Side.EffectName}");
        if (records.Contains(record))
        {
            records.Remove(record);
            Destroy(record.gameObject);
        }
    }
    public void RemoveAllRecords()
    {
        Logger.Log("RemoveAllRecords");
        foreach (var record in records)
        {
            RemoveRecord(record);
        }
    }
}