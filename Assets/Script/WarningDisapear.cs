using UnityEngine;

public class WarningDisapear : MonoBehaviour
{
    [SerializeField] int timeWarn;
    float timerWarn;

    void Start()
    {
        timerWarn = timeWarn;
    }
    void Update()
    {
        if (timerWarn < 0)
            timerWarn -= Time.deltaTime;
        else
            UIManager.Instance.DeactivateFragileWarning();
    }
}
