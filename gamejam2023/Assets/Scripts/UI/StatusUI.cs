using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class StatusUI : MonoBehaviour
{
    [SerializeField]
    private UIDocument m_UIDocument;

    private Label _monetasDisplay;

    private Label _dragonLoveLabel;

    private Label _timeCounter;


    // Start is called before the first frame update
    void OnEnable()
    {
        var rootElement = m_UIDocument.rootVisualElement;
        _monetasDisplay = rootElement.Q<Label>("MonetasValue");
        _dragonLoveLabel = rootElement.Q<Label>("DragonLoveValue");
        _timeCounter = rootElement.Q<Label>("TimeCounter");
    }

    public void SetMonetasValue(float monetasValue)
    {
        _monetasDisplay.text = monetasValue.ToString();
    }

    public void SetDragonLoveValue(float loveValue)
    {
        _dragonLoveLabel.text = loveValue.ToString();
    }

    public void SetTimeCounter(string timeValue)
    {
        _timeCounter.text = timeValue;   
    }
}
