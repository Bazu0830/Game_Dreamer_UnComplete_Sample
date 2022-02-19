using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMap : MonoBehaviour
{
    public int mapsCount = 3;//3,10,20
    [SerializeField] List<string> Mapsname;
    public string mapsstr;
    public string mapsdata;
    [SerializeField] GameObject mapsobj;


    private void Start()
    {
        Create();
    }
    public void Create()
    {
        for (int i = 0; i < Mapsname.Count; i++)
        {
            mapsstr = Mapsname[i];

            string str = StringUtils.GeneratePassword(mapsCount - 1);

            //string str2 = mapstring.Replace("j", "0");
            //int where1 = Random.Range(0, mapsCount - 1);
            //string str1 = str.Insert(where1, "i");
            string str1 = str.Insert(mapsCount-1, "Z");

            for (int j = 0; j < mapsCount; j++)
            {

                mapsobj.transform.GetChild(j + (i * mapsCount)).GetComponent<SpownMap>().MapInfo = mapsstr + " (" + str1.Substring(j, 1) + ")";

            }

            mapsdata += str1;
        }
    }

    public void Insert(int mapscounttmp, string mapsdatatmp)
    {
        for (int i = 0; i < Mapsname.Count; i++)
        {
            mapsstr = Mapsname[i];

            for (int j = 0; j < mapscounttmp; j++)
            {

                mapsobj.transform.GetChild(j + (i * mapsCount)).GetComponent<SpownMap>().MapInfo = mapsstr + " (" + mapsdatatmp.Substring(j * (i * mapscounttmp), 1) + ")";

            }
        }
    }

    public static class StringUtils
    {
        private const string PASSWORD_CHARS =
            "012";

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
}
