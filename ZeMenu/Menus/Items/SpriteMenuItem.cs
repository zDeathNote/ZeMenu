using System.IO;
using System.Drawing;

namespace ZeMenu.Menus.Items
{
    class SpriteMenuItem : MenuItem
    {
        private string SpritePath { get; }
        public GTA.Texture GetTexture { get; }
        public SpriteMenuItem(string DisplayText, GTA.ColorIndex color, params Commands.Command[] commands) : base(DisplayText, commands)
        {
            using (Bitmap b = new Bitmap(50, 50))
            {
                using (Graphics g = Graphics.FromImage(b))
                {
                    g.Clear(color.ToColor());
                }
                using (MemoryStream stream = new MemoryStream())
                {
                    b.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                    GetTexture = new GTA.Texture(stream.ToArray());
                }
                    
            }
        }
        public SpriteMenuItem(string DisplayText, string SpritePath, params Commands.Command[] commands) : base(DisplayText, commands)
        {
            this.SpritePath = SpritePath;
            if (File.Exists(this.SpritePath))
            {
                try
                {
                    GetTexture = new GTA.Texture(File.ReadAllBytes(this.SpritePath));
                }
                catch
                {
                    GetTexture = null;
                }
            }
            else
            {
                //File.AppendAllText(".\\Scripts\\ZeMenu\\notfound.log", "Model name: " + DisplayText + " | " + SpritePath + "\n");
                GetTexture = null;
            }
        }
        internal override void Activate()
        {
            foreach (Commands.Command c in Actions)
            {
                c.DoAction();
            }
        }
    }
}
