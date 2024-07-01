using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setnitro : MonoBehaviour
{
   private void OnTriggerEnter(Collider other)
   {
      if (other.gameObject.CompareTag(GameConstant.Tag_Player))
      {
          LevelManager.instace.Canvas.GetComponent<RCC_MobileButtons>().FillNos();
          LevelManager.instace.Canvas.GetComponent<RCC_MobileButtons>().setNos(true);
      }
   }
}
