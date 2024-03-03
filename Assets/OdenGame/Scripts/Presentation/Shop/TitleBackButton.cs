using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace OdenGame.Presentation.Shop
{
    public class TitleBackButton : MonoBehaviour
    {
        [SerializeField]
        private Button _backButton = null;

        private void Awake()
        {
            _backButton.onClick.AddListener(() =>
            {
                SceneManager.LoadScene("Title");
            });
        }

    }
}
