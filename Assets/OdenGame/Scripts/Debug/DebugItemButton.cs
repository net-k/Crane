using System;
using FruitShop.Debug;
using UnityEngine;
using UnityEngine.UI;

namespace OdenGame.Debug
{
    public class DebugItemButton : MonoBehaviour
    {
        [SerializeField] private Button _debugButton = null;

        [SerializeField] private ShopDebugMenu _debugMenu = null;
        
        private void Awake()
        {
            if( GameConstants.IsDebugMode == false )
            {
                _debugButton.gameObject.SetActive(false);
                return;
            }
            
            _debugButton.onClick.AddListener(() =>
                {
                    _debugMenu.Show();
                }
            );
        }
    }
}
