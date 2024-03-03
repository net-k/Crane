using UnityEngine;
using UnityEngine.UI;

namespace SushiGame.Help
{
    public class HelpButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        
        [SerializeField]
        HelpDialogPresenter _helpDialogPresenter;
            
        void Awake()
        {
            _button.onClick.AddListener(() =>
            {
                _helpDialogPresenter.Open();
            });
        }
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
