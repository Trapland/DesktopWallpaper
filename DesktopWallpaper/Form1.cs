using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.IO;
using System.Linq;

namespace DesktopWallpaper
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);
        string[] pictures;
        public Form1()
        {
            InitializeComponent();
            pictures = Directory.GetFiles(@"C:\2\");
            timer1.Interval = 100;
            timer1.Start();
        }

        private void SetWallpaper(string WallpaperLocation, int WallpaperStyle, int TileWallpaper)
        {
            // Sets the actual wallpaper
            SystemParametersInfo(20, 0, WallpaperLocation, 0x01 | 0x02);
            // Set the wallpaper style to streched (can be changed to tile, center, maintain aspect ratio, etc.
            RegistryKey rkWallPaper = Registry.CurrentUser.OpenSubKey("Control Panel\\Desktop", true);
            // Sets the wallpaper style
            rkWallPaper.SetValue("WallpaperStyle", WallpaperStyle);
            // Whether or not this wallpaper will be displayed as a tile
            rkWallPaper.SetValue("TileWallpaper", TileWallpaper);
            rkWallPaper.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Random r = new Random();
            SetWallpaper(pictures[r.Next(2)], 2, 0);
        }
    }
}