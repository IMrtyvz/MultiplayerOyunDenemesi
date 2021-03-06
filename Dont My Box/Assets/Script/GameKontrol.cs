using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class GameKontrol : MonoBehaviour
{
    

    [Header("OYUNCU SAĞLIK AYARLARI")]
    public Image Oyuncu_1_saglik_Bar;
    float Oyuncu_1_saglik=100;
    public Image Oyuncu_2_saglik_Bar;
    float Oyuncu_2_saglik=100;
    PhotonView pw;

    private void Start()
    {
        pw = GetComponent<PhotonView>();
    }
    [PunRPC]
    public void Darbe_vur(int kriter,float darbegucu)
    {

        switch (kriter)
        {

            case 1:
                Oyuncu_1_saglik -= darbegucu;

                Oyuncu_1_saglik_Bar.fillAmount = Oyuncu_1_saglik / 100;

                if (Oyuncu_1_saglik <= 0)
                {

                    Debug.Log("Oyuncu 1 yenildi");

                }
               
                break;
            case 2:
                Oyuncu_2_saglik -= darbegucu;

                Oyuncu_2_saglik_Bar.fillAmount = Oyuncu_2_saglik / 100;

                if (Oyuncu_2_saglik <= 0)
                {

                    Debug.Log("Oyuncu 2 yenildi");

                }
                break;

        }

    }

    [PunRPC]
    public void SaglikDoldur(int hangioyuncu)
    {
        switch (hangioyuncu)
        {

            case 1:
                Oyuncu_1_saglik += 30;

                if (Oyuncu_1_saglik > 100)
                {
                    Oyuncu_1_saglik = 100;
                    Oyuncu_1_saglik_Bar.fillAmount = Oyuncu_1_saglik / 100;

                }
                else
                {
                    Oyuncu_1_saglik_Bar.fillAmount = Oyuncu_1_saglik / 100;
                }

               

                

                break;
            case 2:
                Oyuncu_2_saglik += 30;

                if (Oyuncu_2_saglik > 100)
                {
                    Oyuncu_2_saglik = 100;
                    Oyuncu_2_saglik_Bar.fillAmount = Oyuncu_2_saglik / 100;

                }
                else
                {
                    Oyuncu_2_saglik_Bar.fillAmount = Oyuncu_2_saglik / 100;
                }
                break;

        }

    }
}
