using GameLibrary;
using OpenTK;
using System;
using System.ComponentModel;

namespace GameExample
{
    public class MyWindow : EWindow
    {
        public MyWindow(int width, int height) : base(width, height)
        {
        }

        static void Main(string[] args)
        {
            MyWindow app = new MyWindow(1280, 1080);
        }

        public override void ApplicationStart(EventArgs e)
        {
            
        }

        public override void ApplicationUpdate(FrameEventArgs e)
        {
            
        }
        
        public override void ApplicationRender(FrameEventArgs e)
        {
            
        }

        public override void ApplicationQuit(CancelEventArgs e)
        {
           
        }

    }
}
