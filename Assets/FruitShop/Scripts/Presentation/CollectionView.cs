using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FruitShop.Scripts
{
    public class CollectionView : MonoBehaviour
    {
        [SerializeField]
        private Button _levelUpButton;
        public Button LevelUpButton => _levelUpButton;
        
        [SerializeField]        
        private Button _backButton;
        public Button BackButton => _backButton;
        
        [SerializeField]
        private List<GameObject> _fruitsList;

        [SerializeField]
        private Text _nextLevelPriceText = null;

        public Text NextLevelPriceText => _nextLevelPriceText;

        [SerializeField]
        private GameObject _nextLevelPriceObject = null;
        
        public void VisibleFruits( int level, bool visible )
        {
            int index = level - 1;
            _fruitsList[index].gameObject.SetActive(visible);
        }

        public void HideNextLevelPrice()
        {
            _nextLevelPriceObject.SetActive(false);
        }
    }
}
