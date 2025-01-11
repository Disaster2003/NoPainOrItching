using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blink : MonoBehaviour
{
    private Image imgStart;

    [SerializeField, Header("点滅周期倍率")]
    private float blinkCycle = 0.75f;

    // Start is called before the first frame update
    void Start()
    {
        // コンポーネントの取得
        imgStart = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        // 点滅
        imgStart.color = Color.Lerp(Color.clear, Color.white, Mathf.PingPong(blinkCycle * Time.time, 1.0f));
    }
}
