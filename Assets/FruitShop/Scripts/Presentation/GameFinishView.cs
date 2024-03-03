using UnityEngine;
using UnityEngine.UI;

namespace PanGame.Presentation
{
    public class GameFinishView : MonoBehaviour
    {
        [SerializeField]
        private Button _okButton;

        public Button OkButton => _okButton;
    }
}
