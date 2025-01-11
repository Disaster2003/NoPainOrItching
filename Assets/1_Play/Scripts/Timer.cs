using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private static float timer;
    /// <summary>
    /// タイマーの取得
    /// </summary>
    public static float GetTimer { get { return timer; } }

    [SerializeField] private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // nullチェック
        if (player == null) return;

        // 時間計測
        timer += Time.deltaTime;
    }
}
