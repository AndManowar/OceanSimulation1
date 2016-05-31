using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace OceanSimulation
{
   public class PredatoryFish : BaseObject
   {
        private int Collision_Side;
        public PredatoryFish() { }
        public PredatoryFish(Texture2D Base_Object, Vector2 Position, Vector2 Speed, int LifeTime, int ReproTime)
        {
            CurrentVersion = 1;
            this.Base_Object = Base_Object;
            this.Position.X = Position.X;
            this.Position.Y = Position.Y;
            this.Speed.X = Speed.X;
            this.Speed.Y = Speed.Y;
            this.LifeTime = LifeTime;
            this.ReproTime = ReproTime;
            MinDepth = 0;
            MaxDepth = 500;
            Predatory_Type = true;         
        }
        void Intersect(List<BaseObject> Objects)
        {
            if (this.IsRotate == false) // Проверка на столкновение относительно положения рта рыбы                 
            {                          
                Collision_Side = this.BaseRectangle.Right; 
            }
            else
            {
                Collision_Side = this.BaseRectangle.Left;
            }
            for (int i = 0; i < Objects.Count; i++)
            {
                if (this.BaseRectangle.Intersects(Objects[i].BaseRectangle) && Objects[i].Can_Be_Eaten == true)
           {            
               if ((Collision_Side - Objects[i].BaseRectangle.Bottom <= 0) ||
                     (Collision_Side - Objects[i].BaseRectangle.Top <= 0) ||
                     (Collision_Side - Objects[i].BaseRectangle.Left <= 0) ||
                     (Collision_Side - Objects[i].BaseRectangle.Right <= 0))
                   {
                       Objects.Remove(Objects[i]); 
                   }
               }
           }          
            }
     
        public override void Update()
        {
           // Avoid_Collision(Game1.operations.Objects, 2);
           Intersect(Game1.operations.Objects);
            base.Update();
        }
    }
}
