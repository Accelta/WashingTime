using UnityEngine;

namespace Script.SO
{
    [CreateAssetMenu(fileName = "AlatCuci", menuName = "BajuSo/AlatCuci", order = 0)]
    public class PencuciSO : ScriptableObject
    {
        public Sprite _modelSprite;
        public LangkahCuci _LangkahCuci;
        public AudioClip _SuaraAlatCuci;
    }
}