using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace OceanSimulation
{
   [Serializable]
  public  class BaseObject
  {     [NonSerialized]
        public Texture2D baseObject;
        public  Rectangle BaseRectangle;
        public Vector2 Position, speed, origin;
        public int MinDepth, MaxDepth, ReproTime, LifeTime;
        public bool IsRotate, isDead,isFood,FoodSpawn, remove, reproduction, Preproduction, Dreproduction, meet,sc,pc,dc;
        Random r = new Random();
        public BaseObject() { }
        public virtual void Update()
        {
            Move();
            TimeToDie();
            TimeToReproduction();
          
        }
        public virtual void Move() 
        {
            BaseRectangle = new Rectangle(Convert.ToInt32(Position.X += speed.X), Convert.ToInt32(Position.Y += speed.Y), baseObject.Width, baseObject.Height);
            if (BaseRectangle.X >= 1190)
            {
                speed.X = -speed.X;
                IsRotate = true;
            }
            if (BaseRectangle.X <= 0)
            {
                speed.X = -speed.X;
                IsRotate = false;
            }
            if (BaseRectangle.Y <= MinDepth)
            {
                speed.Y = -speed.Y;
            }
            if (BaseRectangle.Y >= MaxDepth)
            {
                speed.Y = -speed.Y;
            }
        }
        public virtual void Draw(SpriteBatch sprt)
        {
            if (IsRotate == true)
            {
                sprt.Draw(baseObject, BaseRectangle, null, Color.White, 0, origin, SpriteEffects.FlipHorizontally, 0);
            }
            else if (isDead == true)
            {
                sprt.Draw(baseObject, BaseRectangle, null, Color.White, 0, origin, SpriteEffects.FlipVertically, 0);
            }
            else { sprt.Draw(baseObject, BaseRectangle, null, Color.White, 0, origin, SpriteEffects.None, 0); }
        }
       
        public virtual void TimeToDie()
        {
            LifeTime--;
            isDead = (LifeTime <= 0) ? true : false;
            if (isDead == true)
            {
                speed.X = 0;
                speed.Y = -2;
                if (BaseRectangle.Y <= 0)
                    remove = true;
            }
        }
        public virtual void TimeToReproduction()
        {
            ReproTime--;
            reproduction = (ReproTime <= 0) ? true : false;
        }
       
        
    
        }
    }

