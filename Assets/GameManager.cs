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
    int buildIndex;

    ActionControll AC; // インプットアクションを定義

    // Start is called before the first frame update
    void Start()
    {
        if (instance is null)
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

        // シーン番号の初期化
        buildIndex = 0;

        AC = new ActionControll(); // インプットアクションを取得
        AC.Player.Decide.performed += OnDecide; // 全てのアクションにイベントを登録
        AC.Enable(); // インプットアクションを機能させる為に有効化する。
    }

    // Update is called once per frame
    void Update()
    {
        if (buildIndex == (int)STATE_SCENE.PLAY)
        {
            if(PlayerComponent.Hp <= 0)
            {
                // ゲーム終了
                PlayerComponent.Hp = 1;
                SetScene();
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
        switch (buildIndex)
        {
            case (int)STATE_SCENE.TITLE:
            case (int)STATE_SCENE.RESULT:
                SetScene();
                break;
        }
    }

    /// <summary>
    /// シーンの設定
    /// </summary>
    /// <param name="_state_scene">遷移先</param>
    private void SetScene()
    {
        buildIndex = (buildIndex >= (int)STATE_SCENE.RESULT) ? 0 : buildIndex + 1;
        SceneManager.LoadSceneAsync(buildIndex);
    }
}
