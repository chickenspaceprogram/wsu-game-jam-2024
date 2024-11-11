using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Health : MonoBehaviour
{
    // Start is called before the first frame update

    public int maxHealth = 5;
    public bool dead = false;
    public double currHealth;
    private HealthBar healthBar;


    void Awake()
    {
        currHealth = maxHealth;
        
        if(this.GetComponent<GameObject>() == GameObject.Find("Player").GetComponent<GameObject>())
        {
            healthBar = gameObject.GetComponent<HealthBar>();
        }
        else
        {
            healthBar = null;
        }

        //TakeDamage(1);
    }

    public void TakeDamage(double dmg)
    {
        currHealth -= dmg;
        //healthBar = gameObject.GetComponent<HealthBar>();

        if (healthBar != null)
        {
            healthBar.ReloadHealthBar();
        }

        if (currHealth <= 0)
        {
            dead = true;

            ParticleSystem ps = Instantiate<ParticleSystem>(GameObject.Find("DeathParticles").GetComponent<ParticleSystem>());
            //ParticleSystem ps = this.AddComponent<ParticleSystem>();
            ps.gameObject.GetComponent<Transform>().SetPositionAndRotation(gameObject.transform.position, gameObject.transform.rotation);

            ps.transform.SetParent(gameObject.transform);

            ps.Play();

            GameObject sp = Instantiate<GameObject>(GameObject.Find("Soul"));
            sp.gameObject.transform.position = gameObject.transform.position;

            //if(healthBar != null)
            //    Application.Quit();
            Destroy(this.gameObject, 3f);
        }
    }

    public void Heal(double health)
    {
        
        //healthBar = gameObject.GetComponent<HealthBar>();

        if (healthBar != null)
        {
            healthBar.ReloadHealthBar();
        }

        if (currHealth + health >= maxHealth)
        {
            currHealth = maxHealth;
            return;
        }

        currHealth += health;
    }



    // Update is called once per frame
    void Update()
    {
    }
}
