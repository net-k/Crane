using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace OdenGame
{
    public class ItemMenuView : MonoBehaviour
    {
        [SerializeField]
        private Button _closeButton;
        
        [SerializeField] private List<Button> _buttons;

        [SerializeField]
        private List<Text> _itemCountTexts = null;

        [SerializeField]
        private List<Image> _itemUsageImages = null;

        public Button CloseButton => _closeButton;

        public List<Button> Buttons => _buttons;
        
        public List<Text> ItemCountTexts => _itemCountTexts;
        
        public List<Image> ItemUsageImages => _itemUsageImages;
    }
}
