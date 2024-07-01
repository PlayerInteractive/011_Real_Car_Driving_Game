using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Playables;

public class ComplteTrigger : MonoBehaviour
{
    public GameObject Particals,trigger;
    public bool IsSetCamerabackword = true;
    public Transform center;
    
    private async void OnTriggerEnter(Collider other)
    {
        UiManagerObject.instance.HideGamePlay();
        Particals.SetActive(true);
        LevelManager.instace.vehicleCamera.enabled = false;
        LevelManager.instace.vehicleCamera.GetComponent<CameraTransition>().Pos();
        if (other.gameObject.CompareTag(GameConstant.Tag_Player))
        {
            if ( GameManager.Instance.CurrentCar.GetComponent<VehicleProperties>().Grounded)
            {
                await Task.Delay(1000);
                GameManager.Instance.CurrentCar.GetComponent<Rigidbody>().velocity=Vector3.zero; 
                GameManager.Instance.CurrentCar.GetComponent<Rigidbody>().angularVelocity=Vector3.zero; 
            }
            if (iscopleteWithoutecutsene)
            {
                UiManagerObject.instance.ShowComplete();
            }
            else
            {
                if (IsSetCamerabackword)
                {
                    Logger.ShowLog("here");
                    LevelManager.instace.vehicleCamera.gameObject.SetActive(false);
                    GameManager.Instance.CurrentCar.transform.position = center.transform.position;
                    GameManager.Instance.CurrentCar.transform.rotation = center.transform.rotation;
                    GameManager.Instance.CurrentCar.GetComponent<Rigidbody>().isKinematic = true;
                    StartCutscne();
                }
            }
        }
       
    }
    public bool isCutScene;
    public GameObject Timeline;
    public PlayableDirector Director;



    public bool iscopleteWithoutecutsene = false;
    public void StartCutscne()
    {
        Logger.ShowLog("here");
        if (isCutScene) 
        {
            Time.timeScale = 1f;
            Timeline.SetActive(true);
            Director.Play();
            Invoke("HideTimeline", (float)Director.duration - 0.9f);
            GameManager.Instance.CurrentCar.GetComponent<Rigidbody>().isKinematic = false;
            GameManager.Instance.CurrentCar.GetComponent<Rigidbody>().drag = 10;
            GameManager.Instance.CurrentCar.GetComponent<Rigidbody>().angularDrag = 10;
            trigger.gameObject.SetActive(false);
        }
    }

    public void HideTimeline()
    {
        Timeline.SetActive(false);
        LevelManager.instace.Tpscamera.GetComponent<Camera>().farClipPlane = getFar();
        LevelManager.instace.vehicleCamera.gameObject.SetActive(true);
        LevelManager.instace.Tpscamera.gameObject.SetActive(false);
        UiManagerObject.instance.ShowComplete();
        isCutScene = false;
    }

    private int  getFar()
    {
        if (SystemInfo.systemMemorySize > 3500)
        {
            return 250;
        }
        else if (SystemInfo.systemMemorySize > 2500)
        {
            return 200;
        }
        else if (SystemInfo.systemMemorySize > 1200)
        {
            return 150;
        }
        else
        {
            return 100;
        }
    }
}

