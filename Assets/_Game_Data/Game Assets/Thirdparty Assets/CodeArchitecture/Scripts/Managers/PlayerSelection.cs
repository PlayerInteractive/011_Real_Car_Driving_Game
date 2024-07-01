﻿using UnityEngine;
using System.Collections;
using System.Threading.Tasks;
using PlayerInteractive_Mediation;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
[System.Serializable]
public class Specification
{
    public int[] Values;
}

public class PlayerSelection : MonoBehaviour
{

    public Specification[] SpecificationValue;
    public GameObject[] sp_bar;
    public int[] Prices;
    int selectedPlayerValue = 0;
    public GameObject[] selectedDogArray;

    public GameObject nextBtn,
        backBtn,
        dogSelectionCanvas,
        menuCanvas,
        levelSelectionCanvas,
        lockSprite,
        LOADING,
        Play,
        notCash,
        successPannel,
        unlockPlayerButton,
        TestDriveButton,
        MainNextBack;

    public Text PriceText;
    bool isReadyForPurchase;
    int ActivePlayervalue = 1;
    int coinValue;
    public static PlayerSelection instance;


    [Header("CarSoldCutSSceane")] public GameObject Timeline;
    public PlayableDirector Director;
    public GameObject CutSceanCamera;

    public CanvasGroup GrageUi;
    public GameObject MainCamera;


    public Sprite[] CarSprites;

    public Image CarIconImage;

    void Start()
    {
        instance = this;
        Time.timeScale = 1;

    }

    public void OnNextPressed()
    {
        SoundManager.Instance.PlayOneShotSounds(SoundManager.Instance.click);
        if (selectedPlayerValue < selectedDogArray.Length - 1)
        {
            selectedPlayerValue++;

            ShowPlayerNow(selectedPlayerValue);
            CarIconImage.sprite = CarSprites[selectedPlayerValue];
        }

    }

    public void OnBackPressed()
    {
        SoundManager.Instance.PlayOneShotSounds(SoundManager.Instance.click);
        if (selectedPlayerValue > 0)
        {
            selectedPlayerValue--;
            ShowPlayerNow(selectedPlayerValue);
            CarIconImage.sprite = CarSprites[selectedPlayerValue];
        }

    }


    GameObject CurrentPlayer = null;

    public void ShowPlayerNow(int val)
    {

        ActivePlayervalue = val;
        PriceText.text = Prices[val] + "";
        if (PrefsManager.GetPlayerState(val) == 0 && val != 0)
        {
            PriceText.transform.parent.gameObject.SetActive(true);
            lockSprite.SetActive(true);
            Play.SetActive(false);
            unlockPlayerButton.SetActive(true);
            TestDriveButton.SetActive(true);
        }
        else
        {
            lockSprite.SetActive(false);
            PriceText.transform.parent.gameObject.SetActive(false);
            Play.SetActive(true);
            unlockPlayerButton.SetActive(false);
            TestDriveButton.SetActive(false);

        }

        for (int i = 0; i < selectedDogArray.Length; i++)
        {
            selectedDogArray[i].SetActive(false);

        }

        PrefsManager.SetSelectedPlayerValue(val);
        CurrentPlayer = selectedDogArray[val];
        CurrentPlayer.SetActive(true);
        for (int i = 0; i < sp_bar.Length; i++)
        {
            sp_bar[i].GetComponent<filled>().SetEndpoint(SpecificationValue[val].Values[i]);
        }

        if (ActivePlayervalue == selectedDogArray.Length - 1)
        {
            nextBtn.SetActive(false);
            backBtn.SetActive(true);
        }
        else if (ActivePlayervalue == 0)
        {
            nextBtn.SetActive(true);
            backBtn.SetActive(false);
        }
        else
        {
            nextBtn.SetActive(true);
            backBtn.SetActive(true);
        }

        selectedPlayerValue = ActivePlayervalue;
    }

    public void TestDrive()
    {

        lockSprite.SetActive(false);
        PriceText.transform.parent.gameObject.SetActive(false);
        Play.SetActive(true);
        unlockPlayerButton.SetActive(false);
        TestDriveButton.SetActive(false);
        PrefsManager.SetSelectedPlayerValue(selectedPlayerValue);
        SelectDogPlay();
    }

    public void Ps_PlayEvent()
    {
        SoundManager.Instance.PlayOneShotSounds(SoundManager.Instance.click);
        //AdmobAdsManager.Instance.ShowInt(GoToLevelSelect, false);
        GoToLevelSelect();
    }

    public void GoToLevelSelect()
    {
        // CurrentEnv.SetActive(false);
        for (int i = 0; i < selectedDogArray.Length; i++)
        {
            selectedDogArray[i].SetActive(false);
        }

        SelectDogPlay();
    }


    public void SelectDogPlay()
    {
        SoundManager.Instance.PlayOneShotSounds(SoundManager.Instance.click);
        fakeLoading.SetActive(true);
        levelSelectionCanvas.SetActive(true);
    }

    public void ForTutorialPlay()
    {
        levelSelectionCanvas.SetActive(true);
        menuCanvas.SetActive(false);
        Debug.Log("Enable Here");
        LOADING.SetActive(true);
        PrefsManager.SetGameMode("challange");
        PrefsManager.SetCurrentLevel(1);
        PrefsManager.SetLevelMode(0);

    }

    public GameObject fakeLoading;

    public void RedirectToDogSelection()
    {
        //  AdmobAdsManager.Instance.LoadInterstitialAd();
        SoundManager.Instance.PlayOneShotSounds(SoundManager.Instance.click);

        fakeLoading.SetActive(true);
        dogSelectionCanvas.SetActive(true);
        menuCanvas.SetActive(false);
        ShowPlayerNow(PrefsManager.GetLastJeepUnlock());
        MainNextBack.SetActive(true);
        selectedPlayerValue = PrefsManager.GetLastJeepUnlock();
        CarIconImage.sprite = CarSprites[selectedPlayerValue];
        onscripte();
    }

    public void BackToMainCanvas()
    {
        //  AdmobAdsManager.Instance.LoadInterstitialAd();
        SoundManager.Instance.PlayOneShotSounds(SoundManager.Instance.click);
        fakeLoading.SetActive(true);
        dogSelectionCanvas.SetActive(false);
        menuCanvas.SetActive(true);
        Invoke("offscripte", 1);
        Logger.ShowLog("Enable Here");
    }

    public void BackFromLevelScreen()
    {
        SoundManager.Instance.PlayOneShotSounds(SoundManager.Instance.click);
        fakeLoading.SetActive(true);
        levelSelectionCanvas.SetActive(false);
        dogSelectionCanvas.SetActive(true);
        ShowPlayerNow(PrefsManager.GetSelectedPlayerValue());
        MainNextBack.SetActive(true);
        selectedPlayerValue = PrefsManager.GetSelectedPlayerValue();

    }

    public void SetLevelValue(int lValue)
    {
        //AdmobAdsManager.Instance.ShowInt(LoadGamePlay,false);
        LoadGamePlay();
        SoundManager.Instance.PlayOneShotSounds(SoundManager.Instance.click);
        PrefsManager.SetCurrentLevel(lValue);
    }

    public void LoadGamePlay()
    {
        LOADING.SetActive(true);
        LOADING.GetComponentInChildren<bl_SceneLoader>().LoadLevel("GamePlay");
        if (PrefsManager.GetInterInt() != 5)
        {
            FindObjectOfType<Pi_AdsCall>().loadInterstitialAD();
        }

        Invoke(nameof(showInterAd), 7f);
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


    public void UnlockSelectedDog()
    {
        UnlockDog(Prices[ActivePlayervalue]);
    }

    public void UnlockDog(int dogVal)
    {
        SoundManager.Instance.PlayOneShotSounds(SoundManager.Instance.click);
        if (PrefsManager.GetCoinsValue() >= dogVal)
        {
            PrefsManager.SetPlayerState(ActivePlayervalue, 1);
            PrefsManager.SetCoinsValue(PrefsManager.GetCoinsValue() - dogVal);
            Success_purchase();
            PrefsManager.SetLastJeepUnlock(ActivePlayervalue);
            Play.SetActive(true);
            unlockPlayerButton.SetActive(false);
            TestDriveButton.SetActive(false);
        }
        else
        {
            notCash.SetActive(true);
        }
    }

    public void OffNoCash()
    {
        notCash.SetActive(false);
    }

    public void Offsuccess()
    {
        successPannel.SetActive(false);
    }

    public void Success_purchase()
    {
        Time.timeScale = 1f;
        GrageUi.alpha = 0;
        CutSceanCamera.SetActive(true);
        MainCamera.SetActive(false);
        Timeline.SetActive(true);
        Director.Play();
        Invoke("HideTimeline", (float)Director.duration - 0.9f);

    }



    public async void PlayCutScne()
    {
        Time.timeScale = 1f;
        if (FindObjectOfType<Pi_AdsCall>())
        {
            if (PrefsManager.GetInterInt() != 5)
            {
                FindObjectOfType<Pi_AdsCall>().loadInterstitialAD();
            }
        }

        if (FindObjectOfType<Pi_AdsCall>())
        {
            FindObjectOfType<Pi_AdsCall>().showInterstitialAD();
            PrefsManager.SetInterInt(1);
        }

        GrageUi.alpha = 0;
        CutSceanCamera.SetActive(true);
        MainCamera.SetActive(false);
        Timeline.SetActive(true);
        Director.Play();
        Invoke("StopCutScene", (float)Director.duration - 0.9f);
        await Task.Delay(2000);
        if (FindObjectOfType<Pi_AdsCall>())
        {
            if (PrefsManager.GetInterInt() != 5)
            {
                FindObjectOfType<Pi_AdsCall>().loadInterstitialAD();
            }
        }
    }


    public void StopCutScene()
    {
        GrageUi.alpha = 1;
        CutSceanCamera.SetActive(false);
        MainCamera.SetActive(true);
        Timeline.SetActive(false);
    }

    public void HideTimeline()
    {
        GrageUi.alpha = 1;
        CutSceanCamera.SetActive(false);
        MainCamera.SetActive(true);
        Timeline.SetActive(false);





        isReadyForPurchase = true;
        successPannel.SetActive(true);
        lockSprite.SetActive(false);
        unlockPlayerButton.SetActive(false);
        Play.SetActive(true);
        Invoke("Offsuccess", 3f);
        PriceText.transform.parent.gameObject.SetActive(false);
    }







    public GameObject menu, view;
    
    
    public void Cameraviwe()
    {
        MainCamera.GetComponent<CameraRotate>().enabled = true;
        menu.SetActive(false);
        view.SetActive(true);
      
    }

    public void OutCameravi()
    {
        menu.SetActive(true);
        view.SetActive(false);
        MainCamera.GetComponent<CameraRotate>().SetMianPos();
        Invoke("offscripte",1f);
    }

    public void offscripte()
    {
        MainCamera.GetComponent<CameraRotate>().enabled = false;
    }
    public void onscripte()
    {
        MainCamera.GetComponent<CameraRotate>().enabled = true;
    }
}
