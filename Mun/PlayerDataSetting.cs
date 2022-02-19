
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerDataSetting : MonoBehaviour
{
    [SerializeField]
    PlayerData playerdata;
    [SerializeField]
    private InputField inputField;
    private string ID;
    [SerializeField]
    GameObject Playpanel;
    [SerializeField]
    GameObject Inputpanel;

    // Start is called before the first frame update
    void Start()
    {
        if (playerdata.ID != ""
          && playerdata.Name != "")
        {
            PlayPanel();
        }
        else
        {
            InputPanel();
        }
    }
    public void InputPanel()
    {
        Inputpanel.SetActive(true);
        Playpanel.SetActive(false);
    }
    public void PlayPanel()
    {
        Inputpanel.SetActive(false);
        Playpanel.SetActive(true);
    }
    //ランダムでIDを作成する
    public static class StringUtils
    {
        private const string PASSWORD_CHARS =
            "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public static string GeneratePassword(int length)
        {
            var sb = new System.Text.StringBuilder(length);
            var r = new System.Random();

            for (int i = 0; i < length; i++)
            {
                int pos = r.Next(PASSWORD_CHARS.Length);
                char c = PASSWORD_CHARS[pos];
                sb.Append(c);
            }

            return sb.ToString();
        }
    }

    public void SetPlayerName()
    {
        playerdata.Name=inputField.text;
        ID = StringUtils.GeneratePassword(20);
        playerdata.ID=ID;
    }

    public void LoadScenezero()
    {
        if (playerdata.ID != "")
        {
            SceneManager.LoadScene("zero");
        }
        else
        {
            Debug.Log("名前を入力してください");
        }
    }
}