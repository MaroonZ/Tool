using UnityEngine;
using System.Collections;
using System;
using System.Text;
using UnityEngine.UI;
using System.IO;

/// <summary>
/// 
/// </summary>
public class CheckTime : MonoBehaviour {

    #region  

    #endregion
    public string projectName;
    string expirationDateCode;
    string[] checkTimeData;
    int version;
    #region  

    #endregion
    void Start () {
        StartCheckTime();
        //Check(0,2018,06,30);
    }
    /// <summary>
    /// 根据ExpirationDate.txt内的base64解密后，获取相关数据，并调用时效检测
    /// </summary>
    void StartCheckTime()
    {
        if (!File.Exists(Application.streamingAssetsPath + "/ExpirationDate.txt"))
        {
            Debug.Log("没有找到时间信息文件！");
            Application.Quit(); return;
        }
        expirationDateCode = Encoding.Default.GetString(File.ReadAllBytes(Application.streamingAssetsPath + "/ExpirationDate.txt"));
        try
        {
            expirationDateCode = Base64Code.DecodeBase64(Encoding.UTF8, expirationDateCode);
        }
        catch
        {
            Debug.Log("时间信息被修改了！不让你玩了！");
            Application.Quit(); return;
        }
        if (expirationDateCode.Split('+').Length == 0)
        {
            Debug.Log("日期被修改了！不让你玩了！");
            Application.Quit();return;
        }
        version = int.Parse((expirationDateCode.Split('+'))[0]);
        string temp = (expirationDateCode.Split('+'))[1];
        checkTimeData = temp.Split('/');
        if (checkTimeData.Length ==3)
        {
            Check(version, int.Parse(checkTimeData[0]), int.Parse(checkTimeData[1]), int.Parse(checkTimeData[2]));
        }
        else if(checkTimeData.Length == 1)
        {
            Check(version, int.Parse(checkTimeData[0]));
        }
        else
        {
            Debug.Log("日期被修改了！不让你玩了！");
            Application.Quit(); return;
        }
    }

    /// <summary>
    /// 按在当前电脑第一次开启此游戏包，超过多少天后会自动退出游戏。每更新一次包，需将检查时间里的版本号加1，避免同样的playerprefs产生冲突。
    /// </summary>
    /// <param name="version">版本号</param>
    /// <param name="expirationDate">销毁时间（几天后）</param>
    void Check(int version, int expirationDate)
    {
        int state = -1;

        if (PlayerPrefs.HasKey("aogastate" + projectName +  version.ToString()))
        {
            state = PlayerPrefs.GetInt("aogastate" + projectName + version.ToString());
            if (state > 0)
            {
                Debug.Log("退出游戏");
                Application.Quit();
                return;
            }
        }
        CheckLastGameTime(version);

        if (PlayerPrefs.HasKey("aogatime" + projectName + version.ToString()))
        {
            long t = long.Parse(PlayerPrefs.GetString("aogatime" + projectName + version.ToString()));
            DateTime GameExpiredTime = new DateTime(t);
            print("当前PC时间：" + DateTime.Now);
            print("游戏到期时间：" + GameExpiredTime);

            if (DateTime.Now.Ticks >= t)
            {
                Debug.Log("已超出游戏到期时间，退出游戏");
                PlayerPrefs.SetInt("aogastate" + projectName + version.ToString(), 1);
                PlayerPrefs.Save();
                Application.Quit();

            }
        }
        else
        {
            if (state == -1)
            {
                long t = DateTime.Now.AddDays(expirationDate).Ticks;
                PlayerPrefs.SetString("aogatime" + projectName + version.ToString(), t.ToString());
                PlayerPrefs.SetInt("aogastate" + projectName + version.ToString(), 0);
                PlayerPrefs.Save();

                long n = long.Parse(PlayerPrefs.GetString("aogatime" + projectName + version.ToString()));
                DateTime GameExpiredTime = new DateTime(n);
                print("当前PC时间：" + DateTime.Now);
                print("游戏到期时间：" + GameExpiredTime);
            }
            else
            {
                Debug.Log("退出游戏");
                Application.Quit();
            }
        }
    }

    /// <summary>
    /// 按具体日期来检测，到具体日期后的一天就退出
    /// </summary>
    /// <param name="version">版本号，每出一个新的包就版本号+1</param>
    /// <param name="year">年</param>
    /// <param name="month">月</param>
    /// <param name="day">日</param>
    void Check(int version, int year, int month, int day)
    {
        CheckLastGameTime(version);
        DateTime curPCTime = DateTime.Now;
        DateTime GameExpiredTime = new DateTime(year, month, day, 23, 59, 59);

        print("当前系统时间为： " + curPCTime);
        print("游戏到期日期为： " + GameExpiredTime);
        if (GameExpiredTime.Ticks > curPCTime.Ticks)
        {
            print("游戏未到期，开始游戏");
        }
        else
        {
            print("游戏已到期，退出游戏");
            Application.Quit();
        }
    }

    /// <summary>
    /// 检测是否把时间修改为早期时间，修改过就退出
    /// </summary>
    /// <param name="version"></param>
    void CheckLastGameTime(int version)
    {
        if (!PlayerPrefs.HasKey("aogalasttime" + projectName + version.ToString()))
        {
            PlayerPrefs.SetString("aogalasttime" + projectName + version.ToString(), DateTime.Now.Ticks.ToString());
            PlayerPrefs.Save();
        }
        else
        {
            long lPCST = long.Parse(PlayerPrefs.GetString("aogalasttime" + projectName + version.ToString()));
            DateTime lastPCSystemTime = new DateTime(lPCST);
            print("上一次启动游戏时PC的时间：" + lastPCSystemTime);

            if (DateTime.Now.Ticks <= lPCST)
            {
                PlayerPrefs.SetInt("aogastate" + projectName + version.ToString(), 1);
                PlayerPrefs.Save();
                Application.Quit();
                Debug.Log("修改时间了，退出游戏");
            }
            else
            {
                PlayerPrefs.SetString("aogalasttime" + projectName + version.ToString(), DateTime.Now.Ticks.ToString());
            }
        }
    }

}
