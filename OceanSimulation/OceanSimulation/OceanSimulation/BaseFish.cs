using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace OceanSimulation
{
    [Serializable]
    public class BaseObject
    {
        [NonSerialized]
        public Texture2D baseObject;
        public Rectangle BaseRectangle;
        protected Vector2 Position, speed, origin;
        protected int MinDepth, MaxDepth, ReproTime, LifeTime;
        protected bool IsRotate, isDead;
        public bool meet, sc, pc, dc;
        protected Random r = new Random();
        public BaseObject() { }
        public BaseObject(Texture2D baseO, Vector2 position, Vector2 spd, int lifeTime, int repro)
        {

            baseObject = baseO;
            MinDepth = 0;
            MaxDepth = 200;
            Position.X = position.X;
            Position.Y = position.Y;
            speed.X = spd.X;
            speed.Y = spd.Y;
            LifeTime = lifeTime;
            ReproTime = repro;
        }   
        public virtual  void Update()
        {
            Move();
            TimeToDie();
            Repro();       
        }
        protected virtual void Move()
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
        protected virtual void TimeToDie()
        {
            LifeTime--;
            if (LifeTime <= 0)
            {
                this.speed.X = 0; this.speed.Y = -2; this.isDead = true;
            }
            if ((this.Position.Y <= 0) && (this.isDead == true)) { Game1.operations.Objects.Remove(this); }
        }
        protected virtual void Repro()
        {
            this.ReproTime--;
            if (ReproTime <= 0)
            {
                BaseObject obj = new BaseObject(this.baseObject, new Vector2(this.Position.X + 30, this.Position.Y - 20), new Vector2(1, 1), r.Next(1000, 2000), r.Next(1000, 2000));
                if (this.IsRotate == true)
                { obj.IsRotate = true; }
                else obj.IsRotate = false;
                Game1.operations.Objects.Add(obj);
                this.ReproTime = r.Next(1000, 2000);
            }
        }
        public void Draw(SpriteBatch sprt)
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
    }

}