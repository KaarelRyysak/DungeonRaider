using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TeleportRune : Consumable {
    Color c;
    GameObject particleEffectPrefab;
    Vector3 destination;
    bool rotation; // true - right, false - left
    ParticleSystem effectParticleSystem;
    public float range = 2;

    public TeleHolo holoInstance;

    private TeleHolo hologram;


    private int charges;


    public Sprite damagedSprite;


    private void Awake()
    {
        charges = 2;

        Player player = GameObject.Find("Player").GetComponent<Player>();

        

    }

    


    private void Update()
    {
        base.Update();

        if (pickedUp)
        {
            if (hologram == null)
            {
                //Teeme hologrami
                hologram = GameObject.Instantiate(holoInstance, player.transform.position, player.transform.rotation, player.transform);
            }
            //See on hiire asukoht (ehk teleport sihtpunkt)
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //Vektor, mis läheb mängijast teleport sihtpunkti
            Vector3 playerToMouse = worldPoint - player.transform.position;

            //Ignoreerime z-koordinaadi muudatust
            playerToMouse = new Vector3(playerToMouse.x, playerToMouse.y, 0f);

            //Piirame vektori suurust, et range liiga suur ei oleks
            playerToMouse = Vector3.ClampMagnitude(playerToMouse, range);

            //Kasutame vektorit, et määrata mängijast sihtpunkt
            destination = player.transform.position + playerToMouse;

            //Liigutame hologrami
            hologram.transform.position = destination;
        }

        
    }

    public override void Use()
    {
        //player.GetComponent<Player>().enabled = false; // et ei saaks animatsiooni ajal liikuda
        //player.GetComponent<PolygonCollider2D>().enabled = false; // et mängija ei saaks anim. ajal viga
        //player.GetComponent<CapsuleCollider2D>().enabled = false;
        //player.GetComponent<Rigidbody2D>().simulated = false; // et mängija ei kukuks animatsiooni ajal läbi maa
        ////player.GetComponent<TimeController>().enabled = false; // et animatsioon ei uimerdaks mitu sekundit

        //See on hiire asukoht (ehk teleport sihtpunkt)
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //Vektor, mis läheb mängijast teleport sihtpunkti
        Vector3 playerToMouse = worldPoint - player.transform.position;

        //Ignoreerime z-koordinaadi muudatust
        playerToMouse = new Vector3(playerToMouse.x, playerToMouse.y, 0f);

        //Piirame vektori suurust, et range liiga suur ei oleks
        playerToMouse = Vector3.ClampMagnitude(playerToMouse, range);

        //Kasutame vektorit, et määrata mängijast sihtpunkt
        destination = player.transform.position + playerToMouse;

        //destination.z = 0f;
        //destination.y += 0.3f; // Muidu langeb veidi allapoole

        ////playerRb.velocity = new Vector2(0, 0);


        //Kui tohib teleportida, telepordime
        if (hologram.validSpot)
        {
            particleEffectPrefab = (GameObject)Resources.Load("Prefabs/Implode_01");
            GameObject effect = Instantiate(particleEffectPrefab, playerRb.position, Quaternion.identity);
            effectParticleSystem = effect.GetComponent<ParticleSystem>();
            effectParticleSystem.Play();
            //StartFadingOut();
            playerSprite.transform.position = destination;


            //Change sprite to be damaged
            storedImage.sprite = damagedSprite;

            //Use charge, destroy if empty
            charges -= 1;
            if (charges <= 0)
            {
                Destroy(hologram.gameObject);

                DestroyConsumable();
            }
        }

        
    }


    IEnumerator FadeOut()
    {
        for (float f = 1f; f > 0f; f -= 0.1f)
        {
            c = playerSprite.material.color;
            c.a = f;
            playerSprite.material.color = c;

            if (player.transform.localScale.x > 0) // Karakter pööratud paremale
            {
                rotation = true;
                player.transform.localScale -= new Vector3(0.1f * 4, 0.1f * 4, 0f);
            }
            else // Karakter pööratud vasakule
            {
                rotation = false;
                player.transform.localScale -= new Vector3(-0.1f * 4, 0.1f * 4, 0f);
            }

            yield return new WaitForSeconds(0.1f);
        }

        StartFadingIn();
    }



    IEnumerator FadeIn()
    {
        while (effectParticleSystem.isEmitting)
        {
            yield return new WaitForSeconds(0.1f);
        }

        if (rotation) player.transform.localScale = new Vector3(4f, 4f, 1f);
        else player.transform.localScale = new Vector3(-4f, 4f, 1f);

        player.GetComponent<Rigidbody2D>().simulated = true;
        playerSprite.transform.position = destination;
        playerRb.position = destination;
        player.GetComponent<Rigidbody2D>().simulated = false;

        for (float f = 0f; f < 1f; f += 0.2f)
        {
            c = playerSprite.material.color;
            c.a = f;
            playerSprite.material.color = c;
            yield return new WaitForSeconds(0.1f);
        }

        player.GetComponent<Rigidbody2D>().simulated = true;
        player.GetComponent<Player>().enabled = true;
        player.GetComponent<PolygonCollider2D>().enabled = true;
        player.GetComponent<CapsuleCollider2D>().enabled = true;
        //player.GetComponent<TimeController>().enabled = true;

        DestroyConsumable();
    }

    public void StartFadingOut()
    {
        StartCoroutine("FadeOut");
    }

    public void StartFadingIn()
    {
        StartCoroutine("FadeIn");
    }
}
