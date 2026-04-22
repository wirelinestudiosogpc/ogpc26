using UnityEngine;

public static class AudioStuff
{
    public static AudioClip sfxJump = Resources.Load<AudioClip>("sfx/jump");
    public static AudioClip sfxSlash = Resources.Load<AudioClip>("sfx/slash");
    public static AudioClip sfxDash = Resources.Load<AudioClip>("sfx/dash");
    public static AudioClip sfxPlayerDie = Resources.Load<AudioClip>("sfx/loss");
    public static AudioClip sfxPlayerHurt = Resources.Load<AudioClip>("sfx/player_hurt");
    public static AudioClip sfxEnemyHurt = Resources.Load<AudioClip>("sfx/enemy_hurt");
    public static AudioClip sfxEnemyDie = Resources.Load<AudioClip>("sfx/enemy_hurt");
    public static AudioClip sfxLevelEnd = Resources.Load<AudioClip>("sfx/jump");

    public static void PlaySFX(AudioClip clip, Transform objParent)
    {
        GameObject obj = new();
        obj.transform.position = objParent.position;
        obj.transform.SetParent(objParent);
        obj.name = $"SFX_{clip.name}";
        AudioSource source = obj.AddComponent<AudioSource>();
        source.clip = clip;
        source.Play();
        UnityEngine.Object.Destroy(obj, clip.length);
    }
}
