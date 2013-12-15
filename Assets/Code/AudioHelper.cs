using UnityEngine;
using System.Collections;

public class AudioHelper : MonoBehaviour {

    public static AudioHelper Instance;

    public AudioClip Crash1;
    public AudioClip Crash2;
    public AudioClip Crash3;

    public AudioClip Hydrant;
    public AudioClip Objective;
    public AudioClip Explosion;

    public AudioClip Screech1;
    public AudioClip Screech2;
    public AudioClip Screech3;

    private float coolDown = 0;
    private float screechCoolDown = 0;

    void Update()
    {
        if (coolDown != 0)
        {
            coolDown = Mathf.Max(coolDown - Time.deltaTime, 0);
        }
        if (screechCoolDown != 0)
        {
            screechCoolDown = Mathf.Max(screechCoolDown - Time.deltaTime, 0);
        }
    }

    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("2");
        }
        Instance = this;
    }

    private void MakeSound(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip, transform.position);
    }

    public void MakeCrashSound()
    {
        if (coolDown == 0)
        {
            System.Random r = new System.Random();
            int i = r.Next(3);

            if (i < 1)
            {
                MakeSound(Crash1);
            }
            if (i < 2)
            {
                MakeSound(Crash2);
            }
            if (i < 3)
            {
                MakeSound(Crash3);
            }
            coolDown = 0.5f;
        }
        
    }

    public void MakeScreechSound()
    {
        if (screechCoolDown == 0)
        {
            System.Random r = new System.Random();
            int i = r.Next(3);

            if (i < 1)
            {
                MakeSound(Screech1);
            }
            if (i < 2)
            {
                MakeSound(Screech2);
            }
            if (i < 3)
            {
                MakeSound(Screech3);
            }
            screechCoolDown = 0.75f;
        }

    }
    
    internal void MakeHydrantSound()
    {
        MakeSound(Hydrant);
    }

    public void MakeObjectiveSound()
    {
        MakeSound(Objective);
    }

    public void MakeExplosionSound()
    {
        MakeSound(Explosion);
    }
}
