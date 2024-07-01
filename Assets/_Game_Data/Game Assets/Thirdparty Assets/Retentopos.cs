using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Retentopos: MonoBehaviour
{
    private async void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag==GameConstant.Tag_Player)
        {
            if (GameManager.Instance.TpsStatus == PlayerStatus.CarDriving)
            {
                if ( LevelManager.instace.istriggeranyobject == true)
                {
                    // Instantiate(LevelManager.instace.Watersplase, other.transform.position, Quaternion.identity);
                    GameManager.Instance.CurrentCar.transform.position = LevelManager.instace.LastPosition;
                    GameManager.Instance.CurrentCar.transform.rotation = LevelManager.instace.LastRoation;
                }
                else
                {
                    if (PrefsManager.GetGameMode() != "free")
                    {
                        if (PrefsManager.GetLevelMode() == 0)
                        {
                            GameManager.Instance.CurrentCar.transform.position = LevelManager.instace.CurrentLevelProperties.CarPosition.transform.position;
                            GameManager.Instance.CurrentCar.transform.rotation = LevelManager.instace.CurrentLevelProperties.CarPosition.transform.rotation;
                        }
                    }
                    else
                    {
                        if (LevelManager.instace.istriggeranyobject == true)
                        {
                           
                            GameManager.Instance.CurrentCar.transform.position = other.transform.position;
                            GameManager.Instance.CurrentCar.transform.rotation = other.transform.rotation;
                        }
                        else
                        {
                           
                            GameManager.Instance.CurrentCar.transform.position = LevelManager.instace.OpenWorldManager.CarPostiom.transform.position;
                            GameManager.Instance.CurrentCar.transform.rotation = LevelManager.instace.OpenWorldManager.CarPostiom.transform.rotation; 
                        }
                    }
                }
            }
            else if (GameManager.Instance.TpsStatus == PlayerStatus.ThirdPerson)
            {
               UiManagerObject.instance.HideGamePlay();
               UiManagerObject.instance.deadpanel.SetActive(true);
               Invoke("ofdeadPanel",3f);
            }
            GameManager.Instance.CurrentCar. GetComponent<Rigidbody>().velocity=Vector3.zero; 
            GameManager.Instance.CurrentCar. GetComponent<Rigidbody>().angularVelocity=Vector3.zero;
        }
    }

    private void ofdeadPanel()
    {
        UiManagerObject.instance.deadpanel.SetActive(false);
        UiManagerObject.instance.Restart();
    }
}
