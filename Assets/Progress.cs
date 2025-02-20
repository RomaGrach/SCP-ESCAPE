using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Accessibility;
using YG;
using static UnityEngine.AudioSettings;

/*

Progress.Instance.PlayerInfo.Coins
Progress.Instance.SaveProgres();

*/




[System.Serializable]
public class PlayerInfo
{
    public int LevelProgress = 0;
    public int MaxScore = 0;
    public int money = 0;
    public bool Desktop = true;
    public bool FirstGame = true;
}



public class Progress : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void ShowAdv();
    [DllImport("__Internal")]
    private static extern void SaveExtern(string date);
    [DllImport("__Internal")]
    private static extern void LoadExtern();

    public static Progress Instance;
    public PlayerInfo PlayerInfo;
    public bool Yandex = false;
    public bool Desktop = false;
    public bool YandexSDK = false;
    public bool Test = false;
    public bool PlayerDidSomething = false;
    public bool PlayerFirstGame = true;
    public bool DebugRejim = false;

    private void OnEnable() => YandexGame.GetDataEvent += SetPlayerInfoYandexSDK;

    private void OnDisable() => YandexGame.GetDataEvent -= SetPlayerInfoYandexSDK;

    private void Awake()
    {
        /*
        Yandex = false;
#if UNITY_WEBGL
        Yandex = true;
        Debug.Log("Unity WEBGL");
#endif
#if UNITY_EDITOR
        Yandex = false;
        Debug.Log("Unity Editor");
#endif
        */

        if (Instance == null)
        {
            transform.parent = null;
            DontDestroyOnLoad(gameObject);
            Instance = this;
            //StartCoroutine(WaitTime());
        }
        else
        {
            Destroy(gameObject);
        }
        //Instance.PlayerInfo = YandexGame.savesData.PlayerInfo;
    }
    private IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(2f);
        Test = true;
        //Debug.Log("WaitTime" + PlayerInfo.Coins);
        //DownloadProgress();
        //Debug.Log("WaitTime" + PlayerInfo.Coins);

    }


    // Start is called before the first frame update
    void Start()
    {
        if (Instance.PlayerInfo == null)
        {
            resetProgress();
            Debug.Log("____________________Ошибка Instance.PlayerInfo___________________");

        }
        //YandexGame.ResetSaveProgress();
        if (Instance.PlayerInfo.FirstGame)
        {
            //Instance.PlayerInfo.FirstGame = false;

            resetProgress();
            Instance.PlayerInfo.FirstGame = false;
            SaveProgres();
        }
        if (Yandex)
        {
            Instance.PlayerInfo.Desktop = YandexGame.EnvironmentData.isDesktop;
            YandexGame.OpenFullAdEvent += StopGame;
            YandexGame.CloseFullAdEvent += PlayGame;
            YandexGame.OpenVideoEvent += StopGame;
            YandexGame.CloseVideoEvent += PlayGame;
            //YandexGame.RewardVideoEvent += Reward;
            YandexGame.FullscreenShow();
        }
        Instance.PlayerInfo.Desktop = Desktop;


    }

    void StopGame()
    {
        Time.timeScale = 0f;
        AudioListener.volume = 0f;
    }

    void PlayGame()
    {
        Time.timeScale = 1f;
        AudioListener.volume = 1f;
    }



    // Update is called once per frame
    void Update()
    {
        if (DebugRejim)
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                if (PlayerInfo == null)
                {
                    Debug.Log("____________________Ошибка PlayerInfo___________________");
                }
                if (Instance.PlayerInfo == null)
                {
                    resetProgress();
                    Debug.Log("____________________Ошибка Instance.PlayerInfo___________________");

                }
                if (PlayerInfo == null)
                {
                    Debug.Log("____________________Ошибка PlayerInfo___________________");
                }
                if (Instance.PlayerInfo == null)
                {
                    Instance.PlayerInfo = new PlayerInfo();
                    Instance.PlayerInfo.Desktop = Desktop;
                    Debug.Log("____________________Ошибка Instance.PlayerInfo___________________");

                }
                foreach (var field in Instance.PlayerInfo.GetType().GetFields())
                {
                    var value = field.GetValue(PlayerInfo);
                    Debug.Log(field.Name + ": " + value);
                }

            }
        }
    }
    public void SaveProgres()
    {

        Debug.Log("SaveProgres" + Yandex);
        if (Yandex)
        {
            //SaveProgresYandex();
            SaveProgresYandexSDK();
        }
        else
        {
            SaveProgresPlayerPrefs();
        }
        Debug.Log(PlayerInfo);

    }


    public void DownloadProgress()
    {

        Debug.Log("DownloadProgress" + Yandex);
        if (Yandex)
        {
            //LoadExtern();
            SetPlayerInfoYandexSDK();
        }
        else
        {
            DownloadProgressPlayerPrefs();
        }
        Debug.Log(PlayerInfo);

    }

    public void DownloadProgressPlayerPrefs()
    {
        if (PlayerPrefs.HasKey("ProgresSave"))
        {
            string jsonString = PlayerPrefs.GetString("ProgresSave");
            PlayerInfo = JsonUtility.FromJson<PlayerInfo>(jsonString);
        }
    }

    public void SaveProgresPlayerPrefs()
    {
        string jsonString = JsonUtility.ToJson(PlayerInfo);
        PlayerPrefs.SetString("ProgresSave", jsonString);
    }

    public void resetProgress()
    {
        Debug.Log("____________________resetProgress___________________");
        PlayerInfo = new PlayerInfo();
        Instance.PlayerInfo.Desktop = Desktop;
        SaveProgres();
    }

    public void SaveProgresYandex()
    {
        string jsonString = JsonUtility.ToJson(PlayerInfo);
        SaveExtern(jsonString);
    }

    public void SaveProgresYandexSDK()
    {
        //YandexGame.savesData.PlayerInfo = Instance.PlayerInfo;
        /*
        YandexGame.savesData.Coins= Instance.PlayerInfo.Coins;
        YandexGame.savesData.Killed =Instance.PlayerInfo.Killed;
        YandexGame.savesData.Flight =Instance.PlayerInfo.Flight;
        YandexGame.savesData.MaxHP =Instance.PlayerInfo.MaxHP;
        YandexGame.savesData.Damage =Instance.PlayerInfo.Damage;
        YandexGame.savesData.Armor =Instance.PlayerInfo.Armor;
        YandexGame.savesData.TimeBetwinShots =Instance.PlayerInfo.TimeBetwinShots;
        YandexGame.savesData.TimeBetwinShots37 =Instance.PlayerInfo.TimeBetwinShots37;
        YandexGame.savesData.Level =Instance.PlayerInfo.Level;
        YandexGame.savesData.Hprice =Instance.PlayerInfo.Hprice;
        YandexGame.savesData.Dprice =Instance.PlayerInfo.Dprice;
        YandexGame.savesData.Aprice =Instance.PlayerInfo.Aprice;
        YandexGame.savesData.Sprice =Instance.PlayerInfo.Sprice;
        YandexGame.savesData.S23price =Instance.PlayerInfo.S23price;
        YandexGame.savesData.S37price = Instance.PlayerInfo.S37price;
        Debug.Log("2222222222222");
        Debug.Log(Instance.PlayerInfo.Coins.ToString());
        Debug.Log(YandexGame.savesData.Coins.ToString());
        Debug.Log("2222222222222");
        */
        YandexGame.SaveProgress();


    }

    public void SetPlayerInfoYandexSDK()
    {
        //Instance.PlayerInfo = YandexGame.savesData.PlayerInfo;
        /*
        YandexGame.ResetSaveProgress();
        Instance.PlayerInfo.Coins = YandexGame.savesData.Coins;
        Instance.PlayerInfo.Killed =YandexGame.savesData.Killed;
        Instance.PlayerInfo.Flight =YandexGame.savesData.Flight;
        Instance.PlayerInfo.MaxHP =YandexGame.savesData.MaxHP;
        Instance.PlayerInfo.Damage =YandexGame.savesData.Damage;
        Instance.PlayerInfo.Armor =YandexGame.savesData.Armor;
        Instance.PlayerInfo.TimeBetwinShots =YandexGame.savesData.TimeBetwinShots;
        Instance.PlayerInfo.TimeBetwinShots37 =YandexGame.savesData.TimeBetwinShots37;
        Instance.PlayerInfo.Level =YandexGame.savesData.Level;
        Instance.PlayerInfo.Hprice =YandexGame.savesData.Hprice;
        Instance.PlayerInfo.Dprice =YandexGame.savesData.Dprice;
        Instance.PlayerInfo.Aprice =YandexGame.savesData.Aprice;
        Instance.PlayerInfo.Sprice =YandexGame.savesData.Sprice;
        Instance.PlayerInfo.S23price =YandexGame.savesData.S23price;
        Instance.PlayerInfo.S37price =YandexGame.savesData.S37price;
        Debug.Log("11111111111111111111");
        Debug.Log(Instance.PlayerInfo.Coins.ToString());
        Debug.Log(YandexGame.savesData.Coins.ToString());
        Debug.Log("11111111111111111111");
        */
    }

    public void SetPlayerInfoYandex(string value)
    {
        PlayerInfo = JsonUtility.FromJson<PlayerInfo>(value);
    }

    public void PlayAdv()
    {
        if (Yandex)
        {
            YandexGame.FullscreenShow();
        }

        /*
        if (Yandex)
        {
            Time.timeScale = 0f;
            AudioListener.volume = 0f;
            ShowAdv();
        }
        */
    }

    public void StopAdv()
    {
        Time.timeScale = 1f;
        AudioListener.volume = 1f;

    }


}
