using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using ITS.AI;
using PlayerInteractive_Mediation;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public enum  CarNames
{
	Car_01=0,
	Car_02=1,
	Car_03=2, 
	Car_04=3, 
	Car_05=4,
	Car_06=5,
	Carn_07=6,
	Car_08=7,
	Car_09=8,
	Car_10=9,
	Car_11=10,
	Car_12=11,
	Car_13=12,
	Car_14=13,
	Car_15=14,
	Car_16=15,
	Car_17=16,
	Car_18=17,
	Car_19r=18,
	Car_20=19,
	Car_21=20,
	Car_22=21,
	Car_23=22,
	Car_24=23,
	Car_25=24,
}

public class VehicleProperties : MonoBehaviour
{
	//public CarNames Names;
    public Transform TpsPosition;
    public GameObject ConeEffect;
    public Rigidbody Rb;
    [FormerlySerializedAs("controller")] public RCC_CarControllerV3 CarController;
   // public GameObject Exuset;
    public GameObject AllAudioSource;

    [Header("SettIngWheel")]
    
    public bool isSetWheelsModel=false;
   // public GameObject Fire;
	private void Awake()
	{
		if (Rb==null)
		{
			Rb = GetComponent<Rigidbody>();
		}
		
		if (CarController==null)
		{
			CarController = GetComponent<RCC_CarControllerV3>();
		}
		/*if (mainCamera == null)
		{
			mainCamera = GameManager.Instance.mainCamera;
		}*/
	
		//CarName = Names.ToString();
	}
	
	private void Start()
	{
		AllAudioSource = transform.Find("All Audio Sources").gameObject;
		
	}



	private void OnDrawGizmos()
	{
		if (isSetWheelsModel)
		{
			isSetWheelsModel = false;
			GetComponent<RCC_CarControllerV3>().SetColliderToModel();
		}
	}


	public bool TrafficCarAi=false;
	public async void GetInCarForDrive()
	{
		
		if (FindObjectOfType<Pi_AdsCall>())
		{
			FindObjectOfType<Pi_AdsCall>().showInterstitialAD();
			PrefsManager.SetInterInt(1);
		}
		TrafficCarAi = false;
		//currentHealth = PrefsManager.Gethealth(CarName);
		StopCoroutine(CheckisGrounded());
		/*if (CarController.chassis)
		{
			CarController.chassis.GetComponent<RCC_Chassis>().enabled = true;
		}*/
		CarController.enabled = true;
		transform.name = "My PlayerCar";
		GetComponent<RCC_CameraConfig>()?.SetCameraSettingsNow();
		if (!TrafficCarAi)
		{
			ConeEffect.SetActive(false);
		}
		if (AllAudioSource != null)
		{
			AllAudioSource.SetActive(false);
		}
		else
		{
			AllAudioSource = transform.Find("All Audio Sources").gameObject;
			AllAudioSource?.SetActive(false);
		}
		Rb.drag=0.05f;
		if (Rb)
		{
			Rb.constraints = RigidbodyConstraints.None;
			Rb.isKinematic = false;
			Rb.useGravity = true;
		}
		GetComponent<RCC_CameraConfig>().enabled = true;
		
		if (GetComponent<TSSimpleCar>())
		{
			/*if (CarController.chassis)
			{
				if (CarController.chassis.GetComponent<RCC_Chassis>().ColliderParent!=null)
				{
					CarController.chassis.GetComponent<RCC_Chassis>().ColliderParent?.SetActive(true);
				}
			}*/
			GetComponent<TSSimpleCar>().enabled = false;
			GetComponent<TSTrafficAI>().enabled = false;
			GetComponent<TSAntiRollBar>().enabled = false;
			GetComponent<TSAntiRollBar>().enabled = false;
			//GetComponent<ChangeWheelTrafficToPlayer>().ChangeToPlayer();
		}
		
		CarController.FrontLeftWheelCollider.enabled = true;
		CarController.FrontRightWheelCollider.enabled = true;
		CarController.RearLeftWheelCollider.enabled = true;
		CarController.RearRightWheelCollider.enabled = true;
		
		
		CarController.FrontLeftWheelCollider.transform.GetChild(0).gameObject.SetActive(true);
		CarController.FrontRightWheelCollider.transform.GetChild(0).gameObject.SetActive(true);
		CarController.RearLeftWheelCollider.transform.GetChild(0).gameObject.SetActive(true);
		CarController.RearRightWheelCollider.transform.GetChild(0).gameObject.SetActive(true);
		//Exuset.SetActive(true);
		
		
		GetComponent<CarShadow>().enabled = true; 
		GetComponent<CarShadow>().ombrePlane.gameObject.SetActive(true);
		
	//	UpdateHealthText();
		
		await Task.Delay(2000);
		if (AllAudioSource != null)
		{
			AllAudioSource.SetActive(true);
		}
		if (FindObjectOfType<Pi_AdsCall>())
		{
			if (PrefsManager.GetInterInt()!=5)
			{
				FindObjectOfType<Pi_AdsCall>().loadInterstitialAD();
			}
		}
	}



	public async void GetOutVehicle()
	{
		if (FindObjectOfType<Pi_AdsCall>())
		{
			FindObjectOfType<Pi_AdsCall>().showInterstitialAD();
			PrefsManager.SetInterInt(1);
		}
		transform.GetComponent<Rigidbody>().velocity=Vector3.zero; 
		transform.GetComponent<Rigidbody>().angularVelocity=Vector3.zero; 
		GetComponent<CarShadow>().enabled = false;
		GetComponent<CarShadow>().ombrePlane.gameObject.SetActive(false);
		TrafficCarAi = true;

		if (AllAudioSource != null)
		{
			AllAudioSource.SetActive(false);
		}
		else
		{
			AllAudioSource = transform.Find("All Audio Sources").gameObject;
			AllAudioSource?.SetActive(false);
		}
		/*if (CarController.chassis)
		{
			CarController.chassis.GetComponent<RCC_Chassis>().enabled = false;
		}*/
	
		CarController.FrontLeftWheelCollider.enabled = false;
		CarController.FrontRightWheelCollider.enabled = false;
		CarController.RearLeftWheelCollider.enabled = false;
		CarController.RearRightWheelCollider.enabled = false;

		CarController.FrontLeftWheelCollider.transform.GetChild(0).gameObject.SetActive(false);
		CarController.FrontRightWheelCollider.transform.GetChild(0).gameObject.SetActive(false);
		CarController.RearLeftWheelCollider.transform.GetChild(0).gameObject.SetActive(false);
		CarController.RearRightWheelCollider.transform.GetChild(0).gameObject.SetActive(false);
		//Exuset.SetActive(false);
		CarController.enabled = false;
		GetComponent<RCC_CameraConfig>().enabled = false;
		
		//PrefsManager.Sethealth(CarName,currentHealth);
		
		
		if (GetComponent<TSSimpleCar>())
		{
			/*if (CarController.chassis)
			{
				if (CarController.chassis.GetComponent<RCC_Chassis>().ColliderParent != null)
				{
					CarController.chassis.GetComponent<RCC_Chassis>().ColliderParent?.SetActive(false);
				}
			}*/
			GetComponent<TSSimpleCar>().enabled = true;
			GetComponent<TSAntiRollBar>().enabled = true;
			GetComponent<TSAntiRollBar>().enabled = true;
			GetComponent<TSTrafficAI>().enabled = true;
			//GetComponent<ChangeWheelTrafficToPlayer>().ChangeToAI();
			
			//UpdateHealthText();
			
			enabled = false;
		}
		else if (!TrafficCarAi)
		{
			ConeEffect.SetActive(true);
			if (Grounded)
			{
				Rb.angularDrag = 0;
				Rb.drag = 50f;
				Rb.isKinematic = true;
				enabled = false;
			}
			else
			{
				Logger.ShowLog("Grounded Car is in the Air");
				Rb.isKinematic = true;
				await Task.Delay(500);
				Rb.isKinematic = false;
				transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0f);
				Rb.angularDrag = 0.05f;
				Rb.drag = 5f;
				StartCoroutine(CheckisGrounded());
			}
		}
		await Task.Delay(2000);
		if (FindObjectOfType<Pi_AdsCall>())
		{
			if (PrefsManager.GetInterInt()!=5)
			{
				FindObjectOfType<Pi_AdsCall>().loadInterstitialAD();
			}
		}
	}


	public IEnumerator CheckisGrounded()
	{
		Logger.ShowLog("Grounded StartDebugging....");
		yield return new WaitUntil(() => Grounded);
		Logger.ShowLog("Grounded is true "+Grounded);
		yield return new WaitForSeconds(1f);
		Rb.isKinematic = true;
		enabled = false;
	}
	
	public bool Grounded = false;
	public float groundCheckDistance=1.1f;
	RaycastHit hit;
	public LayerMask LayerMask;

	
    private void Update()
    {

        Debug.DrawRay(transform.position, -transform.up * groundCheckDistance, Color.green);
        if (Physics.Raycast(transform.position, -transform.up, out hit, groundCheckDistance, LayerMask))
        {
            Grounded = true;
        }
        else
        {
            Grounded = false;
           // scorevalue = Grounded ? scorevalue + groundCheckDistance * 5*Time.deltaTime : scorevalue;
            //UiManagerObject.instance.currentHeightText.text = "Jump: " + scorevalue.ToString("0");
        }
    }



    public async void OnTriggerEnter(Collider other)
    {
	    if (TrafficCarAi)
		    return;
	    if (other.gameObject.CompareTag(GameConstant.Tag_Coin))
	    {
		     LevelManager.instace.CoinSound.Play();
		  //  HHG_UiManager.instance.EffectForcoin.SetActive(true);
		    Invoke("OffCoinsEffect", 3f);
		    PrefsManager.SetCoinsValue(PrefsManager.GetCoinsValue() + 1);
		    LevelManager.instace.coinBar.GetComponentInChildren<Text>().text = PrefsManager.GetCoinsValue().ToString();
		    other.gameObject.SetActive(false);
		    await Task.Delay(2000);
		    other.gameObject.SetActive(true);
	    }
	    /*if (other.CompareTag(GameConstant.Tag_ScrrenShot) && !isTimeStopped)
	    {
		    if (LevelManager.instace.Canvas.GetComponent<RCC_DashboardInputs>().KMH >= other.GetComponent<speedcheck>().meterspeed)
		    {
			    other.gameObject.SetActive(false);
			    UiManagerObject.instance.rewradMoneyText.text = 1000 + "";
			    UiManagerObject.instance.SpeedCaputer[0].text=CarController.speed.ToString("00");
			    UiManagerObject.instance.SpeedCaputer[1].text=CarController.speed.ToString("00");
			    StopTimeAndCapture();
		    }
		    else
		    { 
			    UiManagerObject.instance.SpeedCaputer[0].text=CarController.speed.ToString("00");
			    UiManagerObject.instance.SpeedCaputer[1].text=CarController.speed.ToString("00");
			    UiManagerObject.instance.OnspeedCaputer();
		    }
	    }*/
    }
    

    public void OffCoinsEffect()
    {
	   // HHG_UiManager.instance.EffectForcoin.SetActive(false);
    }



    /*#region HeallthWork
    
    
    public float maxHealth = 100f;
    public float currentHealth;




    public Color[] colers;

    public void ApplyDamage(float damage)
    {
	    currentHealth -= damage;
	    UpdateHealthText();
    }

    public void UpdateHealthText()
    {
	    UiManagerObject.instance.HealthText.text = "" + currentHealth.ToString("F0");
	    currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
	    UiManagerObject.instance.FillhealthBar.fillAmount = (float)currentHealth / maxHealth;
	    if (currentHealth <= 100)
	    {
		    UiManagerObject.instance.FillhealthBar.color  = colers[0];
		    UiManagerObject.instance.Ripairebutton.SetActive(true);
		    Fire.SetActive(false);
	    }
	    if (currentHealth <= 50)
	    {
		    UiManagerObject.instance.FillhealthBar.color  = colers[1];
		    Fire.SetActive(false);
	    }
	    if (currentHealth <= 40)
	    {
		    UiManagerObject.instance.FillhealthBar.color  = colers[2];
		    Fire.SetActive(false);
	    }
	    if (currentHealth <= 30)
	    {
		    UiManagerObject.instance.FillhealthBar.color  = colers[3];
		    Fire.SetActive(true);
	    }
	    if (currentHealth <= 0)
	    {
		    currentHealth = 0;
		    Fire.SetActive(true);
		    DestroyCar();
	    }
	    
    }

    private void DestroyCar()
    {
	    transform.GetComponent<Rigidbody>().velocity=Vector3.zero; 
	    transform.GetComponent<Rigidbody>().angularVelocity=Vector3.zero;

	    CarController.engineRunning = false;
	    Invoke("onpanel",3f);
    }

    public void onpanel()
    {
	    UiManagerObject.instance.repairPanel.SetActive(true);
	    UiManagerObject.instance.HideGamePlay();
    }
    public string CarName = "";
    public void RepairCar()
    { 
	    currentHealth = maxHealth;
	    CarController.repairNow = true;
	    PrefsManager.SetCoinsValue(PrefsManager.GetCoinsValue() - 1000);
	    LevelManager.instace.coinBar.GetComponentInChildren<Text>().text = PrefsManager.GetCoinsValue().ToString();
	    Fire.SetActive(false);
	    CarController.engineRunning = true;
	    UiManagerObject.instance.FillhealthBar.color  = colers[0];
	    UiManagerObject.instance.FillhealthBar.fillAmount = 1;
	    UiManagerObject.instance.ShowGamePlay();
	    PrefsManager.Sethealth(CarName,currentHealth);
	  //  PlayerPrefs.SetFloat("CarHealth", currentHealth); 
	    UpdateHealthText();
	    UiManagerObject.instance.repairPanel.SetActive(false);
	    UiManagerObject.instance.Ripairebutton.SetActive(false);
    }
    #endregion
    #region Car Speed Work 

    

    private float originalFOV;
    public bool isTimeStopped = false;

    public Camera mainCamera;

  
    void StopTimeAndCapture()
    {
     
	    LevelManager.instace.vehicleCamera.gameObject.SetActive(false);
	    originalFOV = mainCamera.fieldOfView;
	    UiManagerObject.instance.HideGamePlay();
	    isTimeStopped = true;
	    mainCamera.gameObject.SetActive(true);
	    // Position the capture camera
	    mainCamera.transform.position = transform.position + transform.forward  *7 + Vector3.up * 2f;
	    mainCamera.transform.LookAt(transform);
        
	    StartCoroutine(CaptureAndShow());
	    Time.timeScale = 0;
    }

    System.Collections.IEnumerator CaptureAndShow()
    {
	    // isTransitioning = true;
	    yield return new WaitForEndOfFrame();
	    RenderTexture rt = new RenderTexture(Screen.width, Screen.height, 24);
	    mainCamera.targetTexture = rt;
	    Texture2D screenShot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
	    mainCamera.Render();
	    RenderTexture.active = rt;
	    screenShot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
	    screenShot.Apply();
	    mainCamera.targetTexture = null;
	    RenderTexture.active = null;
	    Destroy(rt);
	    // Show the captured image in the UI
	    UiManagerObject.instance.capturedImage.sprite = Sprite.Create(screenShot, new Rect(0, 0, screenShot.width, screenShot.height), new Vector2(0.5f, 0.5f));
	    UiManagerObject.instance.uiPanel.SetActive(true);
    }
    

    #endregion*/
}