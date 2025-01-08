using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private static float timer;

    [SerializeField] private GameObject spawner;
    [SerializeField] private GameObject boss;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // nullチェック
        if (spawner != null || boss == null) return;

        // 時間計測
        timer += Time.deltaTime;
    }

    /// <summary>
    /// タイマーの取得
    /// </summary>
    public static float GetTimer { get { return timer; } }
}
