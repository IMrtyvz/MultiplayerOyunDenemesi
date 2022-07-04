using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class anamenuKontrol : MonoBehaviour
{
    public GameObject ilkpanel;
    public GameObject ikincipanel;
    public InputField kullaniciadi;
    public Text Varolankullaniciadi;
    void Start()
    {

        if (!PlayerPrefs.HasKey("Kullanıcıadi"))
        {
            ilkpanel.SetActive(true);

        }else
        {
            ikincipanel.SetActive(true);
            Varolankullaniciadi.text = PlayerPrefs.GetString("Kullanıcıadi");
        }
        
    }

    public void KullaniciAdiKaydet()
    {
       
        PlayerPrefs.SetString("Kullanıcıadi", kullaniciadi.text);

        ilkpanel.SetActive(false);
        ikincipanel.SetActive(true);
        Varolankullaniciadi.text = kullaniciadi.text;



    }
}
