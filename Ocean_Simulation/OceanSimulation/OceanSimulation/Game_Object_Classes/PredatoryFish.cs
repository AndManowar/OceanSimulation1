using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace OceanSimulation
{
    [Serializable]
   public class PredatoryFish : BaseObject
   {
      
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
        public PredatoryFish(SerializationInfo sInfo, StreamingContext Context)
            : base(sInfo, Context)
        {

        }
    
        void Intersect(List<BaseObject> Objects)
        {
            for (int i = 0; i < Objects.Count; i++)
            {
                if (this.BaseRectangle.Intersects(Objects[i].BaseRectangle) && Objects[i].Can_Be_Eaten == true)
                { Objects.Remove(Objects[i]); }
            }
        }
        public override void Update()
   {
      Intersect(Game1.operations.Objects);
       base.Update();
   }
    }
}
