using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using unityroom.Api;

public class SetTime : MonoBehaviour
{
    [SerializeField] Image[] imgNumber;
    [SerializeField] Sprite[] sNumber;

    // Start is called before the first frame update
    void Start()
    {
        // �^�C�����擾
        int time = (int)Timer.GetTimer;

        // ���ꂼ��̌��̐������摜��
        int hundreds = time / 100;
        int tens = (time % 100) / 10;
        int ones = time % 10;
        imgNumber[0].sprite = sNumber[hundreds];
        imgNumber[1].sprite = sNumber[tens];
        imgNumber[2].sprite = sNumber[ones];

        // �{�[�hNo1�Ƀ^�C���𑗐M����B
        UnityroomApiClient.Instance.SendScore(1, time, ScoreboardWriteMode.HighScoreAsc);
    }
}
