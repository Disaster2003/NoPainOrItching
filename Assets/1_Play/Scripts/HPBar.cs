using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    private Image imgHeart;
    [SerializeField] private Sprite[] spriteHeartsNormal;
    [SerializeField] private Sprite[] spriteHeartsBreak;
    private float intervalAnimation;

    private enum INDEX_HP
    {
        NONE,
        ONE,
        TWO,
        THREE,
        FOUR,
        FIVE,
    }
    [SerializeField] private INDEX_HP index_hp;

    // Start is called before the first frame update
    void Start()
    {
        // �R���|�[�l���g�̎擾
        imgHeart = GetComponent<Image>();

        intervalAnimation = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // null�`�F�b�N
        if(imgHeart is null)
        {
            Debug.LogError("�R���|�[�l���g�����擾�ł�");
            return;
        }
        if(index_hp == INDEX_HP.NONE)
        {
            Debug.LogError("HP�ԍ������ݒ�ł�");
            return;
        }

        // �A�j���[�V�����̍Đ�
        if (PlayerComponent.Hp < (int)index_hp) HeartAnimation(spriteHeartsBreak);
        else HeartAnimation(spriteHeartsNormal);
    }

    /// <summary>
    /// �n�[�g�̃A�j���[�V����
    /// </summary>
    /// <param name="spriteHearts">�Đ�����A�j���[�V�����摜</param>
    private void HeartAnimation(Sprite[] spriteHearts)
    {
        if (intervalAnimation < 0.2f)
        {
            // �A�j���[�V�����̃C���^�[�o����
            intervalAnimation += Time.deltaTime;
            return;
        }

        // �A�j���[�V����
        intervalAnimation = 0;
        for (int i = 0; i < spriteHearts.Length; i++)
        {
            if (imgHeart.sprite == spriteHearts[i])
            {
                if (i == spriteHearts.Length - 1)
                {
                    if (spriteHearts == spriteHeartsBreak)
                    {
                        // ���g�̔j��
                        Destroy(gameObject);
                    }

                    // �ŏ��̉摜�ɖ߂�
                    imgHeart.sprite = spriteHearts[0];
                    return;
                }
                else
                {
                    // ���̉摜��
                    imgHeart.sprite = spriteHearts[i + 1];
                    return;
                }
            }
            else if (i == spriteHearts.Length - 1)
            {
                // �摜��ύX����
                imgHeart.sprite = spriteHearts[0];
                return;
            }
        }
    }
}
