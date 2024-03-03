using UnityEngine;
using UnityEngine.UI;

namespace PanGame.Presentation.Menu
{
    public class MenuDialogView : MonoBehaviour
    {
        [SerializeField]
        private Button _backToGameButton = null;

        [SerializeField]
        private Button _retryButton = null;

        public Button BackToGameButton => _backToGameButton;

        public Button RetryButton => _retryButton;
    }
}
