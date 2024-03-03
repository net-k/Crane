using System;
using System.Collections;
using System.Collections.Generic;
using App;
using OdenGame.Repository;
using UnityEngine;
using Zenject;

public class RecordManager : MonoBehaviour
{
    public static RecordManager instance;

    //レコード保持用
	public static int record = 0;

	private GameSettings _gameSettings;
	
	[Inject]
	void Construct(GameSettings gameSettings)
	{
		_gameSettings = gameSettings;
	}
	
    void Awake()
	{
		//シングルトン処理
		if (instance != null)
		{
			Destroy(this.gameObject);
		}
		else if (instance == null)
		{
			instance = this;
		}
		DontDestroyOnLoad (this.gameObject);
	}

    string GetCategory()
    {
	    switch(_gameSettings._GameMode)
	    {
		    case GameSettings.GameMode.Normal:
			    return "normal";
			    break;
		    case GameSettings.GameMode.Item:
			    return "item";
			    break;
		    default:
			    throw new ArgumentOutOfRangeException();
	    }
    }
    
    public void SaveBestScore(int score)
	{
		SaveDataManager.Instance.SaveBestScore(GetCategory(), 0, score);
		record = score;
	}

    public int LoadBestScore()
    {
		record = SaveDataManager.Instance.LoadBestScore(GetCategory(), 0);
		return record;
    }

    public object GetBestScore()
    {
	    return record;
    }
}
