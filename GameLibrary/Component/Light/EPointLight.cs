using OpenTK;
using OpenTK.Graphics;
using System;
using System.Collections.Generic;

namespace GameLibrary.Component
{
    public class EPointLight : ELight
    {
        public static EPointLight[] GetAllPointLight
        {
            get
            {
                List<EPointLight> result = new List<EPointLight>();

                EActor[] all = EScene.Current.ListAllActorInScene;
                for (int i = 0; i < all.Length; i++)
                {
                    EPointLight buffer = all[i].GetComponent<EPointLight>();
                    if (buffer != null)
                    {
                        result.Add(buffer);
                    }
                }

                return result.ToArray();
            }
        }

        public Color4 color = Color4.White;
        public float linear = 0.09f;
        public float quadratic = 0.032f;

        public override void OnUpdate()
        {
            base.OnUpdate();
        }
    }
}
