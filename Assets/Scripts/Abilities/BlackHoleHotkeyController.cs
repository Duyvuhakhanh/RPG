using TMPro;
using UnityEngine;

namespace Abilities
{
    public class BlackHoleHotkeyController : MonoBehaviour
    { 
        private KeyCode hotkey;
        [SerializeField] private TextMeshProUGUI text;
        public void SetupHotkey(KeyCode _hotkey)
        {
            hotkey = _hotkey;
            text.text = hotkey.ToString();
        }
    }
}
