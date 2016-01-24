using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OceanSimulation
{
     [Serializable]
    class DeepFish : BaseObject
    {
          public DeepFish() { }
          public DeepFish(Texture2D DeepFish, Vector2 position, Vector2 spd, int lifeTime, int repro)
        {
            baseObject = DeepFish;
            MinDepth = 600;
            MaxDepth = 780;
            Position.X = position.X;
            Position.Y = position.Y;
            speed.X = spd.X;
            speed.Y = spd.Y;
            LifeTime = lifeTime;
            ReproTime = repro;
            dc = true;
        }
        
    }
}
