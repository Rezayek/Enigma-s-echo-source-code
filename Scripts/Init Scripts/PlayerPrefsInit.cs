using UnityEngine;

public class PlayerPrefsInit : MonoBehaviour
{
    [SerializeField] bool activeUI = true;
    [Range(0,1f)]
    [SerializeField] private float audioVolumeNB;
    [Range(0, 1f)]
    [SerializeField] private float audioVolumeB;
    private void Awake()
    {
        if(PlayerPrefs.HasKey(PlayerPrefsNames.UIActive.ToString()) || !PlayerPrefs.HasKey(PlayerPrefsNames.UIActive.ToString()))
        {
            PlayerPrefs.SetString(PlayerPrefsNames.UIActive.ToString(), "off");
            //Cursor.visible = true;
            //Cursor.lockState = CursorLockMode.None;
            //PlayerPrefs.SetString(PlayerPrefsNames.UIActive.ToString(), "on");
        }


        //Set Audio Volumes
        if (!PlayerPrefs.HasKey(PlayerPrefsNames.AudioVolumeNB.ToString()))
        {
            PlayerPrefs.SetFloat(PlayerPrefsNames.AudioVolumeNB.ToString(), 0.8f);
        }

        if (!PlayerPrefs.HasKey(PlayerPrefsNames.AudioVolumeB.ToString()))
        {
            PlayerPrefs.SetFloat(PlayerPrefsNames.AudioVolumeB.ToString(), 0.8f);
        }

        //Set Senario 

        if (!PlayerPrefs.HasKey(PlayerPrefsNames.CurrentSenario.ToString()))
        {
            PlayerPrefs.SetString(PlayerPrefsNames.CurrentSenario.ToString(), Senarios.EvilBanshee.ToString());
        }

        if (!PlayerPrefs.HasKey(PlayerPrefsNames.CurrentSenarioVersion.ToString()))
        {
            PlayerPrefs.SetString(PlayerPrefsNames.CurrentSenarioVersion.ToString(), SenariosVersion.Version_1.ToString());
        }

        

    }

    private void Update()
    {
        //if (activeUI)
        //{
        //    PlayerPrefs.SetString(PlayerPrefsNames.UIActive.ToString(), "on");
        //}
        //else
        //{
        //    PlayerPrefs.SetString(PlayerPrefsNames.UIActive.ToString(), "off");
        //}


        //PlayerPrefs.SetFloat(PlayerPrefsNames.AudioVolumeNB.ToString(), audioVolumeNB);
        //PlayerPrefs.SetFloat(PlayerPrefsNames.AudioVolumeB.ToString(), audioVolumeB);

    }

    
}


