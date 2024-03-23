using System;
using GXPEngine;

public class SoundHandler
{
    //public static SoundHandler test = new SoundHandler(" ", 1, 0);

    private Sound storedSound;
    private float defaultVolume;
    private uint defaultChannel;

    public SoundHandler(String fileName, float defaultVolume, float defaultChannel)
    {
        storedSound = new Sound(fileName);
        this.defaultVolume = defaultVolume;
        this.defaultChannel = this.defaultChannel;
    }

    public void play(float volume, uint chanel)
    {
        //storedSound.Play(false, chanel, volume, 0);
    }
}
