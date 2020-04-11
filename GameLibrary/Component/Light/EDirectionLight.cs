using OpenTK;
using OpenTK.Graphics;
using System;
using System.Collections.Generic;

namespace GameLibrary.Component
{
    public class EDirectionLight : ELight
    {
        public static EDirectionLight[] GetAllDirectionLight
        {
            get
            {
                List<EDirectionLight> result = new List<EDirectionLight>();

                EActor[] all = EScene.Current.ListAllActorInScene;
                for (int i = 0; i < all.Length; i++)
                {
                    EDirectionLight buffer = all[i].GetComponent<EDirectionLight>();
                    if (buffer != null)
                    {
                        result.Add(buffer);
                    }
                }

                return result.ToArray();
            }
        }

        public Color4 color = Color4.White;

        public override void OnUpdate()
        {
            base.OnUpdate();
        }
    }
}
