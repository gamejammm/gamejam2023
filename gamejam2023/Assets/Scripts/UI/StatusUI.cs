using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class StatusUI : MonoBehaviour
{
    [SerializeField]
    private UIDocument m_UIDocument;

    private Label _monetasDisplay;


    // Start is called before the first frame update
    void OnEnable()
    {
        var rootElement = m_UIDocument.rootVisualElement;
        _monetasDisplay = rootElement.Q<Label>("MonetasValue");
    }

    public void SetMonetasValue(float monetasValue)
    {
        _monetasDisplay.text = monetasValue.ToString();
    }
}
