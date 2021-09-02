using UnityEngine.UI;
using UnityEngine;

namespace ZOMBOM.UI
{
    public class HealthBarDisplay : MonoBehaviour
    {
        public Slider slider;

        public void UpdateSlider(float ammount)
        {
            ammount = Mathf.Clamp01(ammount);
            slider.value = ammount;
        }

    }

}