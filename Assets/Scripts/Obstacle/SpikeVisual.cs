using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeVisual : MonoBehaviour
{
    private SFX sfx;
    private void Start()
    {
        sfx = FindFirstObjectByType<SFX>();
    }
    //play spike sfx
    public void PlaySpikeTrapSFX()
    {
        if (Player.instance.GetHealth() <= 0 || Player.instance.victoryOrDefeat == true)
        {
            //Player dead or victory
            sfx.StopPlay();
        }
        else
        {
            sfx.SpikeTrapSFX();
        }
    }
}
