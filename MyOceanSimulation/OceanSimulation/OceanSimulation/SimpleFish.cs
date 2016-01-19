using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OceanSimulation
{[Serializable]
    class SimpleFish : BaseObject
    {
   
        public SimpleFish() { }
        public SimpleFish(Texture2D SimpleFish,Vector2 position,Vector2 spd,int lifeTime,int repro)
        {
            Random r = new Random();
            baseObject = SimpleFish;
            MinDepth = 0;
            MaxDepth = 200;
            Position.X = position.X;
            Position.Y = position.Y;
            speed.X = spd.X;
            speed.Y = spd.Y;
            LifeTime = lifeTime;
            ReproTime =repro;
            meet = false;
            sc = true;
        }
        public override void Update()
        {
            base.Update();
        }
        public override void Move()
        { base.Move(); }
        public override void Draw(SpriteBatch sprt)
        {
            base.Draw(sprt);
        }
        public override void TimeToDie()
        {
            base.TimeToDie();
        }
        public override void TimeToReproduction()
        {
            base.TimeToReproduction();
        }
    }
}
