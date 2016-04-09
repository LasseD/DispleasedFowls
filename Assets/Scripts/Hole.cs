using UnityEngine;
using System.Collections;

public class Hole : MonoBehaviour {
    public event System.Action OnFixed;

    public float initialHealth = 100f;
    public float altitudeLossPerSecond = 1.5f;

    private float health;

	void Start () {
        health = initialHealth;
    }

    void Update () {
        ApplyDamage();
        CheckHealth();
    }

    void ApplyDamage()
    {
        if (health <= 0)
            return;
        GameManager.instance.getAirship().ReduceAltitude(altitudeLossPerSecond * Time.deltaTime);
    }

    void CheckHealth()
    {
        if (health > 0)
            return;
        Instantiate(GameManager.instance.GetPatchToClone(), gameObject.transform.position, Quaternion.identity);
        GameObject.Destroy(gameObject);
    }
}
