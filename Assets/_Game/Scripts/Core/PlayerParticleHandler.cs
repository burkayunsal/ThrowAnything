using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticleHandler : Singleton<PlayerParticleHandler>
{
   public ParticleSystem playerUpgradeParticles;
   
   public void PlayerUpgraded()
   {
      playerUpgradeParticles.Play();
   }
   
}
