using Client.Model.Directions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Model
{
    public class Sprite 
    {
        public Bitmap Icon { get; set; }
        private Size _defaulSize;
        private Bitmap _defaultIcon;

        public Sprite(Bitmap icon)
        {
            Icon = new Bitmap(icon);
            _defaultIcon = new Bitmap(icon);
            _defaulSize = _defaultIcon.Size;
        }

        public void Rotate(IDirection dir)
        {
            if (dir == null)
                throw new NullReferenceException("Direction was null");

            if(dir is LeftDirection)
            {
                Icon = new Bitmap(_defaultIcon, new Size(_defaulSize.Width, _defaulSize.Height));
                Icon.RotateFlip(RotateFlipType.Rotate90FlipXY);
            }
            else if (dir is RightDirection)
            {
                Icon = new Bitmap(_defaultIcon, new Size(_defaulSize.Width, _defaulSize.Height));
                Icon.RotateFlip(RotateFlipType.Rotate270FlipXY);
            }
            else if (dir is DownDirection)
            {
                Icon = new Bitmap(_defaultIcon, new Size(_defaulSize.Width, _defaulSize.Height));
                Icon.RotateFlip(RotateFlipType.Rotate180FlipX);
            }
            else if (dir is UpDirection)
            {
                Icon = new Bitmap(_defaultIcon, new Size(_defaulSize.Width, _defaulSize.Height));
                
            }
        }
    }
}
