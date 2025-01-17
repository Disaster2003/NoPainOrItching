using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    /// <summary>
    /// �C���X�^���X�̎擾
    /// </summary>
    public static GameManager GetInstance { get { return instance; } }


    private enum STATE_SCENE
    {
        TITLE = 0,  // �^�C�g�����
        PLAY = 1,   // �v���C���
        RESULT = 2, // ���ʉ��
    }
    int buildIndex;

    ActionControll AC; // �C���v�b�g�A�N�V�������`

    // Start is called before the first frame update
    void Start()
    {
        if (instance is null)
        {
            // �C���X�^���X�̏�����
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // ���ɑ��݂��Ă���Ȃ�A
            // ���g��j��
            Destroy(gameObject);
        }

        // �V�[���ԍ��̏�����
        buildIndex = 0;

        AC = new ActionControll(); // �C���v�b�g�A�N�V�������擾
        AC.Player.Decide.performed += OnDecide; // �S�ẴA�N�V�����ɃC�x���g��o�^
        AC.Enable(); // �C���v�b�g�A�N�V�������@�\������ׂɗL��������B
    }

    // Update is called once per frame
    void Update()
    {
        if (buildIndex == (int)STATE_SCENE.PLAY)
        {
            if(PlayerComponent.Hp <= 0)
            {
                // �Q�[���I��
                PlayerComponent.Hp = 1;
                SetScene();
            }
        }
    }

    private void OnDestroy()
    {
        AC.Disable(); // �C���v�b�g�A�N�V������j��
    }

    /// <summary>
    /// Decide�A�N�V�����̓��͎�����
    /// </summary>
    /// <param name="context">����{�^��</param>
    private void OnDecide(InputAction.CallbackContext context)
    {
        switch (buildIndex)
        {
            case (int)STATE_SCENE.TITLE:
            case (int)STATE_SCENE.RESULT:
                SetScene();
                break;
        }
    }

    /// <summary>
    /// �V�[���̐ݒ�
    /// </summary>
    /// <param name="_state_scene">�J�ڐ�</param>
    private void SetScene()
    {
        buildIndex = (buildIndex >= (int)STATE_SCENE.RESULT) ? 0 : buildIndex + 1;
        SceneManager.LoadSceneAsync(buildIndex);
    }
}
