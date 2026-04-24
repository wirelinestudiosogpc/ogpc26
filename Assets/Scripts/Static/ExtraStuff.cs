using System.Collections;
using UnityEngine;

public static class AudioStuff
{
    //main
    public static AudioClip sfxLevelEnd = Resources.Load<AudioClip>("sfx/levelEnd");


    //music
    public static AudioClip musicVillage = Resources.Load<AudioClip>("sfx/music/village");
    public static AudioClip musicForrest = Resources.Load<AudioClip>("sfx/music/village");
    public static AudioClip musicCave = Resources.Load<AudioClip>("sfx/music/cave");
    public static AudioClip musicBoss = Resources.Load<AudioClip>("sfx/music/boss");
    public static AudioClip[] music = new[] {musicVillage, musicForrest, musicCave, musicBoss};


    //player
    public static AudioClip sfxJump = Resources.Load<AudioClip>("sfx/player/jump");
    public static AudioClip sfxSlash = Resources.Load<AudioClip>("sfx/player/slash");
    public static AudioClip sfxDash = Resources.Load<AudioClip>("sfx/player/dash");
    public static AudioClip sfxPlayerDie = Resources.Load<AudioClip>("sfx/player/die");
    public static AudioClip sfxPlayerHurt = Resources.Load<AudioClip>("sfx/player/hurt");


    //npc
    public static AudioClip sfxMurmur = Resources.Load<AudioClip>("sfx/npc/murmur");


    //enemy
    public static AudioClip sfxEnemyDie = Resources.Load<AudioClip>("sfx/enemy/die");
    public static AudioClip sfxEnemyHurt = Resources.Load<AudioClip>("sfx/enemy/hurt");


    //boss
    public static AudioClip sfxBirdSplit = Resources.Load<AudioClip>("sfx/boss/birdSplit");
    public static AudioClip sfxBoss1 = Resources.Load<AudioClip>("sfx/boss/1");
    public static AudioClip sfxBoss2 = Resources.Load<AudioClip>("sfx/boss/2");
    public static AudioClip sfxBirdThrow = Resources.Load<AudioClip>("sfx/boss/birdThrow");
    public static AudioClip sfxGroundpound = Resources.Load<AudioClip>("sfx/boss/groundpound");
    public static AudioClip sfxBossHurt = Resources.Load<AudioClip>("sfx/boss/hurt");
    public static AudioClip sfxBossKilled = Resources.Load<AudioClip>("sfx/boss/killed");
    public static AudioClip sfxBossParry = Resources.Load<AudioClip>("sfx/boss/parry");
    public static AudioClip sfxBossSlash = Resources.Load<AudioClip>("sfx/boss/slash");

    public static void PlaySFX(AudioClip clip, float volume, Transform parent)
    {
        GameObject obj = new();
        obj.transform.position = parent.position;
        obj.transform.SetParent(parent);
        obj.name = $"audio_{clip.name}";
        AudioSource source = obj.AddComponent<AudioSource>();
        source.clip = clip;
        source.volume = volume/100;
        source.Play();
        Object.Destroy(obj, clip.length);
    }
    public static void PlaySFX(AudioClip clip, float volume, Transform parent, bool loop)
    {
        GameObject obj = new();
        obj.transform.position = parent.position;
        obj.transform.SetParent(parent);
        AudioSource source = obj.AddComponent<AudioSource>();
        source.clip = clip;
        source.volume = volume/100;
        source.Play();
        if (loop)
        {
            obj.name = $"audio_{clip.name}_loop";
            source.loop = true;
        }
        else
        {
            obj.name = $"audio_{clip.name}";
            Object.Destroy(obj, clip.length);
        }
    }
    public static void PlaySFX(AudioClip clip, float volume, Transform parent, float loopTime)
    {
        GameObject obj = new();
        obj.transform.position = parent.position;
        obj.transform.SetParent(parent);
        obj.name = $"audio_{clip.name}_loop_{loopTime}_seconds";
        AudioSource source = obj.AddComponent<AudioSource>();
        source.clip = clip;
        source.volume = volume / 100;
        source.loop = true;
        source.Play();
        Object.Destroy(obj, loopTime);
    }
    public static void PlaySFX(AudioClip clip, float volume, Transform parent, int loopCount)
    {
        GameObject obj = new();
        obj.transform.position = parent.position;
        obj.transform.SetParent(parent);
        obj.name = $"audio_{clip.name}";
        AudioSource source = obj.AddComponent<AudioSource>();
        source.clip = clip;
        source.loop = true;
        source.volume = volume / 100;
        source.Play();
        Object.Destroy(obj, clip.length * loopCount);
    }
}
