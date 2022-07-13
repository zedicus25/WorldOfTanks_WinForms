using Client.Controller;
using Client.Model;
using Client.Model.Directions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client.View.Forms
{
    public partial class GameForm : Form
    {
        private Graphics _graphics;
        private Bitmap _bufferedImage;
        private Task _drawTask;
        Tank tanl;
        MyClientController cl;
        public GameForm()
        {
            InitializeComponent();
            cl = new MyClientController();
            _bufferedImage = new Bitmap(this.Width, this.Height);
            _graphics = Graphics.FromImage(_bufferedImage);
            tanl = new Tank(new Position(50, 50), "123", "tank.png", new DownDirection(), 10);
            this.DoubleBuffered = true;
            _drawTask = new Task(Draw);
            _drawTask.Start();

        }

        private void Draw()
        {
            while (true)
            {
                tanl.Move(this.Width, this.Height);
                _graphics.DrawImage(Properties.Resources.back, this.ClientRectangle);
                _graphics.DrawImage(tanl.Sprite.Icon, new Point(tanl.Pos.X, tanl.Pos.Y));
                if(tanl.Projectile.CanMove)
                    _graphics.DrawImage(tanl.Projectile.Sprite.Icon, new Point(tanl.Projectile.Pos.X, tanl.Projectile.Pos.Y));

                this.BackgroundImage = _bufferedImage;
                this.Invalidate();
                Thread.Sleep(100);

            }
            
        }

        private void GameForm_KeyUp(object sender, KeyEventArgs e)
        {
            tanl.SetUserInput(e.KeyCode);
        }
    }
}
