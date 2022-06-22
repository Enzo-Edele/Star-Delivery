using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Spacecraft : MonoBehaviour
{
    public Material spacecraftMaterial;
    public float orderInList;
    public GameObject spacecraft;
    [SerializeField] GameObject shuttlePrefab;
    [SerializeField] Animator ShuttleAnimator;
    GameObject shuttle;
    Vector3 shuttleStartPos;
    public Coroutine launchCo;
    public int packages = 0;
    public int maximumCharge = 4;
    public int overload = 2;
    public float estimatedTime = 20;
    public string spacecraftDestination;
    public bool delivered;
    private bool full;
    public float deliveredTime;
    private int sendScore = 0;
    public TMP_Text destinationText; 
    [SerializeField] Launcher launcher;
    [SerializeField] Cinemachine.CinemachineSmoothPath path;

    private void Awake()
    {
        //spacecraftMaterial = spacecraft.GetComponent<Renderer>().material;
        //spacecraft = this.gameObject.transform.parent.gameObject;
        delivered = false;
        orderInList = GameManager.Instance.spacecraft.Count;
        GameManager.Instance.spacecraft.Add(this);
        shuttleStartPos = new Vector3(0, 1, -3.5f);
        shuttleStartPos = transform.parent.TransformPoint(shuttleStartPos);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Box box = collision.gameObject.GetComponent<Box>();
        if (!full)
        { 
            packages++;
            sendScore += 100;
            if (box.isFragile) sendScore += 50;
            box.Navette(spacecraftDestination);
        }
        if (packages >= (maximumCharge + overload))
        {
            full = true;
        }
        if (packages > maximumCharge)
        {
            estimatedTime += 2;
        }
    }

    public IEnumerator LaunchCoroutine()
    {
        full = true;
        if (!delivered)
        {
            GameManager.Instance.SpacecraftDeliver(packages);
            GameManager.Instance.UpdateScore(sendScore, -1);
            packages = 0;
            spacecraft.SetActive(false);
            shuttle = Instantiate(shuttlePrefab, shuttleStartPos, Quaternion.identity, transform.parent);
            shuttle.GetComponent<Cinemachine.CinemachineDollyCart>().m_Path = path;
            ShuttleAnimator.SetTrigger("Depart");
        }
        delivered = true;
        yield return new WaitForSeconds(estimatedTime);
        estimatedTime = 20;
        if (spacecraft != null)
        {
            spacecraft.SetActive(true);
            launcher.ButtonUp();
        }
        Destroy(shuttle);
        shuttle = null;
        delivered = false;
        full = false;
        sendScore = 0;
    }
}
