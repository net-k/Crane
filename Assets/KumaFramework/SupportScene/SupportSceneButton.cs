using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Quiz.Framework.SupportScene
{
    public class SupportSceneButton: MonoBehaviour {
    
        [SerializeField]
        Button supportSceneButton;


        // Use this for initialization
        void Start () {
            supportSceneButton.onClick.AddListener(OnSupportSceneButtonClicked);	
        }
	
        void OnSupportSceneButtonClicked()
        {
            SceneManager.LoadScene("PanGame/Scenes/SupportScene" );
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }
    }
}
