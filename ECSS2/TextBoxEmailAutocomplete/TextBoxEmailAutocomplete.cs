
// Code created by Greg Benson
// Provided with BSD license.  See license.txt for details.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace CustomControls
{
    public partial class TextBoxEmailAutocomplete : UserControl
    {
        // Email autocomplete source conditioning is:
        //      "FirstName LastName" <email@domain.com>
        // or this is also okay:
        //      "LastName, FirstName <email@domain.com>
        // Email address only is (quotes and angle brackets optional):
        //      email@domain.com
        //      "email@domain.com"
        [
        Browsable(true),
        Description("Data source for email auto completion, format is \"Name\" <email@domain.com>")
        ]
        public string[] EmailAutocompleteSource
        {
            get { return DataSet; }
            set { DataSet = value; }
        }

        [
        Browsable(true),
        DefaultValue(typeof(System.Drawing.Color), "System.Drawing.Color.FromKnownColor(KnownColor.ControlLight)"),
        Description("Highlight color for suggestions list")
        ]
        public Color HighlightColor
        {
            get { return lbSuggestions.HighlightColor; }
            set { lbSuggestions.HighlightColor = value; }
        }

        [
        Browsable(true),
        Description("Get or set the control text")
        ]
        public override string Text
        {
            get { return tbInput.Text; }
            set { tbInput.Text = value; }
        }

        [
        Browsable(true),
        Description("Foreground text color")
        ]
        public Color TextColor
        {
            get { return tbInput.ForeColor; }
            set { tbInput.ForeColor = value; lbSuggestions.ForeColor = value; }
        }

        private string[] DataSet;
        private string CurrentWord = "";
        private const int MaxSuggestions = 30;

        public TextBoxEmailAutocomplete()
        {
            InitializeComponent();

            // Key components are:
            // tbInput
            // lbSuggestions
            tbInput.Clear();
            lbSuggestions.Items.Clear();
            lbSuggestions.Hide();

            TextBoxEmailAutocomplete_Resize(this, null);
        }

        private void tbInput_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case (Keys.Up):
                    if (lbSuggestions.Visible)
                    {
                        if (lbSuggestions.SelectedIndex > 0) lbSuggestions.SelectedIndex--;
                        e.Handled = true;
                    }
                    break;

                case (Keys.Down):
                    if (lbSuggestions.Visible)
                    {
                        if (lbSuggestions.SelectedIndex < (lbSuggestions.Items.Count - 1)) lbSuggestions.SelectedIndex++;
                        e.Handled = true;
                    }
                    break;

                case (Keys.PageUp):
                    if (lbSuggestions.Visible)
                    {
                        lbSuggestions.SelectedIndex = 0;
                        e.Handled = true;
                    }
                    break;

                case (Keys.PageDown):
                    if (lbSuggestions.Visible)
                    {
                        lbSuggestions.SelectedIndex = lbSuggestions.Items.Count - 1;
                        e.Handled = true;
                    }
                    break;

                case (Keys.Left):
                case (Keys.Right):
                    if (lbSuggestions.Visible)
                    {
                        if (tbInput.Text[Math.Max(tbInput.SelectionStart - 1, 0)] == ' ')
                        {
                            lbSuggestions.Hide();
                        }
                    }
                    e.Handled = false; // still move left or right in textbox
                    break;

                case (Keys.Home):
                    if (lbSuggestions.Visible) lbSuggestions.Hide();
                    if (e.Modifiers == Keys.Control)
                    {
                        this.ScrollToStart();
                    }
                    e.Handled = false;
                    break;
                case (Keys.End):
                    if (lbSuggestions.Visible) lbSuggestions.Hide();
                    if (e.Modifiers == Keys.Control)
                    {
                        this.ScrollToEnd();
                    }
                    e.Handled = false;
                    break;
                case (Keys.Escape):
                    if (lbSuggestions.Visible) lbSuggestions.Hide();
                    e.Handled = true;
                    break;

                case (Keys.Tab):
                case (Keys.Enter):
                case (Keys.Oemcomma):
                    if (lbSuggestions.Visible)
                    {
                        e.SuppressKeyPress = true;
                    }
                    AddCurrentListBoxSelection();
                    e.Handled = true;
                    break;
            }
        }

        private void tbInput_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                // Handled by KeyDown events
                case (Keys.Down):
                case (Keys.Up):
                case (Keys.Left):
                case (Keys.Right):
                case (Keys.Home):
                case (Keys.End):
                case (Keys.Escape):
                case (Keys.Tab):
                case (Keys.Enter):
                    if (TextKeyDown  != null)
                        TextKeyDown(this, e);
                    break;
                case (Keys.ControlKey):
                case (Keys.ShiftKey):
                case (Keys.PageUp):
                case (Keys.PageDown):
                    e.Handled = true;
                    break;
            }
            if (e.Handled == false)
            {
                CurrentWord = lbSuggestions.HighlightWord = GetCurrentWord();
                UpdateListBox(CurrentWord);
            }
            this.InvalidateEx();
        }

        private void AddCurrentListBoxSelection()
        {
            if (lbSuggestions.Visible)
            {
                tbInput.SelectionLength = 0;

                int currPos = tbInput.SelectionStart;
                int lastSpace = 1 + tbInput.Text.Substring(0, tbInput.SelectionStart).LastIndexOf(' ');
                tbInput.SelectionStart = currPos - CurrentWord.Length;
                tbInput.Text = tbInput.Text.Remove(lastSpace, CurrentWord.Length);
                tbInput.Text = tbInput.Text.Insert(lastSpace, ((string)lbSuggestions.SelectedItem + ", "));
                tbInput.SelectionStart = currPos + ((string)lbSuggestions.SelectedItem).Length + 2;
                tbInput.ScrollToCaret();

                lbSuggestions.Hide();
            }
        }

        public string GetCurrentWord()
        {
            string left = tbInput.Text.Substring(0, tbInput.SelectionStart);
            string right = tbInput.Text.Substring(tbInput.SelectionStart);

            int leftInd = left.LastIndexOf(' ');
            if (leftInd == -1)
            {
                leftInd = 0;
            }
            else
            {
                // push index up to move from the space to the first character of the next word
                leftInd++;
            }

            int rightInd = right.IndexOf(' ');
            if (rightInd == -1)
            {
                rightInd = right.Length;
            }
            rightInd += left.Length; // change from relative to absolute position

            return tbInput.Text.Substring(leftInd, (rightInd - leftInd));
        }

        private void UpdateListBox(string keyword)
        {
            lbSuggestions.BeginUpdate(); 
            lbSuggestions.Items.Clear();
            string[] list = GetListOfMatchingStrings(keyword, MaxSuggestions);
            if ((list.Length > 0) && (!String.IsNullOrEmpty(list[0])))
            {
                lbSuggestions.BringToFront();
                lbSuggestions.Show();
                this.BringToFront();
                lbSuggestions.Height = Math.Min( (this.Height - this.tbInput.Height), (lbSuggestions.ItemHeight * list.Length) + 4); // Don't know where the 4 pixels come from
                lbSuggestions.Items.AddRange(list);
                lbSuggestions.SelectedIndex = 0;
             
                this.Refresh();
            }
            else
            {
                lbSuggestions.Hide();
            }
            lbSuggestions.EndUpdate();
        }

        private string[] GetListOfMatchingStrings(string key) { return GetListOfMatchingStrings(key, -1); }
        private string[] GetListOfMatchingStrings(string key, int MaxNumberOfMatches)
        {
            if (String.IsNullOrEmpty(key)) return new string[0];

            bool LimitMatches = true;
            if (MaxNumberOfMatches == -1)
            {
                LimitMatches = false;
            }

            StringBuilder rtn = new StringBuilder();
            int count = 0;
            key = Regex.Escape(key); // escape out special characters to make key a fully literal string
            foreach (string data in DataSet)
            {
                // Pattern: ((^)|([\s"<]))(?<key>key)\S*\b
                string pattern = "((^)|([\\s\"<\\.]))(?<key>" + key + ")\\S*\\b";
                if (Regex.Match(data, pattern,
                    RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture)
                    .Success)
                {
                    rtn.Append(data + "\n");
                    count++;

                    if ((LimitMatches) && (count >= MaxNumberOfMatches))
                    {
                        break; // kill the foreach loop
                    }
                }
            }
            if (rtn.Length > 0) rtn.Remove(rtn.Length - 1, 1); // trim the final '\n'
            return rtn.ToString().Split('\n');
        }

        #region Make Control Background Transparent
        // From: http://bytes.com/topic/c-sharp/answers/248836-need-make-user-control-transparent
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x00000020;
                return cp;
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            // dont code anything here. Leave blank
        }

        protected void InvalidateEx()
        {
            if (Parent == null) return;
            Rectangle rc = new Rectangle(this.Location, this.Size);
            Parent.Invalidate(rc, true);
        }
        #endregion

        private void UpdateControlRegion()
        {
            Region ctlRgn = new Region(tbInput.Bounds);
            if (lbSuggestions.Visible) ctlRgn.Union(lbSuggestions.Bounds);
            this.Region = ctlRgn;
        }

        private void TextBoxEmailAutocomplete_Resize(object sender, EventArgs e)
        {
            tbInput.Width = this.Width; // text box width is same as control
            lbSuggestions.Width = Math.Max(tbInput.Width , 100); // list box width is fraction of text box, down to 100 px

            UpdateControlRegion();
        }

        private void TextBoxEmailAutocomplete_Leave(object sender, EventArgs e)
        {
            lbSuggestions.Hide();
        }

        private void lbSuggestions_VisibleChanged(object sender, EventArgs e)
        {
            if (lbSuggestions.Visible) tbInput.AcceptsTab = true;
            else tbInput.AcceptsTab = false;

            UpdateControlRegion();
        }

        private void lbSuggestions_SizeChanged(object sender, EventArgs e)
        {
            UpdateControlRegion();
        }

        private void lbSuggestions_MouseMove(object sender, MouseEventArgs e)
        {
            lbSuggestions.SelectedIndex = lbSuggestions.IndexFromPoint(e.Location);
        }

        private void lbSuggestions_Click(object sender, EventArgs e)
        {
            AddCurrentListBoxSelection();
        }

        public void ScrollToStart()
        {
            tbInput.SelectionStart = 0;
            tbInput.SelectionLength = 0;
            tbInput.ScrollToCaret();
        }

        public void ScrollToEnd()
        {
            tbInput.SelectionStart = tbInput.Text.Length;
            tbInput.SelectionLength = 0;
            tbInput.ScrollToCaret();
        }

        // Need a custom ListBox class for highlighting the selected word in the list items
        private class CustomListBox : ListBox
        {
            private Color highlightColor = Color.FromKnownColor(KnownColor.ControlLight);
            public Color HighlightColor
            {
                get { return highlightColor; }
                set { highlightColor = value; }
            }

            private string highlightWord = "";
            public string HighlightWord
            {
                get { return highlightWord; }
                set { highlightWord = value; }
            }

            private Color CreateSelectedHighlightColor(Color backColor)
            {
                // Create color as mid-point between background color and highlight color
                return Color.FromArgb(
                    (backColor.R + highlightColor.R) / 2,
                    (backColor.G + highlightColor.G) / 2,
                    (backColor.B + highlightColor.B) / 2);
            }

            protected override void OnDrawItem(DrawItemEventArgs e)
            {
                try
                {
                    string line = this.Items[e.Index].ToString();
                    int firstInd = line.IndexOf(highlightWord, StringComparison.InvariantCultureIgnoreCase);

                    string firstStr = line.Substring(0, firstInd);
                    string secondStr = line.Substring(firstInd, highlightWord.Length);

                    StringFormat format = StringFormat.GenericTypographic;

                    SizeF textSize1 = e.Graphics.MeasureString(firstStr, e.Font, e.Bounds.Location, format);
                    SizeF textSize2 = e.Graphics.MeasureString(secondStr, e.Font, e.Bounds.Location, format);

                    if ((e.State & DrawItemState.Selected) != 0)
                    {
                        // selected item background
                        e.Graphics.FillRectangle(new SolidBrush(e.BackColor), e.Bounds);

                        // draw highligh box
                        Color selectedHighlight = CreateSelectedHighlightColor(e.BackColor);
                        e.Graphics.FillRectangle(new SolidBrush(selectedHighlight),
                            e.Bounds.X + textSize1.Width,
                            e.Bounds.Y,
                            textSize2.Width,
                            e.Bounds.Height);
                    }
                    else
                    {
                        // not selected item
                        e.DrawBackground();

                        // draw highligh box
                        e.Graphics.FillRectangle(new SolidBrush(highlightColor),
                            e.Bounds.X + textSize1.Width,
                            e.Bounds.Y,
                            textSize2.Width,
                            e.Bounds.Height);
                    }

                    e.Graphics.DrawString(
                        line, e.Font, new SolidBrush(e.ForeColor),
                        e.Bounds, format);
                }
                catch
                {
                    try
                    {
                        e.Graphics.Clear(e.BackColor);
                        e.DrawBackground();
                        e.Graphics.DrawString(
                            this.Items[e.Index].ToString(), e.Font,
                            new SolidBrush(e.ForeColor), e.Bounds);
                    }
                    catch
                    {
                        e.Graphics.Clear(Color.White);
                    }
                }
            }
        }

        public event EventHandler ValueChanged;
        private void tbInput_TextChanged(object sender, EventArgs e)
        {
            if (ValueChanged != null)
                ValueChanged(this, e);
        }

        public event EventHandler TextValidating;
        private void tbInput_Validating(object sender, CancelEventArgs e)
        {
            if (TextValidating != null)
                TextValidating(this, e);
        }

        public event EventHandler TextKeyDown;
    }
}
