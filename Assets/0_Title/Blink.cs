using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blink : MonoBehaviour
{
    private Image imgStart;

    [SerializeField, Header("�_�Ŏ����{��")]
    private float blinkCycle = 0.75f;

    // Start is called before the first frame update
    void Start()
    {
        // �R���|�[�l���g�̎擾
        imgStart = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        // �_��
        imgStart.color = Color.Lerp(Color.clear, Color.white, Mathf.PingPong(blinkCycle * Time.time, 1.0f));
    }
}
