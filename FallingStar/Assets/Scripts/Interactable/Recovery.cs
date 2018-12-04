using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recovery : Interactable {

    public Particle[] particles;
    Animator anim;

    // Use this for initialization
    void Start()
    {
        anim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void effect(GameObject player)
    {
        player.GetComponent<Star>().Shine = 1000;
        transform.DetachChildren();
        anim.SetTrigger("die");
        foreach (Particle p in particles)
            p.setTarget(player.transform);

        Invoke("DestroyZone", 0.3f);
    }

    void DestroyZone()
    {
        Destroy(this.gameObject);
    }
}
