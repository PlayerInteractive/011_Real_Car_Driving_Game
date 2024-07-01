using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TpsTriger : MonoBehaviour
{
   public void OnTriggerEnter(Collider other)
   {
      //this forcar
      if (other.gameObject.tag == GameConstant.Tag_Carhandle)
      {
         GameControl.manager.getInVehicle.SetActive(true);
         GameManager.Instance.CurrentCar = other.GetComponentInParent<RCC_CarControllerV3>().gameObject;
      }
   }

   private void OnTriggerStay(Collider other)
   {
    
      //this forcar
      if (other.gameObject.tag == GameConstant.Tag_Carhandle)
      {
         GameControl.manager.getInVehicle.SetActive(true);
         GameManager.Instance.CurrentCar = other.GetComponentInParent<RCC_CarControllerV3>().gameObject;
      }
   }

   private void OnTriggerExit(Collider other)
   {
      //this forcar
      if (other.gameObject.tag == GameConstant.Tag_Carhandle)
      {
         GameControl.manager.getInVehicle.SetActive(false);
         //GameManager.Instance.CurrentCar = null;
         Logger.ShowLog("Car Handle");
      }
   }
}
   