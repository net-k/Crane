using System;
using Aquarium.Domain.Localize;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;

namespace Aquarium.Presentation.GameScene.AdditionalPointDialog
{
    public class AdditionalPointView : MonoBehaviour
    {
        [SerializeField]
        private Localize _localize;

        [SerializeField]
        private Text captionText = null;
        public Text CaptionText
        {
            get { return captionText; }
        }
    
        [FormerlySerializedAs("MovieButton")]
        [SerializeField]
        private Button movieButton = null;
        public Button MovieButton
        {
            get { return movieButton; }
        }

        [FormerlySerializedAs("CloseButton")]
        [SerializeField]
        private Button closeButton = null;
        public Button CloseButton
        {
            get { return closeButton; }
        }
    
        private void Awake()
        {
            // captionText.text = _localize.GetText("AdditionalPointDialogCaptionText");
            // movieButton.GetComponentInChildren<Text>().text = _localize.GetText("AdditionalPointDialogMovieButton" );
        }

        private void Start()
        {
            
        }
    }
}
