using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FL_LevelLockingChecker : MonoBehaviour
{
	public int ID;
    private int LockingValue = 0;
    // Use this for initialization
    void Start ()
	{

        if (PrefsManager.GetLevelMode() == 0)
            LockingValue = PrefsManager.GetLevelLocking();
        else if (PrefsManager.GetLevelMode() == 1)
            LockingValue = PrefsManager.GetSnowLevelLocking();
      //  else if (PrefsManager.GetLevelMode() == 2)
	       // LockingValue = PrefsManager.GetParkingMode;
          


        if (LockingValue > ID)
		{
			transform.GetChild (0).gameObject.SetActive (true);
			transform.GetChild (1).gameObject.SetActive (false);
			transform.GetChild (2).gameObject.SetActive (false);
			gameObject.GetComponent<Button> ().interactable = true;
			GetComponent<Button>().enabled = true;
		}
        else if (LockingValue == ID)
        {
	        transform.GetChild(1).gameObject.SetActive(true);
	        transform.GetChild(2).gameObject.SetActive(false);
	        gameObject.GetComponent<Button>().interactable = true;
	        GetComponent<Button>().enabled = true;
        }
        else
        {
	        GetComponent<Button>().enabled = false;
        }
        {
	        
        }

	}
	

}
