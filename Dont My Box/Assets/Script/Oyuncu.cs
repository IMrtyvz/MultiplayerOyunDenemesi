using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Oyuncu : MonoBehaviour
{
    public GameObject top;
    public GameObject TopCikisnoktasi;
    public ParticleSystem TopAtisEfekt;
    public AudioSource TopAtmaSesi;
    float AtisYonu;
     

   [Header("GÜÇ BARI AYARLARI")]
    Image PowerBar;
    float powerSayi;    
    bool sonageldimi=false;
    Coroutine powerDongu;

    PhotonView pw;
    void Start()
    {      

        pw = GetComponent<PhotonView>();

        if (pw.IsMine)
        {
            PowerBar = GameObject.FindWithTag("PowerBar").GetComponent<Image>();
            if (PhotonNetwork.IsMasterClient)
            {
               // gameObject.tag = "Oyuncu_1";
                transform.position = GameObject.FindWithTag("OlusacakNokta_1").transform.position;
                transform.rotation = GameObject.FindWithTag("OlusacakNokta_1").transform.rotation;
                AtisYonu = 2f;                
            }
            else
            {
              //  gameObject.tag = "Oyuncu_2";
                transform.position = GameObject.FindWithTag("OlusacakNokta_2").transform.position;
                transform.rotation = GameObject.FindWithTag("OlusacakNokta_2").transform.rotation;
                AtisYonu = -2f;
                
            }

        }
        InvokeRepeating("Oyunbasladimi", 0, .5f);

    }
    public void Oyunbasladimi()
    {
        if (PhotonNetwork.PlayerList.Length == 2)
        {
            if (pw.IsMine)
            {
                powerDongu = StartCoroutine(PowerBarCalistir());
                CancelInvoke("Oyunbasladimi");

            }
           

        }else
        {
            StopAllCoroutines();
        }
    }
    IEnumerator PowerBarCalistir()
    {
        PowerBar.fillAmount = 0;
        sonageldimi = false;

        while (true)
        {
            if (PowerBar.fillAmount < 1 && !sonageldimi)
            {
                powerSayi = 0.01f;
                PowerBar.fillAmount += powerSayi;
                yield return new WaitForSeconds(0.001f * Time.deltaTime);

            }else
            {
                sonageldimi = true;
                powerSayi = 0.01f;
                PowerBar.fillAmount -= powerSayi;
                yield return new WaitForSeconds(0.001f * Time.deltaTime);

                if (PowerBar.fillAmount==0)
                {
                    sonageldimi = false;

                }

            }


        }

    }

    
    
    void Update()
    {
              

        if (pw.IsMine)
        {
            if (Input.GetKeyDown(KeyCode.Space)) // dokunma eklenecek
            {
               
                PhotonNetwork.Instantiate("Patlama_efekt", TopCikisnoktasi.transform.position, TopCikisnoktasi.transform.rotation, 0, null);
                TopAtmaSesi.Play();
                GameObject topobjem = PhotonNetwork.Instantiate("Top", TopCikisnoktasi.transform.position, TopCikisnoktasi.transform.rotation, 0, null);


                topobjem.GetComponent<PhotonView>().RPC("TagAktar",RpcTarget.All, gameObject.tag);

                Rigidbody2D rg = topobjem.GetComponent<Rigidbody2D>();
                rg.AddForce(new Vector2(AtisYonu, 0f) * PowerBar.fillAmount * 12f, ForceMode2D.Impulse);
                StopCoroutine(powerDongu);
                //  PowerBar.fillAmount  1 * 12 // 0.5 * 12 = 6


            }

        }

       

        
    }


    public void PowerOynasin()
    {
        powerDongu = StartCoroutine(PowerBarCalistir());
    }
}
