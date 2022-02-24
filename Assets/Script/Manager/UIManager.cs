using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region Declaration
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject grabDropIcon;
    [SerializeField] Text grabDropText;
    #endregion
    public static UIManager Instance { get; private set; }
    void Start()
    {
        Instance = this;
    }

    void Update()
    {

    }

    public void ActivateIconGrab()
    {
        grabDropIcon.SetActive(true);
        grabDropText.text = "Grab";
    }
    public void ActivateIconDrop()
    {
        grabDropIcon.SetActive(true);
        grabDropText.text = "Drop";
    }
    public void DeactivateIconGrab()
    {
        if (grabDropIcon != null)
            grabDropIcon.SetActive(false);
    }

    public void ButtonNewGame()
    {

    }
    public void ButtonLoad()
    {

    }
    public void ButtonQuit()
    {

    }
}
