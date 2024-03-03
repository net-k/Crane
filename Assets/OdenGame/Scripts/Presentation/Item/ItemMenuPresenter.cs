using OdenGame.Domain;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace OdenGame
{
    public class ItemMenuPresenter : MonoBehaviour
    {
        [SerializeField]
        private ItemMenuView _view = null;

        [SerializeField]
        private ItemDetailDialogPresenter _itemDialogPresenter = null;

        [SerializeField]
        private ItemUser _itemUser;
        
        private Player _player;
        private ItemUsageInformation _itemUsageInformation;
        
        [Inject]
        private void Construct(Player player, ItemUsageInformation itemUsageInformation)
        {
            _player = player;
            _itemUsageInformation = itemUsageInformation;
        }
        
        private void Awake()
        {
            _itemDialogPresenter.ObservableItemUse.Subscribe(itemId =>
            {
                UpdateView();
            });
            
            _view.CloseButton.onClick.AddListener(() => { Hide(); });
            
            _view.Buttons[0].onClick.AddListener(() => { ShowDetail(1); });
            _view.Buttons[1].onClick.AddListener(() => { ShowDetail(2); });
            _view.Buttons[2].onClick.AddListener(() => { ShowDetail(3); });
            _view.Buttons[3].onClick.AddListener(() => { ShowDetail(4); });
            
            // UpdateView();
        }

        private void UpdateView()
        {
            for (int i = 0; i < 4; i++)
            {
                UpdateItemCount(i);
                UpdateItemUsage(i);
                UpdateButtonState(i);
            }
        }

        private void UpdateButtonState(int i)
        {
            int itemId = i + 1;
            if (_player.GetItemCount(itemId) <= 0 || !_itemUser.CanUseItem(itemId))
            {
                //_view.Buttons[i].enabled = false;
                _view.Buttons[i].interactable = false;
                //ColorBlock colors = _view.Buttons[i].colors;
                //colors.disabledColor = new Color(145.0f/255.0f, 145.0f/255.0f, 145.0f/255.0f, 128.0f/255.0f);
                //_view.Buttons[i].colors = colors;
            }
            else
            {
                _view.Buttons[i].enabled = true;
                _view.Buttons[i].interactable = true;
            }
        }

        private void UpdateItemCount(int i)
        {
            int itemId = i + 1;
            string itemCountString = $"{_player.GetItemCount(itemId).ToString()}";
            _view.ItemCountTexts[i].text = $"x{itemCountString}";
        }

        private void UpdateItemUsage(int i)
        {
            int itemId = i + 1;
            bool canUsage = _itemUsageInformation.CanUsage(itemId);
            if (canUsage)
            {
                canUsage = _itemUser.CanUseItem(itemId);
            }
            _view.ItemUsageImages[i].gameObject.SetActive(!canUsage);
        }

        
        private void Hide()
        {
            gameObject.SetActive(false);
        }

         private void ShowDetail( int itemID)
         {
              _itemDialogPresenter.Show(itemID);
              Hide();
         }

         public void Show()
         {
             gameObject.SetActive(true);
             UpdateView();
         }

         public bool IsShow()
         {
             return gameObject.activeSelf;
         }
    }
}
