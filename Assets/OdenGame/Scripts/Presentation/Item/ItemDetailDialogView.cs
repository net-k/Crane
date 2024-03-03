using UnityEngine;
using UnityEngine.UI;

namespace OdenGame
{
    public class ItemDetailDialogView : MonoBehaviour
    {
        [SerializeField]
        private Text _captionText = null;
        
        [SerializeField]
        private Text _detailText = null;

        [SerializeField]
        private Button _closeButton;

        [SerializeField]
        private Button _okButton;

        [SerializeField]
        private Image _itemImage;

        public Text CaptionText => _captionText;

        public Text DetailText => _detailText;

        public Button CloseButton => _closeButton;

        public Button OkButton => _okButton;
        
        public Image ItemImage => _itemImage;
    }
}
