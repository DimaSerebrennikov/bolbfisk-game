using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopUp : MonoBehaviour
{
    [SerializeField] GameObject _messageBlock;
    [SerializeField] TextMeshProUGUI _textField;
    float savedTimeScale;
    public void ShowPopup(string message)
    {
        if (_messageBlock.activeSelf == false)
        {
            _messageBlock.SetActive(true);
            _textField.text = message;
            savedTimeScale = Time.timeScale;
            Time.timeScale = 0;
        }
    }
    public void CancelPopup()
    {
        _messageBlock.SetActive(false);
        Time.timeScale = savedTimeScale;
    }
}
