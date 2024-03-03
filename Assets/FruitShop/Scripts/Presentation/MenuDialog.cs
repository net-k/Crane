using OdenGame.Repository;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace Quiz.Presentation
{
    class MenuDialog : MonoBehaviour
    {
        [SerializeField]
        private Button _normalModeButton;
        [SerializeField]
        private Button _itemModeButton;

        private GameSettings _gameSettings;
        
        [Inject]
        void Construct(GameSettings gameSettings)
        {
            _gameSettings = gameSettings;
        }
        
        private void Awake()
        {
            _normalModeButton.onClick.AddListener(OnNormalModeButtonClicked);
            _itemModeButton.onClick.AddListener(OnItemModeButtonClicked);
        }
        
        private void OnNormalModeButtonClicked()
        {
            GoNextScene(GameSettings.GameMode.Normal);
        }
        
        private void OnItemModeButtonClicked()
        {
            GoNextScene(GameSettings.GameMode.Item);
        }

        private void GoNextScene(GameSettings.GameMode gameMode)
        {
            _gameSettings._GameMode = gameMode;
            SceneManager.LoadScene("MainScene");
        }
       
        public void Show()
        {
            gameObject.SetActive(true);
        }
    }
}