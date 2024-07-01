using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using PlayerInteractive_Mediation;
using SickscoreGames.HUDNavigationSystem;
using UnityEngine;
public enum PlayerStatus
{
    ThirdPerson,CarDriving,BikeDriving
}
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    public delegate void VehicleInteraction(PlayerStatus Status);
    public static event VehicleInteraction OnVehicleInteraction;


    public PlayerStatus TpsStatus = PlayerStatus.ThirdPerson;
    
    [Header("ThirdPerson Stuff")]
    [Space(5)]

    public GameObject TPSPlayer;

    [Space(5)]
    [Header("Car Stuff")]
    public Transform VehicleCamera;


    public Transform TpsCamera; 
    public GameObject CurrentCar;
   // public Transform TrafficSpawn;
    public Transform Weather;
    public HUDNavigationSystem hud;
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        hud.PlayerCamera = TpsCamera.GetComponent<Camera>();
        hud.PlayerController = TPSPlayer.transform; 
    }

    public async void GetInVehicle()
    {
        if (CurrentCar == null)
        {
            return;
        }

        Time.timeScale = 1;
        UiManagerObject.instance.blankimage.SetActive(true);
        Invoke("offimage", 0.5f);
        UiManagerObject.instance.panels.CarControle.SetActive(true);
        UiManagerObject.instance.panels.TpsControle.SetActive(false);
        CurrentCar.GetComponent<Rigidbody>().angularDrag = 0.05f;
        if (CurrentCar.GetComponent<VehicleProperties>() == null)
        {
            return;
        }

        CurrentCar.GetComponent<VehicleProperties>().enabled = true;
        CurrentCar.GetComponent<DriftPhysics>().enabled = true;
        CurrentCar.GetComponent<DriftPhysics>().Awakewhenicall();
        CurrentCar.GetComponent<VehicleProperties>().GetInCarForDrive(); // = true;
        TPSPlayer.SetActive(false);
        TpsCamera.gameObject.SetActive(false);
        VehicleCamera.gameObject.SetActive(true);
        OnVehicleInteraction?.Invoke(PlayerStatus.CarDriving);
        await Task.Delay(50);
        TpsStatus = PlayerStatus.CarDriving;
        LevelManager.instace.vehicleCamera.SetTarget(CurrentCar);
        hud.PlayerCamera = VehicleCamera.GetComponentInChildren<Camera>();
        hud.PlayerController = CurrentCar.transform;
      
    }


    private void Update()
    {
        
        if (TpsStatus==PlayerStatus.CarDriving)
        {
            Weather.transform.position = VehicleCamera.transform.position;
            //TrafficSpawn.position = VehicleCamera.position;
           // TrafficSpawn.rotation = VehicleCamera.rotation;
        }
        else
        {
            Weather.transform.position = TpsCamera.transform.position;
            //TrafficSpawn.position = TpsCamera.position;
            //TrafficSpawn.rotation = TpsCamera.rotation;
        }
    }
    public void offimage()
    {
        UiManagerObject.instance.blankimage.SetActive(false);
    }
    public async void GetOutVehicle()
    {
        Time.timeScale = 1;
        UiManagerObject.instance.blankimage.SetActive(true);
        Invoke("offimage",0.5f);
        Logger.ShowLog("Here");
        UiManagerObject.instance.panels.CarControle.SetActive(false);
        UiManagerObject.instance.panels.TpsControle.SetActive(true);
        TPSPlayer.SetActive(true);
        TpsCamera.gameObject.SetActive(true);
        CurrentCar.GetComponent<Rigidbody>().velocity=Vector3.zero; 
        CurrentCar.GetComponent<Rigidbody>().angularVelocity=Vector3.zero; 
        VehicleCamera.gameObject.SetActive(false);
        CurrentCar.GetComponent<VehicleProperties>().GetOutVehicle() ;//= false;
        CurrentCar.GetComponent<VehicleProperties>().enabled = false;
        CurrentCar.GetComponent<DriftPhysics>().enabled = false;
        TPSPlayer.transform.position =CurrentCar.GetComponent<VehicleProperties>().TpsPosition.position; 
        TPSPlayer.transform.eulerAngles =new Vector3(0,CurrentCar.GetComponent<VehicleProperties>().TpsPosition.rotation.y,0);
        OnVehicleInteraction?.Invoke(PlayerStatus.ThirdPerson);
        await Task.Delay(50);
        TpsStatus = PlayerStatus.ThirdPerson;
        hud.PlayerCamera = TpsCamera.GetComponent<Camera>();
        hud.PlayerController = TPSPlayer.transform;
        
    }


    /*public void RepairCar()
    {
        CurrentCar.GetComponent<VehicleProperties>().RepairCar();
    }
    public void ResumwTime()
    {
        Time.timeScale = 1;
        mainCamera.gameObject.SetActive(false);
        LevelManager.instace.vehicleCamera.gameObject.SetActive(true);
        UiManagerObject.instance.ShowGamePlay();
        CurrentCar.GetComponent<VehicleProperties>(). isTimeStopped = false;
        UiManagerObject.instance.uiPanel.SetActive(false);
        
    }*/
}
