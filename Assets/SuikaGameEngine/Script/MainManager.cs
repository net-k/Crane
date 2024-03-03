using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using OdenGame.Presentation;
using PanGame.Presentation;
using PanGame.Presentation.Menu;
using Quiz.Framework.SupportScene;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using Zenject;
using Random = UnityEngine.Random;

public class MainManager : MonoBehaviour
{
    [SerializeField] BreadController[] republicControllerPrefabs;

    int masterIDNumber = 0;
    int nextRepublic;

    BreadController nowBreadController;
    BreadController nextBreadController;

    Vector3 mouseWorldPos;
    float nowRepublicPosition;

    private const float nowBreadAutoBaseLimit = 1.8f;// 2.3f;// 3.2f;
    // float nowBreadAutoLimit = 3.2f;
    float nowBreadAutoLimit = 2.3f;

    float dropTimeElapsed = 0f;
    float dropTimeOut = 1f; //落としてから次のパンを落とせるようになるまでの時間

    [SerializeField] Text _scoreText;
    [SerializeField] Text _recordScoreText;
    
    int score = 0;

    [SerializeField] GameFinishPresenter gameOverUI = null;
    [SerializeField]
    private NextBreadPresenter _nextBreadPresenter = null;

    [FormerlySerializedAs("uiManager")] [SerializeField] 
    UIManager _uiManager = null;
    
    [SerializeField]
    private SupportSceneButton _settingsButton = null;

    [SerializeField]
    private MenuButton _menuButton = null;

    public struct BreadPiece
    {
        public BreadController breadController;
        public int masterIDNumber;
        public int hitRepublicIDNumber; //-100:ヒットチェック済み, -1:未ヒット, 0以上:ヒットした相手のIDNumber
    }

    List<MainManager.BreadPiece> breadPieceList = new List<MainManager.BreadPiece>();

    public enum GameState
    {
        MOVE,
        DROP,
        GAMEOVER,
        STOP,
    }
    GameState gameState = GameState.MOVE;
    GameState oldGameState;

    int watermelonCount = 0;
    
    private readonly ItemLogic _itemLogic = new ItemLogic();

    public ItemLogic ItemLogic
    {
        get { return _itemLogic; }
    }

    [Inject]
    void Construct()
    {
        
    }
    
    //---------------------------------------------------------------------------------------------------------------------------------------------------
    void Start()
    {
        DOTween.Init();

        //時間開始
        Time.timeScale = 1;

        //BGM停止
        AudioManager.instance.StopBGM(AudioManager.AudioNo.BGM);
        // AudioManager.instance.StopBGM(1);

        //BGM再生
        AudioManager.instance.PlayBGM(AudioManager.AudioNo.BGM);

        //レコード表示
        #if false
        for(int i = 0; i < recordTMPText.Length; i++)
        {
            recordTMPText[i].SetText(RecordManager.record.ToString());
        }
        #endif
        _scoreText.text = score.ToString();
        _recordScoreText.text = RecordManager.instance.LoadBestScore().ToString();// RecordManager.record.ToString();

        CreateNextBread();

        nowBreadController = nextBreadController;
//        nowBreadAutoLimit = nowBreadAutoBaseLimit - nowBreadController.gameObject.GetComponent<SpriteRenderer>().bounds.size.x * 0.5f;
        nowBreadAutoLimit = nowBreadAutoBaseLimit;
                            // nowBreadController.gameObject.GetComponent<PolygonCollider2D>().bounds.size.x * 0.8f;
       CreateNextBread();
        
        watermelonCount = 0;
        
        // UIの初期表示
        _menuButton.Hide();
        _settingsButton.Show();
    }

    void Update()
    {
        Vector3 mouseScreenPos = Input.mousePosition;
        mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
        nowRepublicPosition = mouseWorldPos.x;
        if(nowRepublicPosition > nowBreadAutoLimit)    nowRepublicPosition = nowBreadAutoLimit;
        if(nowRepublicPosition < -nowBreadAutoLimit)    nowRepublicPosition = -nowBreadAutoLimit;

        switch(gameState)
        {
            case GameState.MOVE:
                MoveState();
                break;

            case GameState.DROP:
                DropState();
                break;

            case GameState.GAMEOVER:
                GameOverState();
                break;

            case GameState.STOP:
                //何もしない
                break;
        }
    }

    void MoveState()
    {
        // Vector3 mousePosition = new Vector3(nowRepublicPosition, 4.25f, 0f);
        Vector3 mousePosition = new Vector3(nowRepublicPosition, 2.55f, 0f);
        nowBreadController.transform.position = mousePosition;
        const float mouseRange = 1.9f;
        
        
        if(     // Input.GetMouseButtonDown(0)
           Input.GetMouseButtonUp(0)
            &&  mouseWorldPos.x > -1 *  mouseRange// -4f
            &&  mouseWorldPos.x < mouseRange )   //マウスクリックした場合
        {   
            /*
            //UIを触ってる場合はスルー
            if(EventSystem.current.IsPointerOverGameObject()){
                return;
            }
            //スマホなどのタッチの場合の判定
            if (Input.touchCount > 0){
                if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)){
                    return;
                }
            }
             */      
            if (!CanDrop()) return;

            //SE再生
            AudioManager.instance.PlaySound(AudioManager.AudioNo.SE_Drop);

            //物理演算を有効化
            nowBreadController.SetRepublicInit();

            //ステート遷移
            gameState = GameState.DROP;
            _settingsButton.Hide();
            _menuButton.Show();
        }
    }

    private bool CanDrop()
    {
        // 何らかのボタンが押された場合は止める
        if (EventSystem.current.currentSelectedGameObject != null)
        {
            return false;
        }

        if (! _uiManager.CanDrop())
        {
            return false;
        }
        
        return true;
    }

    void DropState()
    {
        dropTimeElapsed += Time.deltaTime;
        if(dropTimeElapsed >= dropTimeOut)
        {
            dropTimeElapsed = 0f;

            nowBreadController = nextBreadController;
            // nowBreadAutoLimit = nowBreadAutoBaseLimit - nowBreadController.gameObject.GetComponent<SpriteRenderer>().bounds.size.x * 0.5f;
            nowBreadAutoLimit = nowBreadAutoBaseLimit; // - nowBreadController.gameObject.GetComponent<SpriteRenderer>().bounds.size.x * 0.5f;

            CreateNextBread();

            //ステート遷移
            gameState = GameState.MOVE;
        }
    }

    public void CreateNextBread()
    {
        nextRepublic = GetRandomNext();
        CreateNextRepublic(nextRepublic);
    }
    public void CreateNextBreadWithItem()
    {
        nextRepublic = ItemLogic.GetRandomNextExcept(nextRepublic, GameConstants.RandomNextMaxIndex);
        CreateNextRepublic(nextRepublic);
    }

    void GameOverState()
    {
        //時間停止
        Time.timeScale = 0;

        //ゲームオーバー画面表示
        gameOverUI.Open(); // SetActive(true);

        //レコード更新処理
        if(score > RecordManager.record)    RecordManager.record = score;

        //ステート遷移
        gameState = GameState.STOP;

        //ハイスコアのオンラインランキング更新
        //unityroom向けオンラインランキングの実装方法はこちらを参照してください
        //https://help.unityroom.com/how-to-implement-scoreboard
        //
        //オンラインランキング更新
        //UnityroomApiClient.Instance.SendScore(1, (float)score, ScoreboardWriteMode.HighScoreDesc);
    }

    void ResumeGame()
    {
        //ゲームに戻る処理
        // hammerAndSickleCanvasGroup.gameObject.SetActive(false);

        //時間進行
        Time.timeScale = 1;

        //ステート遷移
        gameState = oldGameState;
    }

    int GetRandomNext()
    {
        return Random.Range(0, /*republicControllerPrefabs.Length*/ GameConstants.RandomNextMaxIndex);
    }

    void CreateNextRepublic(int inputIndex)
    {
        int imageNo = inputIndex + 1;
        _nextBreadPresenter.SetNextBreadImage(imageNo);
        nextBreadController = Instantiate(republicControllerPrefabs[inputIndex], new Vector3(6.25f, 1.5f, 0f), Quaternion.identity);
    }

    public int GetMasterIDNumber(BreadController inputRepublicController)
    {
        masterIDNumber += 1;

        //リストに登録
        MainManager.BreadPiece tmpBreadPiece = new MainManager.BreadPiece();
        tmpBreadPiece.breadController = inputRepublicController;
        tmpBreadPiece.masterIDNumber = masterIDNumber;

        breadPieceList.Add(tmpBreadPiece);

        return masterIDNumber;
    }

    public GameState GetGameState()
    {
        return gameState;
    }

    public void SetGameState(GameState inputGameState)
    {
        gameState = inputGameState;
    }

    public void AddScore(int inputScore)
    {
        score += inputScore;
        _scoreText.text = score.ToString();
    }

    public int GetScore()
    {
        return score;
    }

    public void DeleteRepublicControllerFromList(int inputMasterIDNumber)
    {
        breadPieceList.RemoveAll(where => where.masterIDNumber == inputMasterIDNumber);
    }

    public void SetExplosion(Vector3 explosionOrigin, float explosionForce, float explosionRadius, int exclusionID1, int exclusionID2)
    {
        for(int i = 0; i < breadPieceList.Count; i++)
        {
            if(breadPieceList[i].masterIDNumber != exclusionID1 && breadPieceList[i].masterIDNumber != exclusionID2)
            {
                breadPieceList[i].breadController.AddExplosionForce2D(explosionOrigin, explosionForce, explosionRadius);
            }
        }
    }

    public void AddMakeSuikaCount()
    {
        watermelonCount += 1;
        //オンラインランキング更新
    }

    //---------------------------------------------------------------------------------------------------------------------------------------------------
    // 以下は3個以上同時合体時のバグを修正したもの
    //---------------------------------------------------------------------------------------------------------------------------------------------------
    
    public void SetHitBreadIDNumber(int inputIDNumber, int inputHitRepublicIDNumber)
    {
        for(int i = 0; i < breadPieceList.Count; i++)
        {
            if(breadPieceList[i].masterIDNumber == inputIDNumber)
            {
                MainManager.BreadPiece tmpBreadPiece = breadPieceList[i];
                tmpBreadPiece.hitRepublicIDNumber = inputHitRepublicIDNumber;
                breadPieceList[i] = tmpBreadPiece;
                break;
            }
        }
    }

    void FixedUpdate()
    {
        UpdateMergeAllPieces();
    }

    private void UpdateMergeAllPieces()
    {
        List<int> tmpDeleteIDNumber = new List<int>(); //削除リスト

        for (int i = 0; i < breadPieceList.Count; i++)
        {
            if (breadPieceList[i].hitRepublicIDNumber >= 0)
            {
                bool tmpIsFindPair = false;
                for (int j = 0; j < breadPieceList.Count; j++)
                {
                    if (breadPieceList[j].masterIDNumber == breadPieceList[i].hitRepublicIDNumber
                        && breadPieceList[j].hitRepublicIDNumber >= 0)
                    {
                        tmpIsFindPair = MergePiece(i, j, tmpDeleteIDNumber);

                        break;
                    }
                }

                if (tmpIsFindPair == false)
                {
                    MainManager.BreadPiece tmpBreadPiece;
                    tmpBreadPiece = breadPieceList[i];
                    tmpBreadPiece.hitRepublicIDNumber = -1;
                    breadPieceList[i] = tmpBreadPiece;
                }
            }
        }

        //MainManagerのリストから削除とGameObjectの破棄
        DeleteGameObject(tmpDeleteIDNumber);
    }

    private bool MergePiece(int i, int j, List<int> tmpDeleteIDNumber)
    {
        bool tmpIsFindPair;
        MainManager.BreadPiece tmpBreadPiece;
        tmpBreadPiece = breadPieceList[i];
        tmpBreadPiece.hitRepublicIDNumber = -100;
        breadPieceList[i] = tmpBreadPiece;

        tmpBreadPiece = breadPieceList[j];
        tmpBreadPiece.hitRepublicIDNumber = -100;
        breadPieceList[j] = tmpBreadPiece;

        //削除リストに追加
        tmpDeleteIDNumber.Add(breadPieceList[i].masterIDNumber);
        tmpDeleteIDNumber.Add(breadPieceList[j].masterIDNumber);

        tmpIsFindPair = true;

        //パンのマージ
        Vector3 tmpPosition = (breadPieceList[i].breadController.gameObject.transform.position +
                               breadPieceList[j].breadController.gameObject.transform.position) * 0.5f; //2Republicの中間座標を算出

        if (breadPieceList[i].breadController.GetRepublicNmber() == GameConstants.MaxBreadNumber - 1) AddMakeSuikaCount();

        //合体エフェクト生成
        breadPieceList[i].breadController.SetExplosionEffect(tmpPosition);
        SetExplosion(this.transform.position, 450f, 2f, breadPieceList[i].masterIDNumber, breadPieceList[j].masterIDNumber);

        if (breadPieceList[i].breadController.GetRepublicNmber() !=
            GameConstants.MaxBreadNumber) //スイカの場合は合体Republic生成。スイカはただ消えるだけ…
        {
            //合体 寿司の生成
            breadPieceList[i].breadController.SetNextRepublic(tmpPosition);
        }

        //スコア加算
        AddScore(breadPieceList[i].breadController.GetScore());
        return tmpIsFindPair;
    }

    public void DeleteRandomGameObject()
    {
        List<int> tmpDeleteIDNumber = new List<int>();  //削除リスト

        int tmpDeleteIndex = Random.Range(0, breadPieceList.Count);
        tmpDeleteIDNumber.Add(breadPieceList[tmpDeleteIndex].masterIDNumber);

        DeleteGameObject(tmpDeleteIDNumber);
    }
    
    private void DeleteGameObject(List<int> tmpDeleteIDNumber)
    {
        for (int i = 0; i < tmpDeleteIDNumber.Count; i++)
        {
            for (int j = 0; j < breadPieceList.Count; j++)
            {
                if (breadPieceList[j].masterIDNumber == tmpDeleteIDNumber[i])
                {
                    Destroy(breadPieceList[j].breadController.gameObject);
                    break;
                }
            }

            DeleteRepublicControllerFromList(tmpDeleteIDNumber[i]);
        }
    }

    public object GetBestScore()
    {
        return RecordManager.instance.GetBestScore();
    }

    public int GetWatermelonCount()
    {
        return watermelonCount;
    }

    public bool ExistsGameObject()
    {
        if (breadPieceList.Count() <= 0)
        {
            return false;
        }

        return true;
    }
}
