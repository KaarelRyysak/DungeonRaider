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

    public override void Use()
    {
        player.GetComponent<CharacterController2>().enabled = false; // et ei saaks animatsiooni ajal liikuda
        player.GetComponent<PolygonCollider2D>().enabled = false; // et mängija ei saaks anim. ajal viga
        player.GetComponent<CapsuleCollider2D>().enabled = false;
        player.GetComponent<Rigidbody2D>().simulated = false; // et mängija ei kukuks animatsiooni ajal läbi maa
        //player.GetComponent<TimeController>().enabled = false; // et animatsioon ei uimerdaks mitu sekundit

        destination = mousePointer.transform.position;
        destination.z = 0f;
        destination.y += 0.3f; // Muidu langeb veidi allapoole

        playerRb.velocity = new Vector2(0, 0);
        particleEffectPrefab = (GameObject) Resources.Load("Prefabs/Implode_01");
        GameObject effect = Instantiate(particleEffectPrefab, playerRb.position, Quaternion.identity);
        effectParticleSystem = effect.GetComponent<ParticleSystem>();
        effectParticleSystem.Play();
        StartFadingOut();
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
        player.GetComponent<CharacterController2>().enabled = true;
        player.GetComponent<PolygonCollider2D>().enabled = true;
        player.GetComponent<CapsuleCollider2D>().enabled = true;
        //player.GetComponent<TimeController>().enabled = true;
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
