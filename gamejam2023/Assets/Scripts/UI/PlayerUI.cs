using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    private UIDocument m_UIDocument;

    private Label _litterCountDisplay;


    // Start is called before the first frame update
    void Start()
    {
        var rootElement = m_UIDocument.rootVisualElement;
        _litterCountDisplay = rootElement.Q<Label>("BottleCountLabel");
    }

    public void SetLitterValue(int litterCount)
    {
        _litterCountDisplay.text = litterCount.ToString();
    }
}
