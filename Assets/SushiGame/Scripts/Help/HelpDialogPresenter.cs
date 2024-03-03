using UnityEngine;

namespace SushiGame.Help
{
    public class HelpDialogPresenter : MonoBehaviour
    {
        [SerializeField]
        HelpDialogView _view;
        
        void Awake()
        {
            _view.CloseButton.onClick.AddListener(() =>
            {
                Close();
            });
        }

        private void Close()
        {
            gameObject.SetActive(false);
        }

        public void Open()
        {
            gameObject.SetActive(true);
        }

        public bool IsShow()
        {
            return gameObject.activeSelf;
        }
    }
}
