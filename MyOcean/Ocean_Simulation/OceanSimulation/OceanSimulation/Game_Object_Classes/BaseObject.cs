using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Permissions;
using System.Text;
using System.Xml.Serialization;

namespace OceanSimulation
{
    public class BaseObject
    {
        protected int CurrentVersion;
        protected int Some_Property;
        public Texture2D Base_Object;
        public Rectangle BaseRectangle;
        public Vector2 Position, Speed;
        public int MinDepth, MaxDepth, ReproTime, LifeTime;
        protected bool IsRotate, isDead;
        public bool Simple_Type, Predatory_Type, Deep_Type, Can_Be_Eaten,Type;
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
        public virtual void Update()
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
                if (this.Simple_Type == true)
                {
                    BaseObject obj = new SimpleFish(this.Base_Object, new Vector2(this.Position.X + 50, this.Position.Y -30), new Vector2(this.Speed.X, this.Speed.Y), r.Next(1000, 2000), r.Next(1000, 2000));
                    this.ReproTime = r.Next(1000, 2000);
                    if (this.IsRotate == true)
                    { obj.IsRotate = true; }
                    else obj.IsRotate = false;
                    Game1.operations.Objects.Add(obj);
                }
                else  if (this.Predatory_Type == true)
                {
                    BaseObject obj = new PredatoryFish(this.Base_Object, new Vector2(this.Position.X + 50, this.Position.Y - 30), new Vector2(this.Speed.X, this.Speed.Y), r.Next(1000, 2000), r.Next(1000, 2000));
                    this.ReproTime = r.Next(2000, 3000);
                    if (this.IsRotate == true)
                    { obj.IsRotate = true; }
                    else obj.IsRotate = false;
                    Game1.operations.Objects.Add(obj);
                }
                else if (this.Deep_Type == true)
                {
                    BaseObject obj = new DeepFish(this.Base_Object, new Vector2(this.Position.X + 50, this.Position.Y - 30), new Vector2(this.Speed.X, this.Speed.Y), r.Next(1000, 2000), r.Next(1000, 2000));
                    this.ReproTime = r.Next(3000, 5000);
                    if (this.IsRotate == true)
                    { obj.IsRotate = true; }
                    else obj.IsRotate = false;
                    Game1.operations.Objects.Add(obj);
                }   
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
        public virtual void Save_To_Stream()
        {
            StreamWriter stream = new StreamWriter(new FileStream(Game1.FilePath, FileMode.Append));
            stream.WriteLine(CurrentVersion);
            stream.WriteLine(BaseRectangle.X);
            stream.WriteLine(BaseRectangle.Y);
            stream.WriteLine(BaseRectangle.Width);
            stream.WriteLine(BaseRectangle.Height);
            stream.WriteLine(Position.X);
            stream.WriteLine(Position.Y);
            stream.WriteLine(Speed.X);
            stream.WriteLine(Speed.Y);
            stream.WriteLine(MinDepth);
            stream.WriteLine(MaxDepth);
            stream.WriteLine(ReproTime);
            stream.WriteLine(LifeTime);
            stream.WriteLine(isDead);
            stream.WriteLine(IsRotate);
            stream.WriteLine(Simple_Type);
            stream.WriteLine(Predatory_Type);
            stream.WriteLine(Deep_Type);
            stream.WriteLine(Can_Be_Eaten);
            if (CurrentVersion == 2)
            {
                stream.WriteLine(Some_Property);
            }
            stream.Close();
        }
        public void Load_From_Stream(int ListCount)
        {
            StreamReader stream = new StreamReader(new FileStream(Game1.FilePath, FileMode.Open));
            for (int i = 0; i < ListCount; i++)
            {
                CurrentVersion = Convert.ToInt16(stream.ReadLine());
                BaseRectangle.X = Convert.ToInt16(stream.ReadLine());
                BaseRectangle.Y = Convert.ToInt16(stream.ReadLine());
                BaseRectangle.Width = Convert.ToInt16(stream.ReadLine());
                BaseRectangle.Height = Convert.ToInt16(stream.ReadLine());
                Position.X = Convert.ToInt16(stream.ReadLine());
                Position.Y = Convert.ToInt16(stream.ReadLine());
                Speed.X = Convert.ToInt16(stream.ReadLine());
                Speed.Y = Convert.ToInt16(stream.ReadLine());
                MinDepth = Convert.ToInt16(stream.ReadLine());
                MaxDepth = Convert.ToInt16(stream.ReadLine());
                ReproTime = Convert.ToInt16(stream.ReadLine());
                LifeTime = Convert.ToInt16(stream.ReadLine());
                isDead = Convert.ToBoolean(stream.ReadLine());
                IsRotate = Convert.ToBoolean(stream.ReadLine());
                Simple_Type = Convert.ToBoolean(stream.ReadLine());
                Predatory_Type = Convert.ToBoolean(stream.ReadLine());
                Deep_Type = Convert.ToBoolean(stream.ReadLine());
                Can_Be_Eaten = Convert.ToBoolean(stream.ReadLine());
                if (CurrentVersion == 2)
                {
                    Some_Property = Convert.ToInt16(stream.ReadLine());
                }
                if (Simple_Type == true) { Base_Object = Game1.Resourses.Get_Texture("Simple"); }
                else if (Predatory_Type == true) { Base_Object = Game1.Resourses.Get_Texture("Predatory"); }
                else if (Deep_Type == true) { Base_Object = Game1.Resourses.Get_Texture("Deep"); }
                Game1.operations.Objects.Add(new BaseObject(Base_Object, Position, Speed, LifeTime, ReproTime));
            }
            stream.Close();
        }
        protected virtual void Avoid_Collision(List<BaseObject> Objects,int Type_Case)
        {
            switch(Type_Case)
            {
                case 1: Type = Simple_Type;
                    break;
                case 2: Type = Predatory_Type;
                    break;
                case 3: Type = Deep_Type;
                    break;
            }
            for (int i = 0; i < Objects.Count; i++)
            {
                if (Objects[i] != this)
                {
                    if (new Rectangle((int)this.Position.X + 10, (int)this.Position.Y + 10, this.Base_Object.Width, this.Base_Object.Height).Intersects(Objects[i].BaseRectangle)&&(Objects[i].Type==true)  ||
                    (new Rectangle((int)this.Position.X - 10, (int)this.Position.Y - 10, this.Base_Object.Width, this.Base_Object.Height).Intersects(Objects[i].BaseRectangle) && (Objects[i].Type == true)))
                    {
                        if (this.IsRotate == true) { this.IsRotate = false; }
                        else { this.IsRotate = true; }
                        this.Speed.X = -Speed.X;
                        this.Speed.Y = -Speed.Y;
                    }
                }
            }
        }
      
    }
}
