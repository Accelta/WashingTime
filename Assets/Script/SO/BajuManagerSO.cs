using System.Collections.Generic;
using UnityEngine;

namespace Script.SO
{
    [CreateAssetMenu(fileName = "BajuManager", menuName = "BajuSo/BajuManager")]
    public class BajuManagerSO : ScriptableObject
    {
        public  List<BajuSo> BajuSoList = new List<BajuSo>();
    }
}