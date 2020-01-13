using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tanks1990MG_csharp.Application.Interfaces;

namespace Tanks1990MG_csharp.Application.Logic.Graphic
{
    class ObjectRendererSprite : IRendererModel
    {
        /// <summary>
        /// Источник для рисованиея Source : Drawable
        /// </summary>
        public Interfaces.IDrawable Source { get; set; }

        public event Action<object, IRendererModel> IRendererModelChanged;

        /// <summary>
        /// Рисует ядро обьекта, просит источник Source.Draw(target,states);
        /// </summary>
        /// <param name="target">Окно</param>
        /// <param name="states">Состояния</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            Source.Draw(spriteBatch);
            //Source.Draw(target, states);
        }

        /// <summary>
        /// Обновить если надо источник
        /// </summary>
        /// <param name="time">Время кадра</param>
        public void Update(GameTime time)
        {
            //обнови если это обновляемое
            (Source as IUpdatebleTime)?.Update(time);
        }

    }
}
