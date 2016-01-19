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
          Rectangle DeepRect;
      
          public DeepFish() { }
          public DeepFish(Texture2D DeepFish, Vector2 position, Vector2 spd, int lifeTime, int repro)
        {
            Random r = new Random();
            baseObject = DeepFish;
            this.DeepRect = BaseRectangle;
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
          public override void Update()
          {
              base.Update();
          }
          public override void Move()
          {
              base.Move();
          }
          public override void TimeToDie()
          {
              base.TimeToDie();
          }
          public override void Draw(SpriteBatch sprt)
          {
              base.Draw(sprt);
          }
          public override void TimeToReproduction()
          {
              ReproTime--;
              Dreproduction = (ReproTime <= 0) ? true : false;
          }
    }
}
