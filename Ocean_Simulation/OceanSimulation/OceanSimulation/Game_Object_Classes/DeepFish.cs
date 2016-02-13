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
    public class DeepFish : BaseObject
    {
        public DeepFish() { }
        public DeepFish(Texture2D Base_Object, Vector2 Position, Vector2 Speed, int LifeTime, int ReproTime)
        {
            CurrentVersion = 1;
            this.Base_Object = Base_Object;
            this.Position.X = Position.X;
            this.Position.Y = Position.Y;
            this.Speed.X = Speed.X;
            this.Speed.Y = Speed.Y;
            this.LifeTime = LifeTime;
            this.ReproTime = ReproTime;
            MinDepth = 600;
            MaxDepth = 700;
            Deep_Type = true;
        }
              public DeepFish(SerializationInfo sInfo, StreamingContext Context)
                  : base(sInfo, Context)
        {
        }    }
    }
