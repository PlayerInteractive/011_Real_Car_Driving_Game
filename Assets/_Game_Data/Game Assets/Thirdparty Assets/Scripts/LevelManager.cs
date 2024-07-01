using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using  SickscoreGames.HUDNavigationSystem;
using UnityEngine.Rendering;

public class LevelManager : MonoBehaviour
{
    public GameObject[] Stuntlevels;
    public GameObject[] JumpLevels;
    public GameObject[] Players;
    public Texture[] TPSplayers;
    public int randeomSpidervalue = 0;
    public Material spidersout;
    public int[] Reward;
    public LevelProperties CurrentLevelProperties;
    public GameObject SelectedPlayer,FreeMode;
    public static LevelManager instace;
    public int CurrentLevel, coinsCounter;
    public HUDNavigationCanvas canvashud;
    public RCC_Camera vehicleCamera;
    public PlayerCamera_New Tpscamera;
    public OpenWorldManager OpenWorldManager;
    public GameObject TpsPlayer,coinBar,Canvas;

    public AudioSource CoinSound;
    [Header("WheatherArea")]
    
    public ParticleSystem snowParticleSystem;
    
    public Material daySkybox;
    public Material nightSkybox;
    public Material Defult;

    public Vector3 LastPosition;
    public Quaternion LastRoation;
    public bool istriggeranyobject = false;
  
    public GameObject directionalLightGO;
    void Awake()
    {
        instace = this;
        if (PrefsManager.GetGameMode() != "free")
        {
            if (PrefsManager.GetLevelMode() == 0)
            {
                SelectedPlayer = Players[PrefsManager.GetSelectedPlayerValue()];
                CurrentLevel = PrefsManager.GetCurrentLevel() - 1;
                CurrentLevelProperties = Stuntlevels[CurrentLevel].GetComponent<LevelProperties>();
                CurrentLevelProperties.gameObject.SetActive(true);
                RenderSettings.skybox = Defult;
                SetTransform(CurrentLevelProperties.TpsPlayer, CurrentLevelProperties.CarPosition);
            }
            if (PrefsManager.GetLevelMode() == 1)
            {
                SelectedPlayer = Players[PrefsManager.GetSelectedPlayerValue()];
                CurrentLevel = PrefsManager.GetCurrentLevel() - 1;
                CurrentLevelProperties = JumpLevels[CurrentLevel].GetComponent<LevelProperties>();
                CurrentLevelProperties.gameObject.SetActive(true);
                RenderSettings.skybox = Defult;
                SetTransform(CurrentLevelProperties.TpsPlayer, CurrentLevelProperties.CarPosition);
            }
        }
        else
        {
            Time.timeScale = 1; 
            FreeMode.SetActive(true);
            RenderSettings.skybox = daySkybox;
            SelectedPlayer = Players[PrefsManager.GetSelectedPlayerValue()];
            SetTransform(OpenWorldManager.TpsPosition, OpenWorldManager.CarPostiom);
        }
   
      
        randeomSpidervalue = Random.Range(0, TPSplayers.Length); 
        spidersout.mainTexture= TPSplayers[randeomSpidervalue];
        SelectedPlayer.SetActive(true);
        
        SelectedPlayer.GetComponent<Rigidbody>().velocity = Vector3.zero;
        SelectedPlayer.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        SelectedPlayer.GetComponent<Rigidbody>().isKinematic = false;
        SelectedPlayer.GetComponent<VehicleProperties>().ConeEffect.SetActive(false);
        SelectedPlayer.GetComponent<CarShadow>().enabled = true;
    }
    public void SetTransform(Transform playerposition, Transform defulcar)
    {
        
        TpsPlayer.transform.position = playerposition.position;
        TpsPlayer.transform.rotation = playerposition.rotation;
        
        Tpscamera.transform.position = playerposition.position;
        Tpscamera.transform.rotation = playerposition.rotation;
        
        SelectedPlayer.transform.position = defulcar.position;
        SelectedPlayer.transform.rotation = defulcar.rotation;
        
    }

  



    public void DAy()
    {
        snowParticleSystem.Play();
        RenderSettings.skybox = nightSkybox;
        directionalLightGO.GetComponent<Light>().intensity = 0.4f;
        snowParticleSystem.gameObject.SetActive(true);
    }
    
    public void Night()
    {
        RenderSettings.skybox = daySkybox;
        directionalLightGO.GetComponent<Light>().intensity = 0.9f;
            
        snowParticleSystem.Stop();
        snowParticleSystem.gameObject.SetActive(false);
    }

    public void RestCar()
    {
        GameManager.Instance.CurrentCar.GetComponent<RCC_CarControllerV3>().RestCar();
    }

    public Material[] CarEffect;
    public float multiplaxer;
    private float offset;

    void Update()
    {
       
        offset += Time.deltaTime * multiplaxer;
        foreach (var VARIABLE in CarEffect)
        {
            VARIABLE.mainTextureOffset=new Vector2(0, offset);
        }
       
    }
    
    
}
