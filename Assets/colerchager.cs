using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using  SickscoreGames.HUDNavigationSystem;
using UnityEngine.Rendering;

public class colerchager : MonoBehaviour
{
   public Material Yellow;
   public Material whigte;

   public Color[] foryellow;
   public Color[] forwhigte;
   public int RenemColeryellow = 0;
   public int RenemColerwhigte = 1;
   private void Start()
   {
      RenemColeryellow =Random.Range(0, foryellow.Length); 
      Yellow.color= foryellow[RenemColeryellow];
      
      RenemColerwhigte =Random.Range(0, forwhigte.Length); 
      whigte.color= forwhigte[RenemColerwhigte];
   }
}
