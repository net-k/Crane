using UnityEngine;
using UnityEngine.UI;

namespace PanGame.Presentation
{
    public class NextBreadView : MonoBehaviour
    {
        [SerializeField]
        Image _nextBreadImage;

        public Image NextBreadImage => _nextBreadImage;
    }
}
