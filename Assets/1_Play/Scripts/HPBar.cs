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
        // コンポーネントの取得
        imgHeart = GetComponent<Image>();

        intervalAnimation = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // nullチェック
        if(imgHeart is null)
        {
            Debug.LogError("コンポーネントが未取得です");
            return;
        }
        if(index_hp == INDEX_HP.NONE)
        {
            Debug.LogError("HP番号が未設定です");
            return;
        }

        // アニメーションの再生
        if (PlayerComponent.Hp < (int)index_hp) HeartAnimation(spriteHeartsBreak);
        else HeartAnimation(spriteHeartsNormal);
    }

    /// <summary>
    /// ハートのアニメーション
    /// </summary>
    /// <param name="spriteHearts">再生するアニメーション画像</param>
    private void HeartAnimation(Sprite[] spriteHearts)
    {
        if (intervalAnimation < 0.2f)
        {
            // アニメーションのインターバル中
            intervalAnimation += Time.deltaTime;
            return;
        }

        // アニメーション
        intervalAnimation = 0;
        for (int i = 0; i < spriteHearts.Length; i++)
        {
            if (imgHeart.sprite == spriteHearts[i])
            {
                if (i == spriteHearts.Length - 1)
                {
                    if (spriteHearts == spriteHeartsBreak)
                    {
                        // 自身の破棄
                        Destroy(gameObject);
                    }

                    // 最初の画像に戻す
                    imgHeart.sprite = spriteHearts[0];
                    return;
                }
                else
                {
                    // 次の画像へ
                    imgHeart.sprite = spriteHearts[i + 1];
                    return;
                }
            }
            else if (i == spriteHearts.Length - 1)
            {
                // 画像を変更する
                imgHeart.sprite = spriteHearts[0];
                return;
            }
        }
    }
}
