using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    /// <summary>
    /// インスタンスの取得
    /// </summary>
    public static GameManager GetInstance { get { return instance; } }


    private enum STATE_SCENE
    {
        TITLE = 0,  // タイトル画面
        PLAY = 1,   // プレイ画面
        RESULT = 2, // 結果画面
    }
    private STATE_SCENE state_scene;

    ActionControll AC; // インプットアクションを定義

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            // インスタンスの初期化
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // 他に存在しているなら、
            // 自身を破棄
            Destroy(gameObject);
        }

        // 状態の初期化
        state_scene = STATE_SCENE.TITLE;

        AC = new ActionControll(); // インプットアクションを取得
        AC.Player.Decide.started += OnDecide; // 全てのアクションにイベントを登録
        AC.Enable(); // インプットアクションを機能させる為に有効化する。
    }

    // Update is called once per frame
    void Update()
    {
        if (state_scene == STATE_SCENE.PLAY)
        {
            if(!GameObject.FindGameObjectWithTag("Player"))
            {
                // ゲーム終了
                SetScene(STATE_SCENE.RESULT);
            }
        }
    }

    private void OnDestroy()
    {
        AC.Disable(); // インプットアクションを破棄
    }

    /// <summary>
    /// Decideアクションの入力時処理
    /// </summary>
    /// <param name="context">決定ボタン</param>
    private void OnDecide(InputAction.CallbackContext context)
    {
        switch (state_scene)
        {
            case STATE_SCENE.TITLE:
                SetScene(STATE_SCENE.PLAY);
                break;
            case STATE_SCENE.PLAY:
                break;
            case STATE_SCENE.RESULT:
                SetScene(STATE_SCENE.TITLE);
                break;
        }
    }

    /// <summary>
    /// シーンの設定
    /// </summary>
    /// <param name="_state_scene">遷移先</param>
    private void SetScene(STATE_SCENE _state_scene)
    {
        state_scene = _state_scene;
        SceneManager.LoadSceneAsync((int)state_scene);
    }
}
