using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZOMBOM.Data
{
    [CreateAssetMenu(fileName = "WeaponData", menuName = "Data/New Weapon Data", order = 2)]
    public class D_Weapon : ScriptableObject
    {
        public float damage;
        public AnimatorOverrideController animator;
        public GameObject projectile;

        public void EquipWeapon()
        {

        }

        public void Attack()
        {

        }
        public void Shoot()
        {
            
            Instantiate(projectile );
        }
    }
}
