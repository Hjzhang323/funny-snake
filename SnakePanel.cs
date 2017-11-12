using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FunnySnake
{
    class SnakePanel : Panel
    {
        public Map map { get; set; } = null;
        public Serpent serpent { get; set; } = null;

        protected override void OnResize(EventArgs eventargs)
        {
            base.OnResize(eventargs);

            if (map != null)
            {
                map.ResizeMap(this);
                if (serpent != null)
                    map.DrawSerpentCells(serpent);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (map != null)
            {
                map.RedrawMap();
                if (serpent != null)
                    map.DrawSerpentCells(serpent);
            }
        }
    }
}
