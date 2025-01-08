using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    private enum STATE_SCENE
    {
        TITLE = 0,  // �^�C�g��
        PLAY = 1,   // �v���C
        RESULT = 2, // ���U���g
    }
    private STATE_SCENE state_scene;

    ActionControll AC; // �C���v�b�g�A�N�V�������`

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
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

        // ��Ԃ̏�����
        state_scene = STATE_SCENE.TITLE;

        AC = new ActionControll(); // �C���v�b�g�A�N�V�������擾
        AC.Player.Decide.started += OnDecide; // �S�ẴA�N�V�����ɃC�x���g��o�^
        AC.Enable(); // �C���v�b�g�A�N�V�������@�\������ׂɗL��������B
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// �C���X�^���X�̎擾
    /// </summary>
    public static GameManager GetInstance { get { return instance; } }

    /// <summary>
    /// ���菈��
    /// </summary>
    /// <param name="context">����{�^��</param>
    private void OnDecide(InputAction.CallbackContext context)
    {
        switch (state_scene)
        {
            case STATE_SCENE.TITLE:
                SetScene(STATE_SCENE.PLAY);
                break;
            case STATE_SCENE.PLAY:
                SetScene(STATE_SCENE.RESULT);
                break;
            case STATE_SCENE.RESULT:
                SetScene(STATE_SCENE.TITLE);
                break;
        }
    }

    /// <summary>
    /// �V�[���̐ݒ�
    /// </summary>
    /// <param name="_state_scene">�J�ڐ�</param>
    private void SetScene(STATE_SCENE _state_scene)
    {
        state_scene = _state_scene;
        SceneManager.LoadSceneAsync((int)state_scene);
    }
}
