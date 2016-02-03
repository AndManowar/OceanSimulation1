using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OceanSimulation
{
    
   public class PredatoryFish : BaseObject
    {
        public PredatoryFish() { }
        public PredatoryFish(Texture2D PredatoryFish, Vector2 position, Vector2 spd, int lifeTime, int repro)
        {
            baseObject = PredatoryFish;
            MinDepth = 0;
            MaxDepth = 500;
            Position.X = position.X;
            Position.Y = position.Y;
            speed.X = spd.X;
            speed.Y = spd.Y;
            LifeTime = lifeTime;
            ReproTime = repro;
            meet = true;
            pc = true;
        }   
    }
}
