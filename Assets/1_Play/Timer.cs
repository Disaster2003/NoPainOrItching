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
        // null�`�F�b�N
        if (spawner != null || boss == null) return;

        // ���Ԍv��
        timer += Time.deltaTime;
    }

    /// <summary>
    /// �^�C�}�[�̎擾
    /// </summary>
    public static float GetTimer { get { return timer; } }
}
