using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SideRecordsManager : MonoBehaviour
{
    [SerializeField] private SideRecord sideRecordPrefab;
    public enum SortType { Strength, Weight }
    private List<SideRecord> records = new List<SideRecord>();
    void Start()
    {

    }

    public void CreateRecord(BaseSide side)
    {
        var recordObj = Instantiate(sideRecordPrefab.gameObject);
        recordObj.transform.SetParent(transform);
        var record = recordObj.GetComponent<SideRecord>();
        record.Initialize(side);
        records.Add(record);
    }
    public void CreateAllRecords()
    {
        foreach (var side in InventoryManager.Instance.SideInventory.GetByFrontOrBack(SidePanel.Instance.FrontOrBack))
        {
            CreateRecord(side);
        }
    }
    public void SortChildren(SortType type, bool ascending = true)
    {
        // 1. 子要素の Transform と、そこについているデータ用コンポーネントをペアで取得
        var children = transform.Cast<Transform>()
            .Select(t => new { Trans = t, Data = t.GetComponent<BaseSide>() })
            .Where(x => x.Data != null); // ItemDataがないものは除外

        // 2. 指定されたタイプに応じて並び替え
        IEnumerable<Transform> sorted;
        if (type == SortType.Strength)
        {
            sorted = ascending ? children.OrderBy(x => x.Data.Strength).Select(x => x.Trans)
                               : children.OrderByDescending(x => x.Data.Strength).Select(x => x.Trans);
        }
        else
        {
            sorted = ascending ? children.OrderBy(x => x.Data.Weight).Select(x => x.Trans)
                               : children.OrderByDescending(x => x.Data.Weight).Select(x => x.Trans);
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
        if (records.Contains(record))
        {
            records.Remove(record);
            Destroy(record.gameObject);
        }
    }
    public void RemoveAllRecords()
    {
        foreach ( var record in records)
        {
            RemoveRecord(record);
        }
    }
}