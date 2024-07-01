using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using GameAnalyticsSDK;
using UnityEngine;

public class RestPosion : MonoBehaviour
{
   public async void OnTriggerEnter(Collider other)
   {
      if (other.gameObject.tag==GameConstant.Tag_Player)
      {
         try 
         {
          gameObject.SetActive(false);
          LevelManager.instace.istriggeranyobject = true;
          LevelManager.instace.LastPosition =other.transform.position ;
          LevelManager.instace.LastRoation =other.transform.rotation ;
          await Task.Delay(1000);
          gameObject.SetActive(true); 
         }
         catch (Exception e) 
         { 
            GameAnalytics.NewErrorEvent(GAErrorSeverity.Error,
             "Exception at Reset Position" + e.ToString());
          throw; 
         }
      }
   }
}
