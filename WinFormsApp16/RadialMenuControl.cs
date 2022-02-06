using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace WinFormsApp16
{

    public partial class RadialMenuControl : PictureBox
    {
        readonly System.Windows.Forms.Timer AnimationTimer;
        public EventHandler<MenuClickEventArgs>? OnMenuClick;

        public new Font Font { get; set; } = new Font("Yu Gothic UI", 14.0f);
        public float FocusBorder { get; set; } = 5.0f;
        public float CenterSize { get; set; } = 60.0f;
        public float CenterBorder { get; set; } = 5.0f;
        public float Step { get; set; } = 4.0f;
        public List<string> Items { get; set; } = new List<string>() { "One", "Two", "Three", "Four", "Five", "Six" };
        int CircleSize => Math.Min(Width, Height);
        float OX => Width / 2.0f;
        float OY => Height / 2.0f;

        float oldAngle = 0;
        float oldScale = 0;
        float oldAlpha = 0;

        Bitmap? TmpImage;

        //フォーカスの状態
        bool FocusedCenter;
        int FocusedMenuIndex;

        /// <summary>
        /// コンストラクタ処理
        /// </summary>
        public RadialMenuControl()
        {
            BackColor = Color.Transparent;
            DoubleBuffered = true;
            AnimationTimer = new();
            AnimationTimer.Interval = 1;
            AnimationTimer.Tick += new EventHandler(AnimationTimer_Tick);
            StartAnimation(AnimationState.Enter);
        }

        AnimationState AnimationState;
        float AnimationCount;

        internal void StartAnimation(AnimationState animationState)
        {
            AnimationCount = 0;
            AnimationState = animationState;
            if (animationState == AnimationState.Enter)
            {
                FocusedCenter = false;
                FocusedMenuIndex = -1;
            }
            AnimationTimer.Start();
        }

        void AnimationTimer_Tick(object sender, EventArgs e)
        {
            if (AnimationState.Enter == AnimationState)
                DrawEnter();
            else
                DrawLeave();

            if (60 <= AnimationCount)
                AnimationTimer.Stop();
            else
            {
                AnimationCount = Math.Min(60.0f, AnimationCount + Step);
            }
        }

        void DrawEnter()
        {
            var angle = AnimationCount - 60;
            var scale = AnimationCount / 60.0f;
            var alpha = scale;
            DrawRadialMenu(angle, scale, alpha);
        }

        void DrawLeave()
        {
            var angle = AnimationCount;
            var scale = 1.0f - AnimationCount / 60.0f;
            var alpha = scale;
            DrawRadialMenu(angle, scale, alpha);
        }

        static RectangleF Calc(float ox, float oy, float size)
        {
            var cx = ox - size / 2;
            var cy = oy - size / 2;
            return new RectangleF(cx, cy, size, size);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (!AnimationTimer.Enabled && AnimationState == AnimationState.Enter)
            {
                DrawRadialMenu(0.0f, 1.0f, 1.0f);
            }
            base.OnPaint(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            //フォーカスチェック
            if (AnimationTimer.Enabled)
                return;

            var mx = e.X - OX;
            var my = e.Y - OY;
            var ml = Math.Sqrt(mx * mx + my * my);

            //中央かどうか
            if (ml <= (CenterSize + CenterBorder) / 2)
            {
                var isUpdate = !FocusedCenter;
                FocusedCenter = true;
                FocusedMenuIndex = -1;
                if (isUpdate)
                {
                    oldAngle = oldScale = oldAlpha = 0;
                    Invalidate();
                }
            }
            else
            {
                var oldItem = FocusedMenuIndex;
                var angle = 2.0 * Math.PI + Math.Atan2(my, mx);
                FocusedCenter = false;
                FocusedMenuIndex = (int)(Items.Count * angle / (2.0 * Math.PI)) % Items.Count;
                if (oldItem != FocusedMenuIndex)
                {
                    oldAngle = oldScale = oldAlpha = 0;
                    Invalidate();
                }
            }
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            //base.OnMouseClick(e);
            OnMenuClick?.Invoke(this, new MenuClickEventArgs(FocusedCenter, FocusedMenuIndex));
            StartAnimation(AnimationState.Leave);
        }

        void CheckSize()
        {
            if (Image?.Width == Width && Image?.Height == Height)
                if (TmpImage?.Width == Width && TmpImage?.Height == Height)
                    return;


            Image?.Dispose();
            Image = new Bitmap(Width, Height);
            TmpImage?.Dispose();
            TmpImage = new Bitmap(Width, Height);
            oldAngle = oldScale = oldAlpha = 0;
        }

        /// <summary>
        /// 描画
        /// </summary>
        /// <param name="angle">０～３６０（角度）</param>
        /// <param name="scale">０～１<（原寸）/param>
        /// <param name="alpha">（透明）０～１（不透明）</param>
        void DrawRadialMenu(float angle, float scale, float alpha)
        {
            SuspendLayout();
            CheckSize();

            if (oldAngle == angle && oldScale == scale && oldAlpha == alpha)
                return;
            oldAngle = angle;
            oldScale = scale;
            oldAlpha = alpha;

            using var g = Graphics.FromImage(TmpImage);
            using var d = Graphics.FromImage(Image);

            //g.CompositingQuality = CompositingQuality.Default;
            //g.SmoothingMode = SmoothingMode.None;
            //g.InterpolationMode = InterpolationMode.Default;
            //g.PixelOffsetMode = PixelOffsetMode.Default;

            g.Clear(Color.White);
            d.Clear(Color.White);

            //分割
            var n = Items.Count;
            var step = 360.0 / n;
            var adjustDegree = Math.PI / n;
            for (var idx = 0; idx < n; idx++)
            {
                var degree = 2.0 * Math.PI * idx / n;
                var px = (float)(CircleSize * Math.Cos(degree));
                var py = (float)(CircleSize * Math.Sin(degree));
                var str = Items[idx];
                var strSize = g.MeasureString(str, Font, CircleSize);

                var kx = OX + (float)(CircleSize * Math.Cos(degree + adjustDegree) / 3);
                var ky = OY + (float)(CircleSize * Math.Sin(degree + adjustDegree) / 3);

                var color = idx == FocusedMenuIndex ? Brushes.Gray : Brushes.LightGray;
                g.DrawString(str, Font, Brushes.LightGray, new PointF(kx - strSize.Width / 2, ky));
                g.DrawPie(new Pen(color, FocusBorder), Calc(OX, OY, CircleSize - FocusBorder), 360 / n * idx, 360 / n);
            }
            for (var idx = 0; idx < n; idx++)
            {
                var degree = 2.0 * Math.PI * idx / n;
                var px = (float)(CircleSize * Math.Cos(degree));
                var py = (float)(CircleSize * Math.Sin(degree));
                g.DrawLine(new Pen(Color.White, FocusBorder + 1), new PointF(OX, OY), new PointF(OX + px, OY + py));
            }

            //中心
            g.FillEllipse(FocusedCenter ? Brushes.Gray : Brushes.LightGray, Calc(OX, OY, CenterSize + CenterBorder));
            g.FillEllipse(Brushes.White, Calc(OX, OY, CenterSize));

            TmpImage.MakeTransparent();

            //回転
            d.ResetTransform();
            d.TranslateTransform(-Width / 2, -Height / 2);
            d.RotateTransform(angle, MatrixOrder.Append);
            d.TranslateTransform(+Width / 2, +Height / 2, MatrixOrder.Append);

            //縮小
            var offset = (1.0f - scale) / 2.0f;
            if (0 < scale)
            {
                d.ScaleTransform(scale, scale, MatrixOrder.Append);
                d.TranslateTransform(offset * Width, offset * Height, MatrixOrder.Append);
            }

            //半透明画像設定
            var cm = new ColorMatrix
            {
                Matrix00 = 1,
                Matrix11 = 1,
                Matrix22 = 1,
                Matrix33 = alpha,
                Matrix44 = 1
            };
            var ia = new ImageAttributes();
            ia.SetColorMatrix(cm);

            //描画
            d.DrawImage(TmpImage, new Rectangle(0, 0, Width, Height), 0.0f, 0.0f, Width, Height, GraphicsUnit.Pixel, ia);
            (Image as Bitmap)?.MakeTransparent();
            Invalidate();
            ResumeLayout(true);
        }
    }

    internal enum AnimationState
    {
        Enter,
        Leave,
    }
    
}
