using OdenGame.Repository;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace OdenGame
{
    public class ItemButton : MonoBehaviour
    {
        [SerializeField]
        private Button _itemButton = null;
        
        [SerializeField]
        private ItemMenuPresenter _itemMenuPresenter = null;

        private GameSettings _gameSettings;
        
        [Inject]
        void Construct(GameSettings gameSettings)
        {
            _gameSettings = gameSettings;
        }
        
        private void Awake()
        {
            switch (_gameSettings._GameMode)
            {
                case GameSettings.GameMode.Normal:
                    _itemButton.gameObject.SetActive(false);
                    break;
                case GameSettings.GameMode.Item:
                    _itemButton.gameObject.SetActive(true);
                    break;
            }
            
            _itemButton.onClick.AddListener( () => { ShowItemMenu(); });
        }

        private void ShowItemMenu()
        {
            _itemMenuPresenter.Show();
        }
    }
}
