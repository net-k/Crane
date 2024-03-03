using UnityEngine;
using UnityEngine.UI;

namespace ShogiOnline.Presentation
{
    public class WatchBoardView : MonoBehaviour
    {
        [SerializeField]
        private Button _backResultButton = null;

        public Button BackResultButton => _backResultButton;

        
    }
}
