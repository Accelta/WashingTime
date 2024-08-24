using UnityEngine;

namespace Script.SO
{
    public enum tipe{
        ringan,
        berat
    }
    [CreateAssetMenu(fileName = "BajuJemuran", menuName = "BajuSo/BajuJemuranItem")]
    public class BajuJemuranItem : ScriptableObject
    {
        public Sprite BajuSprite;
        public Sprite BajuHangerSprite;
        public string BajuAnimation;
        public tipe BajuType;
    }
}