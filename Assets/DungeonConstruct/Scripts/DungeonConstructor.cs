using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class DungeonConstructor : MonoBehaviour
{
    // ダンジョンの構築単位のPrefab
    [SerializeField] private GameObject levelPrefab;
    [SerializeField] private GameObject currentLevel;
    [SerializeField] private GameObject pastLevel = null;
    [SerializeField] private GameObject firstLevel;
    [SerializeField] private Vector3 firstPositionAjustment = new Vector3(0f, 0f, 10f);

    // イージングの種類を定義
    public enum EasingType
    {
        Linear,
        EaseInQuad,
        EaseOutQuad,
        EaseInOutQuad,
        EaseInCubic,
        EaseOutCubic
    }

    /// <summary>
    /// オブジェクトをイージング付きで移動させるコルーチンを開始します。
    /// </summary>
    /// <param name="startPos">移動開始地点</param>
    /// <param name="endPos">終了地点</param>
    /// <param name="duration">移動にかかる時間（秒）</param>
    /// <param name="easing">使用するイージングタイプ</param>
    public void StartMoving(Vector3 startPos, Vector3 endPos, float duration, EasingType easing)
    {
        StartCoroutine(MoveRoutine(startPos, endPos, duration, easing));
    }

    private IEnumerator MoveRoutine(Vector3 start, Vector3 end, float duration, EasingType easing)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            // 進行度 (0.0 ～ 1.0) を計算
            float t = Mathf.Clamp01(elapsedTime / duration);

            // イージング関数を適用して進行度を加工
            float easedT = ApplyEasing(t, easing);

            // 線形補間(Lerp)にイージング後の値を渡す
            transform.position = Vector3.Lerp(start, end, easedT);

            yield return null;
        }

        // 最後に確実に目的地へ配置
        transform.position = end;
        Destroy(pastLevel);
    }

    /// <summary>
    /// 01の進行度に対してイージング数式を適用します
    /// </summary>
    private float ApplyEasing(float t, EasingType type)
    {
        switch (type)
        {
            case EasingType.Linear:
                return t;
            case EasingType.EaseInQuad:
                return t * t;
            case EasingType.EaseOutQuad:
                return t * (2f - t);
            case EasingType.EaseInOutQuad:
                return t < 0.5f ? 2f * t * t : -1f + (4f - 2f * t) * t;
            case EasingType.EaseInCubic:
                return t * t * t;
            case EasingType.EaseOutCubic:
                return 1f - Mathf.Pow(1f - t, 3f);
            default:
                return t;
        }
    }

    public void ConstructDungeon()
    {
        // ダンジョン構築のロジックをここに実装
        // 例: levelPrefabを複数配置してダンジョンを作成するなど
        pastLevel = currentLevel;
        currentLevel = Instantiate(levelPrefab, currentLevel.transform.position + Vector3.forward * 10f, Quaternion.identity);
    }

    // インスペクターからテストするためのサンプルコード
    [ContextMenu("Test Move")]
    public void TestMove()
    {
        Vector3 start = transform.position;
        Vector3 end = start + Vector3.forward * 5f;
        StartMoving(start, end, 2.0f, EasingType.EaseInOutQuad);
    }

    public void ProceedDungeon()
    {
        // ダンジョンの進行ロジックをここに実装
        // 例: プレイヤーが特定の位置に到達したら次のレベルを生成するなど
        if (firstLevel) Destroy(firstLevel);
        ConstructDungeon();
        Vector3 nextPoint = new Vector3(transform.position.x, transform.position.y, currentLevel.transform.position.z + 5f);
        StartMoving(transform.position, nextPoint, 2.0f, EasingType.EaseInOutQuad);
    }

    private void Start()
    {
        firstLevel = Instantiate(levelPrefab, transform.position + firstPositionAjustment + Vector3.back * 5f, Quaternion.identity);
        currentLevel = Instantiate(levelPrefab, transform.position + firstPositionAjustment, Quaternion.identity);
        ProceedDungeon();
    }
}
