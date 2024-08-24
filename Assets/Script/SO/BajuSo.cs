using System;
using System.Collections.Generic;
using UnityEngine;

namespace Script.SO
{
    
    public enum LangkahCuci{
        Detergen,
        Bilas,
        Pewangi,
        Sikat,
        Pemutih
    }

    public enum BajuType{
        Warna,
        Putih
    }
    [CreateAssetMenu(fileName = "BajuManagerItem", menuName = "BajuSo/BajuItem")]

    public class BajuSo : ScriptableObject
    {
        public List<LangkahCuci> langkahCuci;
        public BajuType Bajutype ;
        public Sprite DisplayBajuInventory;
        public Sprite BajuKotorSprite;
        public Sprite BajuBasahSprite;
        public Sprite BajuBersihSpritel;

    }
}