using ZOMBOM.Data;
using UnityEngine;

namespace ZOMBOM.Combat
{
    public class EnemyCombat : MonoBehaviour
    {
        [SerializeField] D_Weapon startingWeapon;

        D_Weapon currentWeapon;

        private void Start()
        {
            currentWeapon = startingWeapon;
        }
    }
}
