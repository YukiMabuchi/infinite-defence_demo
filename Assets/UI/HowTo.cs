using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowTo : MonoBehaviour
{
    [SerializeField] GameObject howToPopup;

    private void Awake()
    {
        Close();
    }

    public void Open()
    {
        howToPopup.SetActive(true);
    }
    public void Close()
    {
        howToPopup.SetActive(false);
    }
}
