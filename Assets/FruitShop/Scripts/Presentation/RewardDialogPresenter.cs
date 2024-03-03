using App;
using FruitShop.Domain;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace FruitShop.Presentation
{
    public class RewardDialogPresenter : MonoBehaviour
    {
        [SerializeField]
        private RewardDialogView _view;

        [SerializeField]
        private MainManager _mainManager;
        
        [Inject]
        private void Construct()
        {
            
        }
        
        private void Awake()
        {
            _view.OkButton.onClick.AddListener(
                () =>
                {
                    Close();
                });
        }

        private void Close()
        {
            gameObject.SetActive(false);
            // タイトルシーンへ
            SceneManager.LoadScene ("Title");
        }

        public void Open()
        {
            gameObject.SetActive(true);
            
            var bonusCalculator = new BonusCalculator();
            
            int score = _mainManager.GetScore();
            int scoreBonus = bonusCalculator.CalculateScoreBonus(score);
            int watermelonCount = _mainManager.GetWatermelonCount();
            int watermelonBonus = bonusCalculator.CalculateWatermelonBonus(watermelonCount);
            _view.ScoreBonusText.text = scoreBonus.ToString();
            _view.WatermelonBonusText.text = watermelonBonus.ToString();
            
            int bonus = scoreBonus + watermelonBonus;
            SaveDataManager.Instance.SaveAddMoney(bonus);
        }
    }
}
