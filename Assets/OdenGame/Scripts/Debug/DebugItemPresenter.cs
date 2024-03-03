using System;
using OdenGame.Domain;
using UnityEngine;
using Zenject;

namespace OdenGame.Debug
{
    public class DebugItemPresenter : MonoBehaviour
    {
        [SerializeField]
        private DebugItemView _view;

        private Player _player;
        
        [Inject]
        private void Construct( Player player)
        {
            _player = player;
        }
        
        private void Awake()
        {
            _view.AddItemButton.onClick.AddListener(() =>
            {
                // TODO
                for (int i = 0; i < 3; i++)
                {
                    int itemId = i + 1;
                    int itemCount = 1; 
                    _player.AddItem(itemId, itemCount);
                }
            });
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
