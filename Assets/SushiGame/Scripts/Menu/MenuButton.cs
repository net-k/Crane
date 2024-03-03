using System;
using UnityEngine;
using UnityEngine.UI;

namespace PanGame.Presentation.Menu
{
    public class MenuButton : MonoBehaviour
    {
        [SerializeField]
        private Button _button = null;
        
        [SerializeField]
        private MenuDialogPresenter _menuDialogPresenter = null;

        private void Awake()
        {
            _button.onClick.AddListener( () =>
            {
                _menuDialogPresenter.Open();
            });
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }
    }
}
