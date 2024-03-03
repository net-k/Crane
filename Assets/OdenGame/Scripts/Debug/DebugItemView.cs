using UnityEngine;
using UnityEngine.UI;

namespace OdenGame.Debug
{
    public class DebugItemView : MonoBehaviour
    {
        [SerializeField] private Text _itemContentText = null;
        [SerializeField] private Button _addItemButton = null;

        public Button AddItemButton => _addItemButton;

        
    }
}
