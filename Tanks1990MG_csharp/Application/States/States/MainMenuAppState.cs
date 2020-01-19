using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tanks1990MG_csharp.Application.InputMG.Interfaces;
using Tanks1990MG_csharp.Application.InputMG.Solutions;
using Tanks1990MG_csharp.Application.States.Interfaces;

namespace Tanks1990MG_csharp.Application.States.Solution
{
    

    class MainMenuAppState : IAppState
    {
        public bool DontUnloadFromMemory { get; set; }
        public bool Initialized { get; set; } = false;
        public List<IBindebleKey> StateKeyboardLayout { get; set; } = new List<IBindebleKey>();

        public event Action<StateBuilder.StateID> ChangeStateRequest;
        public MainMenuAppState()
        {
            DontUnloadFromMemory = true;

            //Myra.Graphics2D.UI.Desktop.Render();

            //var vl = new VerticalLayout();

            //vl.SetPosition(new Layout2d("&.width-width + width/20", "&.height+40% - height"));
            //vl.SetSize(new Layout2d("&.width-width", " &.h"));

            //GUI.Add(vl);

            //GUI.Add(new Button() { Position = new Vector2f(100, 200), Text = "PLAY", TextSize = 40 }) ;
            //GUI.Add(new Button() { Position = new Vector2f(100, 240), Text = "Settings", TextSize = 40 });
            //GUI.Add(new Button() { Position = new Vector2f(100, 280), Text = "Reserved", TextSize = 40 });
            //GUI.Add(new Button() { Position = new Vector2f(100, 320), Text = "Reserved", TextSize = 40 });
            //GUI.Add(new Button() { Position = new Vector2f(100, 360), Text = "Reserved", TextSize = 40 });

            //var t = new Button() { Position = new Vector2f(100, 700), Text = "Exit", TextSize = 40 };
            ////t.SetPosition(new Layout2d("&.width / 6", "&.height - height-10%"));
            //t.Clicked += (object sender, SignalArgsVector2f arg) => { KeyInterpretator.GetInstance().GetAction("RenderWindow.Close()")(); };
            //vl.Add(t);


        }


        public void Save()
        {
            //stop music
        }
        public void Load()
        {
            //resume music
        }

        public void Update(GameTime time)
        {

        }

        public void Draw(GraphicsDevice device)
        {

        }
    }
}
