using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    public EnemyAttack attack;
    public float maxTime, minTime, decreaseBySec;

    AudioSource audioSourceFox;
    AudioClip AudioFox;

    private bool performInvoke;

	// Use this for initialization
	void Start () {
        Invoke("Spawn", minTime);
        performInvoke = false;
        attack.stopped += Attack_stopped;

        AudioFox = Resources.Load("Audio/Zorro Rushing") as AudioClip;
        audioSourceFox = GetComponents<AudioSource>()[3];
        audioSourceFox.clip = AudioFox;
    }

    private void Attack_stopped(object sender, System.EventArgs e)
    {
        if (performInvoke)
        {
            attack.Run();
            Invoke("Spawn", Random.Range(minTime, maxTime));
            performInvoke = false;

            audioSourceFox.Stop();
        }
    }

    // Update is called once per frame
    void Update() {
        if (minTime > 0) minTime -= decreaseBySec * Time.deltaTime;
        if (maxTime > 1) maxTime -= decreaseBySec * Time.deltaTime;
    }

    void Spawn()
    {
        CancelInvoke();

        if (!attack.isRunning)
        {
            attack.Run();
            Invoke("Spawn", Random.Range(minTime, maxTime));

            if (!GameController.GameFinished)
                audioSourceFox.PlayOneShot(AudioFox);
        }
        else
        {
            performInvoke = true;
        }
    }
}
