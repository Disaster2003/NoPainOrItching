using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBackGround : MonoBehaviour
{
    private Material matBackground;

    // Start is called before the first frame update
    void Start()
    {
        // �R���|�[�l���g�̎擾
        matBackground = GetComponent<SpriteRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if (matBackground is null)
        {
            Debug.LogError("�R���|�[�l���g�����擾�ł�");
            return;
        }

        // �w�i�̃X�N���[��
        Vector2 offset = matBackground.mainTextureOffset + new Vector2(0.1f * Time.deltaTime, 0);
        matBackground.SetTextureOffset("_MainTex", offset);
    }
}
