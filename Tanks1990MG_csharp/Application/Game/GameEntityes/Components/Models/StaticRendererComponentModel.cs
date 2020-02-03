using EMCS.Interfaces.Entity;
using EMCS.Realisations.Components.Model;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tanks1990MG_csharp.Application.Interfaces;

namespace Tanks1990MG_csharp.Application.GameEntityes.Components
{
    //renderer model. Behavior

    //class BindibleSpritePosition : StandartModelBehavior
    //{
    //    public override void Update(GameTime time)
    //    {
    //        ((Model as StaticRendererComponentModel).Drawable as Sprite).Position = (Model as StaticRendererComponentModel).SharedPosition.Position;
    //        base.Update(time);
    //    }
    //}

    //class StaticRendererComponentModel : StandartModel
    //{
    //    public Application.Interfaces.IDrawable Drawable { get; set; }
    //    public PositionComponent SharedPosition { get; set; }
    //    public StaticRendererComponentModel()
    //    {
    //        Behavior = new BindibleSpritePosition();
    //    }
    //    public override void WayToAtivate(IEntity parrent)
    //    {
    //        SharedPosition = parrent.Components.GetComponent<PhisycModel>().GetComponent<PositionComponent>();
    //        if (SharedPosition != null)
    //            Activated = true;
    //        base.WayToAtivate(parrent);
    //    }
    //}
}
