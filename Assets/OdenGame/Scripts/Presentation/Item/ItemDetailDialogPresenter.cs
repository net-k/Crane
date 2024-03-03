using System;
using I2.Loc;
using UniRx;
using UnityEngine;
using Zenject;


namespace OdenGame
{
    public class ItemDetailDialogPresenter : MonoBehaviour
    {
        [SerializeField]
        private ItemDetailDialogView _itemDetailDialogView = null;

        
        [SerializeField]
        private ItemUser _itemUser = null;
        
        private int _itemId;

        private ItemMaster _itemMaster = null;
        
        private Subject<int> _itemUseSubject = new Subject<int>();
        public IObservable<int> ObservableItemUse => _itemUseSubject;
        
        [Inject]
        void Construct(ItemMaster itemMaster)
        {
            _itemMaster = itemMaster;
        }
        
        private void Awake()
        {
            _itemUseSubject.Subscribe(itemId =>
            {
            });
            
            _itemDetailDialogView.CloseButton.onClick.AddListener(() =>
            {
                Hide();
            });
            
            _itemDetailDialogView.OkButton.onClick.AddListener(() =>
            {
                if (_itemUser.UseItem(_itemId))
                {
                    _itemUseSubject.OnNext(_itemId);
                }
                Hide();
            });
        }

        public void Show(int itemId)
        {
            var item = _itemMaster.GetItem(itemId);
            string keyCaption = item.name;
            string keyDetail = item.detail;
            
            string caption = LocalizationManager.GetTranslation(keyCaption);
            _itemDetailDialogView.CaptionText.text = caption;
            
            string detail = LocalizationManager.GetTranslation(keyDetail);
            _itemDetailDialogView.DetailText.text  = detail;
            
            string filePath = $"Textures/Items/{item.fileName}"; 
            _itemDetailDialogView.ItemImage.sprite = Resources.Load<Sprite>(filePath);
            
            _itemId = itemId;
            
            gameObject.SetActive(true);
        }
        
        private void Hide()
        {
            gameObject.SetActive(false);
        }

        public bool IsShow()
        {
            return gameObject.activeSelf;
        }
    }
}
