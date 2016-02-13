using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;
using System.Xml.Serialization;

namespace OceanSimulation
{ [Serializable]
    public class BaseObject:ISerializable
    {   
        public  int CurrentVersion;
        //private int Some_Property;     
        public Texture2D Base_Object;   
        public Rectangle BaseRectangle;
        public Vector2 Position, Speed;
        public int MinDepth, MaxDepth, ReproTime, LifeTime;
        protected bool IsRotate, isDead;
        public bool Simple_Type,Predatory_Type,Deep_Type,Can_Be_Eaten;//переменная ,отвечающая за "возможность быть съеденной".
        protected Random r = new Random();
        public BaseObject() { }
        public BaseObject(Texture2D Base_Object, Vector2 Position, Vector2 Speed, int LifeTime, int ReproTime)
        {
            this.Base_Object = Base_Object;
            this.Position.X = Position.X;
            this.Position.Y = Position.Y;
            this.Speed.X = Speed.X;
            this.Speed.Y = Speed.Y;
            this.LifeTime = LifeTime;
            this.ReproTime = ReproTime;
        }
        public BaseObject(SerializationInfo sInfo, StreamingContext Context)
        {
            CurrentVersion = Convert.ToInt32(sInfo.GetInt32("CurrentVersion"));
            BaseRectangle = (Rectangle)sInfo.GetValue("Rectangle", typeof(Rectangle));
            Position = (Vector2)sInfo.GetValue("Position", typeof(Vector2));
            Speed = (Vector2)sInfo.GetValue("Speed", typeof(Vector2));
            MinDepth = sInfo.GetInt32("MinDepth");
            MaxDepth = sInfo.GetInt32("MaxDepth");
            ReproTime = sInfo.GetInt32("ReproTime");
            LifeTime = sInfo.GetInt32("LifeTime");
            IsRotate = sInfo.GetBoolean("IsRotate");
            isDead = sInfo.GetBoolean("isDead");
            Simple_Type = sInfo.GetBoolean("Simple_Type");
            Predatory_Type = sInfo.GetBoolean("Predatory_Type");
            Deep_Type = sInfo.GetBoolean("Deep_Type");
            Can_Be_Eaten = sInfo.GetBoolean("Can_Be_Eaten");
        }//Реализация ISerializable со всеми необходимыми полями сериализации.
        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public virtual void GetObjectData(SerializationInfo sInfo, StreamingContext context)
        {
            sInfo.AddValue("CurrentVersion", CurrentVersion);
            sInfo.AddValue("Rectangle", BaseRectangle);
            sInfo.AddValue("Position", Position);
            sInfo.AddValue("Speed", Speed);
            sInfo.AddValue("MinDepth", MinDepth);
            sInfo.AddValue("MaxDepth", MaxDepth);
            sInfo.AddValue("ReproTime", ReproTime);
            sInfo.AddValue("LifeTime", LifeTime);
            sInfo.AddValue("IsRotate", IsRotate);
            sInfo.AddValue("isDead", isDead);
            sInfo.AddValue("Simple_Type", Simple_Type);
            sInfo.AddValue("Predatory_Type", Predatory_Type);
            sInfo.AddValue("Deep_Type", Deep_Type);
            sInfo.AddValue("Can_Be_Eaten", Can_Be_Eaten);
        } 
        public virtual  void Update()
        {
            Movement();
            Time_To_Die();
            Reproduction();       
        }
        protected virtual void Movement()
        {
            BaseRectangle = new Rectangle(Convert.ToInt32(Position.X += Speed.X), Convert.ToInt32(Position.Y += Speed.Y), Base_Object.Width, Base_Object.Height);
            if (BaseRectangle.X >= 1190)
            {
                Speed.X = -Speed.X;
                IsRotate = true;
            }
            if (BaseRectangle.X <= 0)
            {
                Speed.X = -Speed.X;
                IsRotate = false;
            }
            if (BaseRectangle.Y <= MinDepth)
            {
                Speed.Y = -Speed.Y;
            }
            if (BaseRectangle.Y >= MaxDepth)
            {
                Speed.Y = -Speed.Y;
            }
        }
        protected virtual void Time_To_Die()
        {
            LifeTime--;
            if (LifeTime <= 0)
            {
                this.Speed.X = 0; this.Speed.Y = -2; this.isDead = true;
            }
            if ((this.Position.Y <= 0) && (this.isDead == true)) { Game1.operations.Objects.Remove(this); }
        }
        protected virtual void Reproduction()
        {
            this.ReproTime--;
            if (ReproTime <= 0)
            {
                BaseObject obj = new BaseObject(this.Base_Object, new Vector2(this.Position.X + 30, this.Position.Y - 20), new Vector2(1, 1), r.Next(1000, 2000), r.Next(1000, 2000));
                if (this.IsRotate == true)
                { obj.IsRotate = true; }
                else obj.IsRotate = false;
                Game1.operations.Objects.Add(obj);
                this.ReproTime = r.Next(1000, 2000);
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (IsRotate == true)
            {
                spriteBatch.Draw(Base_Object, BaseRectangle, null, Color.White, 0, new Vector2(0, 0), SpriteEffects.FlipHorizontally, 0);
            }
            else if (isDead == true)
            {
                spriteBatch.Draw(Base_Object, BaseRectangle, null, Color.White, 0, new Vector2(0, 0), SpriteEffects.FlipVertically, 0);
            }
            else { spriteBatch.Draw(Base_Object, BaseRectangle, null, Color.White, 0, new Vector2(0, 0), SpriteEffects.None, 0); }
        }
        public void Version_Control(int CurrentVersion)//Метод контроля версий объекта.
        {
            if (CurrentVersion == 2)
            { 
               // Some_Property = 20;
            }
            else if (CurrentVersion == 3) { }
        }
    }
}