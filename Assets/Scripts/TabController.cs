using UnityEngine;
using UnityEngine.UI;
public class TabController : MonoBehaviour
{
    public GameObject tab1Content;
    public GameObject tab2Content;

    public Text SliderValue;

    public Slider slider;
    
    private void Start()
    {
        // Set tab1Content to be active and tab2Content to be inactive at the start.
        tab1Content.SetActive(true);
        tab2Content.SetActive(false);
    }

    public void ShowTab1Content()
    {
        tab1Content.SetActive(true);
        tab2Content.SetActive(false);
    }

    public void ShowTab2Content()
    {
        tab1Content.SetActive(false);
        tab2Content.SetActive(true);
        slider.onValueChanged.AddListener(UpdateSliderValue);
        SliderValue.text = SliderValue.text = "Slider Value: " + slider.value;;
    }
    
    public void IncreaseSlider()
    {
        slider.value += 1;
    }

    public void DecreaseSlider()
    {
        slider.value -= 1;
    }
    
    public void UpdateSliderValue(float value)
    {
        SliderValue.text = "Slider Value: " + slider.value;
    }
}