using System;
using GXPEngine;

public class SoundManager
{
    public static SoundManager PlayerShoot = new SoundManager("sound/PlayerShoot.wav", 0, 0);
    public static SoundManager PlayerTakeDamage = new SoundManager("sound/PlayerTakingDamage.wav", 0, 0);
    public static SoundManager BackGroundMusic = new SoundManager("sound/BackgroundMusic.wav", 0, 0);
    public static SoundManager EnemyTakingDamage = new SoundManager("sound/EnemyTakingDamage.wav", 0, 0);
    public static SoundManager EnemyDeath = new SoundManager("sound/EnemyDeath.wav", 0, 0);

    private Sound storedSound;
    private float defaultVolume;
    private uint defaultChannel;

    public SoundManager(String fileName, float defaultVolume, float defaultChannel)
    {
        storedSound = new Sound(fileName);
        this.defaultVolume = defaultVolume;
        this.defaultChannel = this.defaultChannel;
    }

    public void play(float volume, uint chanel)
    {
        storedSound.Play(false, chanel, volume, 0);
    }
}
