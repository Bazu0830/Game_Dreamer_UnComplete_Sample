using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkySystem : MonoBehaviour
{

    //角度を入れる変数
    [SerializeField]
    private float sunPos;
    [SerializeField]
    private int sunspeed;
    //Skyboxを入れる変数、[]があると複数入れられるように
    public Material[] skybox;


    void Update()
    {
        //X軸を回転させる
        transform.eulerAngles = new Vector3(sunPos, 0, 0);

        //1日のスピードを調節する
        sunPos += Time.deltaTime * sunspeed;

        //0度以上72度未満の時に一つ目のSkyboxを出す
        if (sunPos >= 0 && 24 > sunPos)
            RenderSettings.skybox = skybox[0];

        //elseがないと切り替わらない
        else if (sunPos >= 24 && 48 > sunPos)
            RenderSettings.skybox = skybox[1];

        else if (sunPos >= 48 && 72 > sunPos)
            RenderSettings.skybox = skybox[2];

        else if (sunPos >= 72 && 96 > sunPos)
            RenderSettings.skybox = skybox[3];

        else if (sunPos >= 96 && 120 > sunPos)
            RenderSettings.skybox = skybox[4];
        else if (sunPos >= 120 && 144 > sunPos)
            RenderSettings.skybox = skybox[5];

        else if (sunPos >= 144 && 168 > sunPos)
            RenderSettings.skybox = skybox[6];

        else if (sunPos >= 168 && 192 > sunPos)
            RenderSettings.skybox = skybox[7];

        else if (sunPos >= 192 && 216 > sunPos)
            RenderSettings.skybox = skybox[8];
        else if (sunPos >= 216 && 240 > sunPos)
            RenderSettings.skybox = skybox[9];

        else if (sunPos >= 240 && 264 > sunPos)
            RenderSettings.skybox = skybox[10];

        else if (sunPos >= 264 && 288 > sunPos)
            RenderSettings.skybox = skybox[11];

        else if (sunPos >= 288 && 312 > sunPos)
            RenderSettings.skybox = skybox[12];
        else if (sunPos >= 310 && 336 > sunPos)
            RenderSettings.skybox = skybox[13];

        else if (sunPos >= 334 && 360 > sunPos)
            RenderSettings.skybox = skybox[14];

        //360度以上になったら0に戻す
        if (sunPos > 360)
            sunPos = 0;
    }
}