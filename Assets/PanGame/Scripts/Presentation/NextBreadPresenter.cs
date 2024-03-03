using UnityEngine;

namespace PanGame.Presentation
{
    public class NextBreadPresenter : MonoBehaviour
    {
        [SerializeField]
        private NextBreadView _view = null;
        
        public void SetNextBreadImage(int imageNo)
        {
            // Resources から index の sprite を生成する
            var sprite = Resources.Load<Sprite>($"Next/{imageNo}");
            _view.NextBreadImage.sprite = sprite;
        }
    }
}
