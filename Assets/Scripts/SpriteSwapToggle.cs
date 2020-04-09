using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Events;

namespace NMenus
{
    [System.Serializable]
    public class ToggleEvent : UnityEvent<bool> { }

    public class SpriteSwapToggle : MonoBehaviour
    {

        public Sprite onSprite, offSprite;
        public ToggleEvent onToggle;

        Image myImg;

        bool isOn = false;
        
        public bool IsON {
            get {
                return isOn;
            }
        }

        // Use this for initialization
        public void Init()
        {
            myImg = GetComponent<Image>();
            GetComponent<Button>().onClick.AddListener(SwitchToggle);
        }

        public void SwitchToggle()
        {
            isOn = !isOn;
            UpdateSprite();
            onToggle.Invoke(isOn);
        }
        
        void UpdateSprite()
        {
            myImg.sprite = isOn ? onSprite : offSprite;
        }

    }
}
