using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TextBoxEmailAutocomplete
{
    /// <summary>
    /// Very basic slider control with selection range.
    /// </summary>
    [Description("Very basic slider control with selection range.")]
    public partial class DoubleTrackBar : UserControl
    {
        /// <summary>
        /// Minimum value of the slider.
        /// </summary>
        [Description("Minimum value of the slider.")]
        public int Min
        {
            get { return min; }
            set { min = value; Invalidate(); }
        }
        int min = 0;
        /// <summary>
        /// Maximum value of the slider.
        /// </summary>
        [Description("Maximum value of the slider.")]
        public int Max
        {
            get { return max; }
            set { max = value; Invalidate(); }
        }
        int max = 100;

        public Boolean ValueVisible
        {
            get { return valueVisible; }
            set { valueVisible = value; Invalidate(); }
        }
        Boolean valueVisible = true;
        /// <summary>
        /// Minimum value of the selection range.
        /// </summary>
        [Description("Minimum value of the selection range.")]
        public int SelectedMin
        {
            get { return selectedMin; }
            set
            {
                selectedMin = value;
                if (SelectionChanged != null)
                    SelectionChanged(this, null);
                Invalidate();
            }
        }
        int selectedMin = 0;
        /// <summary>
        /// Maximum value of the selection range.
        /// </summary>
        [Description("Maximum value of the selection range.")]
        public int SelectedMax
        {
            get { return selectedMax; }
            set
            {
                selectedMax = value;
                if (SelectionChanged != null)
                    SelectionChanged(this, null);
                Invalidate();
            }
        }
        int selectedMax = 100;
        /// <summary>
        /// Current value.
        /// </summary>
        [Description("Current value.")]
        public int Value
        {
            get { return value; }
            set
            {
                this.value = value;
                if (ValueChanged != null)
                    ValueChanged(this, null);
                Invalidate();
            }
        }
        int value = 50;

        public Color TrackBarColor
        {
            get { return trackBarColor; }
            set { trackBarColor = value; Invalidate(); }
        }
        Color trackBarColor = Color.Blue ;



        /// <summary>
        /// Fired when SelectedMin or SelectedMax changes.
        /// </summary>
        [Description("Fired when SelectedMin or SelectedMax changes.")]
        public event EventHandler SelectionChanged;
        /// <summary>
        /// Fired when Value changes.
        /// </summary>
        [Description("Fired when Value changes.")]
        public event EventHandler ValueChanged;

        public DoubleTrackBar()
        {
            InitializeComponent();
            //avoid flickering
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            Paint += new PaintEventHandler(SelectionRangeSlider_Paint);
            MouseDown += new MouseEventHandler(SelectionRangeSlider_MouseDown);
            MouseMove += new MouseEventHandler(SelectionRangeSlider_MouseMove);
        }

        void SelectionRangeSlider_Paint(object sender, PaintEventArgs e)
        {
            //paint background in white
            e.Graphics.FillRectangle(Brushes.White, ClientRectangle);
            //paint selection range in blue
            Rectangle selectionRect = new Rectangle(
                (selectedMin - Min) * Width / (Max - Min),
                Height/2,
                (selectedMax - selectedMin) * Width / (Max - Min),
                Height);
            e.Graphics.FillRectangle(new SolidBrush(trackBarColor), selectionRect);

            Point[] points = new Point[3];
            points[0] = new Point((selectedMin - Min) * Width / (Max - Min), Height / 2);
            points[1] = new Point((selectedMin - Min) * Width / (Max - Min)-4, 0);
            points[2] = new Point((selectedMin - Min) * Width / (Max - Min)+4, 0);
            e.Graphics.FillPolygon(Brushes.Gray , points);

            points = new Point[3];
            points[0] = new Point((selectedMax - Min) * Width / (Max - Min) - 1, Height / 2);
            points[1] = new Point((selectedMax - Min) * Width / (Max - Min) - 5, 0);
            points[2] = new Point((selectedMax - Min) * Width / (Max - Min) + 3, 0);
            e.Graphics.FillPolygon(Brushes.Gray, points);

            //draw a black frame around our control
            e.Graphics.DrawRectangle(Pens.Gray, 0, 0, Width - 1, Height - 1);
            e.Graphics.DrawLine(Pens.Black, 0, Height / 2, Width , Height/2);
            //draw a simple vertical line at the Value position
            if (ValueVisible) {
                e.Graphics.DrawLine(Pens.Black,
                (Value - Min) * Width / (Max - Min), 0,
                (Value - Min) * Width / (Max - Min), Height);
            }
            
        }

        void SelectionRangeSlider_MouseDown(object sender, MouseEventArgs e)
        {
            //check where the user clicked so we can decide which thumb to move
            int pointedValue = Min + e.X * (Max - Min) / Width;
            int distValue = Math.Abs(pointedValue - Value);
            int distMin = Math.Abs(pointedValue - SelectedMin);
            int distMax = Math.Abs(pointedValue - SelectedMax);
            int minDist = Math.Min(distValue, Math.Min(distMin, distMax));
            if (minDist == distValue)
                movingMode = MovingMode.MovingValue;
            else if (minDist == distMin)
                movingMode = MovingMode.MovingMin;
            else
                movingMode = MovingMode.MovingMax;
            //call this to refreh the position of the selected thumb
            SelectionRangeSlider_MouseMove(sender, e);
        }

        void SelectionRangeSlider_MouseMove(object sender, MouseEventArgs e)
        {
            //if the left button is pushed, move the selected thumb
            if (e.Button != MouseButtons.Left)
                return;
            int pointedValue = 0;
            if (movingMode == MovingMode.MovingValue)
            {
                pointedValue = Min + e.X * (Max - Min) / Width;
                Value = pointedValue;
            }
            else if (movingMode == MovingMode.MovingMin)
            {
                pointedValue = Math.Max(Min, Min + e.X * (Max - Min) / Width);
                SelectedMin = pointedValue;
            }
            else if (movingMode == MovingMode.MovingMax)
            {
                pointedValue = Math.Min(Max, Min + e.X * (Max - Min) / Width);
                SelectedMax = pointedValue;
            }
        }

        /// <summary>
        /// To know which thumb is moving
        /// </summary>
        enum MovingMode { MovingValue, MovingMin, MovingMax }
        MovingMode movingMode;
    }
}
