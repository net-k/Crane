using PanGame.Presentation.Menu;
using SushiGame.Help;
using UnityEngine;

namespace OdenGame.Presentation
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField]
        private ItemMenuPresenter _itemMenuPresenter;
        
        [SerializeField]
        private ItemDetailDialogPresenter _itemDetailDialogPresenter;

        [SerializeField]
        private MenuDialogPresenter _menuDialogPresenter;

        [SerializeField]
        private HelpDialogPresenter _helpDialogPresenter;
        
        public bool CanDrop()
        {
            // itemMenu や itemDetail が表示されていなければ true
            return !_itemMenuPresenter.IsShow() && !_itemDetailDialogPresenter.IsShow()
                                                && !_menuDialogPresenter.IsShow() && !_helpDialogPresenter.IsShow();
        }
    }
}
