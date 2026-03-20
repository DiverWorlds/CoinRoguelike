using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SideRecordsManager : MonoBehaviour
{
    public enum SortType { Strength, Weight }
    private List<SideRecord> records = new List<SideRecord>();
    void Start()
    {

    }

    public void CreateRecords(List<BaseSide> sides)
    {
        // 新しいレコードを作成
        foreach (var side in sides)
        {
            var recordObj = new GameObject("SideRecord");
            recordObj.transform.SetParent(transform);
            var record = recordObj.AddComponent<SideRecord>();
            record.Initialize(side);
            records.Add(record);
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
    public void RemoveAllRecords()
    {
        foreach (var record in records)
        {
            Destroy(record.gameObject);
        }
        records.Clear();
    }
}