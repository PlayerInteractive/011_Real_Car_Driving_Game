using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using GameAnalyticsSDK;
using PlayerInteractive_Mediation;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiManagerObject : MonoBehaviour
{
    [Serializable]
    public class Panels
    {
        //public GameObject tachometer;
        public GameObject CarControle;
        public GameObject TpsControle;
        public GameObject  RateUsPanel;
    }
    public Panels panels;
    // Start is called before the first frame update
    public GameObject ObjectivePannel, Pause, Fail, Complete, Loading,OutOfFuel,error,deadpanel;
    public Text NosCountText;
    public static UiManagerObject instance;
    public int TotalLevels;
    public Image fuelBar;
    public GameObject blankimage;
    public Image NosFiller;
    public GameObject NosButton;
    // Text UI for displaying current height
    /*[Header("health WORK")] 
    public Text HealthText;
    public GameObject repairPanel ,Ripairebutton;
    public Image FillhealthBar;
    [Header("SS WORK")]
    public GameObject uiPanel;    // The UI Panel to show the captured picture
    public Image capturedImage;   // The Image component to display the captured picture
    public Button resumeButton;
    public Text rewradMoneyText;
    public Text[] SpeedCaputer;
    public GameObject ToSlowPanel;*/
    void Awake()
    {

        instance = this;
        SoundManager.Instance.PlayAudio(SoundManager.Instance.BgSound);
        Time.timeScale = 1f;
        /*LevelManager.instace.SelectedPlayer.GetComponent<VehicleProperties>().UpdateHealthText();
        FillhealthBar.color  = LevelManager.instace.SelectedPlayer.GetComponent<VehicleProperties>().colers[0];
        Ripairebutton.SetActive(false);
        uiPanel.SetActive(false);*/
    }

    /*public void OnspeedCaputer()
    {
        ToSlowPanel.SetActive(true);
        ToSlowPanel.GetComponent<Animator>().Play("SpeedChacker");
        Invoke("OFFspeedCaputer",3.5f);
    }
    public void OFFspeedCaputer()
    {
        ToSlowPanel.SetActive(false);
    }*/
    
    void Start()
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, PrefsManager.GetGameMode(),PrefsManager.GetCurrentLevel());
    }
    void OnEnable()
    {
       
        if (FindObjectOfType<Pi_AdsCall>())
        {
            FindObjectOfType<Pi_AdsCall>().showBanner1();
            FindObjectOfType<Pi_AdsCall>().showBanner2();
            FindObjectOfType<Pi_AdsCall>().hideBigBanner();
            if (PrefsManager.GetInterInt()!=5)
            {
                FindObjectOfType<Pi_AdsCall>().loadInterstitialAD();
            }
        }
    }
    public void OnRateusPanel()
    {
        panels.RateUsPanel.SetActive(true);
        GameManager.Instance.CurrentCar.GetComponent<Rigidbody>().velocity = Vector3.zero;
        GameManager.Instance.CurrentCar.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        HideGamePlay();
    }


    public void FillFuelbar(float fillAmount) {
        fuelBar.fillAmount = fillAmount;
        if (fillAmount <= 0) {
            OutOfFuel.SetActive(true);
        }
    }

    public void FillFuelTank() {
        if (PrefsManager.GetCoinsValue() > 1000)
            PrefsManager.SetCoinsValue(PrefsManager.GetCoinsValue() - 1000);
        else { 
            error.SetActive(true);
            Invoke("OffError",4f);
        }
        OutOfFuel.SetActive(false);
    }


    public void OffError() {
        error.SetActive(false);

    }

   public void FullTankWithVideo() { 
        //LevelManager.instace.SelectedPlayer.GetComponent<RCC_CarControllerV3>().FillFullTank();
        OutOfFuel.SetActive(false);

    }

   public void HideGamePlay()
   {
       LevelManager.instace.canvashud.gameObject.SetActive(false);
       Logger.ShowLog("Here");
       panels. CarControle.SetActive(false);
       panels.TpsControle.SetActive(false);
     
   }

   public void ShowGamePlay()
   {
      
       if (GameManager.Instance.TpsStatus == PlayerStatus.BikeDriving)
       {
           panels.CarControle.SetActive(false);
           panels.TpsControle.SetActive(false);
       }

       if (GameManager.Instance.TpsStatus == PlayerStatus.CarDriving)
       {
           panels. CarControle.SetActive(true);
           panels. TpsControle.SetActive(false);
       }

       if (GameManager.Instance.TpsStatus == PlayerStatus.ThirdPerson)
       {
           panels. CarControle.SetActive(false);
           panels. TpsControle.SetActive(true);
       }
       LevelManager.instace.canvashud.gameObject.SetActive(true);
   }

    public void HideObjectivePannel()
    {
        ObjectivePannel.SetActive(false);
        SetTimeScale(1);
        ShowGamePlay();
       
    }

    public void ShowPause()
    {
        SoundManager.Instance.PlayOneShotSounds(SoundManager.Instance.click);
        ShowPauseNow();
    }

    public async void ShowPauseNow()
    {
        if (FindObjectOfType<Pi_AdsCall>())
        {
            FindObjectOfType<Pi_AdsCall>().showInterstitialAD();
            PrefsManager.SetInterInt(1);
        }

        Pause.SetActive(true);
        HideGamePlay();
        LevelManager.instace.vehicleCamera.
                GetComponentInChildren<Camera>().GetComponent<AudioListener>().enabled = false;
        SetTimeScale(0);
        await Task.Delay(2000);
        if (FindObjectOfType<Pi_AdsCall>())
        {
            if (PrefsManager.GetInterInt() != 5)
            {
                FindObjectOfType<Pi_AdsCall>().loadInterstitialAD();
            }
        }

        GameManager.Instance.CurrentCar.GetComponent<Rigidbody>().velocity = Vector3.zero;
        GameManager.Instance.CurrentCar.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }

    public void Resume()
    {
        SoundManager.Instance.PlayOneShotSounds(SoundManager.Instance.click);
        Pause.SetActive(false);
        ShowGamePlay();
        SetTimeScale(1);
        LevelManager.instace.vehicleCamera.
            GetComponentInChildren<Camera>().GetComponent<AudioListener>().enabled = true;
        if (FindObjectOfType<Pi_AdsCall>())
        {
            if (PrefsManager.GetInterInt() != 5)
            {
                FindObjectOfType<Pi_AdsCall>().loadInterstitialAD();
            }
        }
    }

    public void Restart()
    {
        SoundManager.Instance.PlayOneShotSounds(SoundManager.Instance.click);
        SetTimeScale(1);
        Loading.SetActive(true);
        Loading.GetComponentInChildren<bl_SceneLoader>().LoadLevel("GamePlay");
        if (PrefsManager.GetInterInt()!=5)
        {
            FindObjectOfType<Pi_AdsCall>().loadInterstitialAD();
        }
        Invoke(nameof(showInterAd),5f);
    }

    public void Home()
    {
        SoundManager.Instance.PlayOneShotSounds(SoundManager.Instance.click);
        SetTimeScale(1);
        Loading.SetActive(true);
        Loading.GetComponentInChildren<bl_SceneLoader>().LoadLevel("MenuScene");
        if (PrefsManager.GetInterInt()!=5)
        {
            FindObjectOfType<Pi_AdsCall>().loadInterstitialAD();
        }
        Invoke(nameof(showInterAd),5f);
    }
    public GameObject AdBrakepanel;
    public async void showInterAd()
    {
        AdBrakepanel.SetActive(true);
        await Task.Delay(1000);
        if (FindObjectOfType<Pi_AdsCall>())
        {
            FindObjectOfType<Pi_AdsCall>().showInterstitialAD();
			
            PrefsManager.SetInterInt(1);
        }
        AdBrakepanel.SetActive(false);
    }

    public void LevelCompleteNow()
    {
        Complete.SetActive(true);
        if (PrefsManager.GetGameMode() != "free")
        {

            if (PrefsManager.GetLevelMode() == 0)
            {
                Debug.Log("FirstMode"+PrefsManager.GetCurrentLevel()+" "+PrefsManager.GetLevelLocking());
                if (PrefsManager.GetCurrentLevel() >= PrefsManager.GetLevelLocking())
                {
                    PrefsManager.SetLevelLocking(PrefsManager.GetLevelLocking() + 1);

                }
                Debug.Log("FirstMode"+PrefsManager.GetCurrentLevel()+" "+PrefsManager.GetLevelLocking());
            }
            else if (PrefsManager.GetLevelMode() == 1)
            {
                Debug.Log("SnowMode"+PrefsManager.GetCurrentLevel()+" "+PrefsManager.GetSnowLevelLocking());
                if ((PrefsManager.GetCurrentLevel()) >= PrefsManager.GetSnowLevelLocking())
                {
                    PrefsManager.SetSnowLevelLocking(PrefsManager.GetSnowLevelLocking() + 1);

                }
                Debug.Log("SnowMode"+PrefsManager.GetCurrentLevel()+" "+PrefsManager.GetSnowLevelLocking());
            }
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, PrefsManager.GetGameMode(), PrefsManager.GetCurrentLevel());
        }
    }

    public async void ShowComplete()
    {
        LevelCompleteNow();
        if (FindObjectOfType<Pi_AdsCall>())
        {
            FindObjectOfType<Pi_AdsCall>().showInterstitialAD();
            PrefsManager.SetInterInt(1);
        }

        SoundManager.Instance?.PlayAudio(SoundManager.Instance.LevelComplete);
        await Task.Delay(2000);
        if (FindObjectOfType<Pi_AdsCall>())
        {
            if (PrefsManager.GetInterInt() != 5)
            {
                FindObjectOfType<Pi_AdsCall>().loadInterstitialAD();
            }
        }
    }

    public  async void ShowFail()
    {
       
        SoundManager.Instance.PlayAudio(SoundManager.Instance.levelFail);
        if (FindObjectOfType<Pi_AdsCall>())
        {
            FindObjectOfType<Pi_AdsCall>().showInterstitialAD();
            PrefsManager.SetInterInt(1);
        }

        ShowLevelFailNow();
        await Task.Delay(2000);
        if (FindObjectOfType<Pi_AdsCall>())
        {
            if (PrefsManager.GetInterInt() != 5)
            {
                FindObjectOfType<Pi_AdsCall>().loadInterstitialAD();
            }
        }
    }

    public void ShowLevelFailNow()
    {
        
        Fail.SetActive(true);
        SetTimeScale(0);
          GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, PrefsManager.GetGameMode(), PrefsManager.GetCurrentLevel());
    }

    public void Next()
    {
     
        PrefsManager.SetCurrentLevel(PrefsManager.GetCurrentLevel()+1);
        Loading.SetActive(true);
        Complete.SetActive(false);
        if (PrefsManager.GetCurrentLevel() >= TotalLevels)
        { 
            Loading.GetComponentInChildren<bl_SceneLoader>().LoadLevel("MenuScene");

        }
        else
        {
            Loading.GetComponentInChildren<bl_SceneLoader>().LoadLevel("GamePlay");
        }
        SetTimeScale(1);
    }
    public void SetTimeScale(float timescale)
    {
        Time.timeScale = timescale;
    }
}
