using UnityEngine;
using UnityEngine.UI;

namespace FruitShop.Presentation
{
    public class RewardDialogView : MonoBehaviour
    {
        [SerializeField]
        private Button _okButton;

        public Button OkButton => _okButton;

        [SerializeField]
        private Text _scoreBonusText = null;
        [SerializeField]
        private Text _watermelonBonusText = null;
        public Text ScoreBonusText => _scoreBonusText;
        public Text WatermelonBonusText => _watermelonBonusText;

       
    }
}
