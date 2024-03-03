using System;
using App;
using Aquarium.Presentation.GameScene.AdditionalPointDialog;
using I2.Loc;
using Quiz.Framework.Ad.AdMob;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(AdditionalPointView))]
public class AdditionalPointPresenter : MonoBehaviour
{
    private AsyncOperation async;
    private AdditionalPointView _view = null;

    private SaveDataManager _saveDataManager = null;
//    [SerializeField]
//    private Localize _localize = null;
    
    [SerializeField]
    private AdMobRewardVideo _adMobRewardVideo = null;
    private DialogPresenter _dialogPresenter = null;

    [Inject]
    void Construct()
    {
        _saveDataManager = SaveDataManager.Instance;
    }
    
    public void Awake()
    {
        _view = GetComponent<AdditionalPointView>();
        if (_view == null)
        {
            throw new Exception("view の取得に失敗");
        }
        
        SetEvents();
    }

    // Viewのイベントの設定を行う
    private void SetEvents()
    {
        _view.MovieButton.onClick.AddListener(OnMovieButtonClicked);
        _view.CloseButton.onClick.AddListener(OnCloseButtonClicked);
        
    }

    void OnMovieButtonClicked()
    {
        OpenMovieDialog();
    }

    
    private void OpenMovieDialog()
    {
        _view.CaptionText.text = LocalizationManager.GetTranslation("AdditionalPointDialogCaptionText");
        _view.MovieButton.GetComponentInChildren<Text>().text = LocalizationManager.GetTranslation("AdditionalPointDialogMovieButton" );
        
        string dialogText = "";
        int watchTimes = _saveDataManager.LoadWatchPointAddedVideoTimes();
		
        string dialogContext =	string.Format( LocalizationManager.GetTranslation("PointDialogLimitText"), watchTimes.ToString(), GameConstants.WatchPointAddedVideoTimesPerDay.ToString() ) ;
        DialogPresenter.DialogType dialogType = DialogPresenter.DialogType.OK;
        if (watchTimes >= GameConstants.WatchPointAddedVideoTimesPerDay)
        {
            // dialogText= string.Format("また明日ポイントを追加してね！");
            dialogText= string.Format( LocalizationManager.GetTranslation("PointDialogTomorrowText"));
        }
        else
        {
            //	dialogText = string.Format("動画を見てポイントを追加しますか");
            var localizedText = LocalizationManager.GetTranslation("PointDialogAdditionalPointText");
            // dialogText = string.Format( localizedText );
            dialogText = string.Format( localizedText, GameConstants.RewardVideoAdditionalDiamond.ToString() );
            dialogType = DialogPresenter.DialogType.YesNo;
        }

        dialogContext = dialogContext.Replace( "\\n", Environment.NewLine );
        dialogText = dialogText + dialogContext;
        var dialog = DialogPresenter.Instance.ShowDialog(dialogText, dialogType);
        dialog.onYes += () =>
        {
            if (_adMobRewardVideo == null)
            {
                Debug.LogError("_adMobRewardVideo is null リワード動画は再生できません");
                return;
            }
            
            if (!_adMobRewardVideo.Show() )
			{
				DialogPresenter.Instance.ShowDialog("動画の読み込みに失敗しました", DialogPresenter.DialogType.OK);
			}
			else
			{
				SoundManager.Instance.Suspend();
				dialog.Close();
                gameObject.SetActive(false);
			}
        };
        dialog.onNo += () =>
        {
            // Debug.Log("リワード動画広告 Dialog.OnNo");
        };
        _dialogPresenter = dialog; 
    }
    
    void OnCloseButtonClicked()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
}
