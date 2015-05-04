using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SerialportSample
{
    /// <summary>
    /// 使用率图形控件
    /// zgke@sina.com
    /// qq:116149
    /// </summary>
    public class MonitorControl :UserControl
    {

        public MonitorControl()
        {
            InitializeComponent();
        }

        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose( bool disposing )
        {
            if ( disposing && ( components != null ) )
            {
                components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region 组件设计器生成的代码

        private void InitializeComponent()
        {
            this.ViewGroup = new System.Windows.Forms.GroupBox();
            this.ViewLabel = new System.Windows.Forms.Label();
            this.ViewPicture = new System.Windows.Forms.PictureBox();
            this.LineGroup = new System.Windows.Forms.GroupBox();
            this.LineLabel = new System.Windows.Forms.Label();
            this.LinePicture = new System.Windows.Forms.PictureBox();
            this.ViewGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ViewPicture)).BeginInit();
            this.LineGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LinePicture)).BeginInit();
            this.SuspendLayout();
            // 
            // ViewGroup
            // 
            this.ViewGroup.Controls.Add(this.ViewLabel);
            this.ViewGroup.Controls.Add(this.ViewPicture);
            this.ViewGroup.Dock = System.Windows.Forms.DockStyle.Left;
            this.ViewGroup.Location = new System.Drawing.Point(0, 0);
            this.ViewGroup.Name = "ViewGroup";
            this.ViewGroup.Size = new System.Drawing.Size(60, 209);
            this.ViewGroup.TabIndex = 0;
            this.ViewGroup.TabStop = false;
            this.ViewGroup.Text = "Memory";
            // 
            // ViewLabel
            // 
            this.ViewLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ViewLabel.Location = new System.Drawing.Point(10, 194);
            this.ViewLabel.Name = "ViewLabel";
            this.ViewLabel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ViewLabel.Size = new System.Drawing.Size(41, 12);
            this.ViewLabel.TabIndex = 1;
            this.ViewLabel.Text = "";
            this.ViewLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ViewPicture
            // 
            this.ViewPicture.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.ViewPicture.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ViewPicture.Location = new System.Drawing.Point(6, 13);
            this.ViewPicture.Name = "ViewPicture";
            this.ViewPicture.Size = new System.Drawing.Size(48, 178);
            this.ViewPicture.TabIndex = 0;
            this.ViewPicture.TabStop = false;
            // 
            // LineGroup
            // 
            this.LineGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LineGroup.Controls.Add(this.LineLabel);
            this.LineGroup.Controls.Add(this.LinePicture);
            this.LineGroup.Location = new System.Drawing.Point(60, 0);
            this.LineGroup.Name = "LineGroup";
            this.LineGroup.Size = new System.Drawing.Size(233, 209);
            this.LineGroup.TabIndex = 1;
            this.LineGroup.TabStop = false;
            // 
            // LineLabel
            // 
            this.LineLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LineLabel.Location = new System.Drawing.Point(6, 194);
            this.LineLabel.Name = "LineLabel";
            this.LineLabel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.LineLabel.Size = new System.Drawing.Size(221, 12);
            this.LineLabel.TabIndex = 2;
            this.LineLabel.Text = "";
            this.LineLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.LineLabel.Click += new System.EventHandler(this.LineLabel_Click);
            // 
            // LinePicture
            // 
            this.LinePicture.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LinePicture.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.LinePicture.Location = new System.Drawing.Point(6, 13);
            this.LinePicture.Name = "LinePicture";
            this.LinePicture.Size = new System.Drawing.Size(221, 178);
            this.LinePicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.LinePicture.TabIndex = 1;
            this.LinePicture.TabStop = false;
            // 
            // MonitorControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.LineGroup);
            this.Controls.Add(this.ViewGroup);
            this.Name = "MonitorControl";
            this.Size = new System.Drawing.Size(296, 209);
            this.Load += new System.EventHandler(this.MonitorControl_Load);
            this.ClientSizeChanged += new System.EventHandler(this.MonitorControl_ClientSizeChanged);
            this.ViewGroup.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ViewPicture)).EndInit();
            this.LineGroup.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.LinePicture)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox ViewGroup;
        private System.Windows.Forms.Label ViewLabel;
        private System.Windows.Forms.PictureBox ViewPicture;
        private System.Windows.Forms.GroupBox LineGroup;
        private System.Windows.Forms.Label LineLabel;
        private System.Windows.Forms.PictureBox LinePicture;
        private Color m_BlackColor = Color.Black;
        private Color m_UserColor = Color.FromArgb( 0, 255, 0 );
        private decimal _OldHeight = 0; //上次的位置


        private void MonitorControl_Load( object sender, EventArgs e )
        {
            ViewPicture.Image = LoadViewImage( 0 );
            _OldHeight = ViewPicture.Height;
            SetLineBlackImage();
        }
        public string MonitorName { get; set; }
        /// <summary>
        /// 显示图形
        /// </summary>
        /// <param name="p_Value">数据</param>
        /// <param name="p_MaxValue">最大数据</param>    
        public void MonitorView( decimal p_Value, decimal p_MaxValue )
        {
            try
            {
                decimal _Value = 0;
                if (p_Value != 0 && p_MaxValue != 0) _Value = p_Value / (p_MaxValue / 100);
                int _ValueInt = (int)_Value;

                this.Invoke((MethodInvoker)delegate
                {
                    ViewPicture.Image = LoadViewImage(_Value);
                    ViewLabel.Text = string.Empty;

                    LinePicture.Image = LoadLineImage(_Value);

                    LineLabel.Text = MonitorName;
                    LineLabel.Text = p_Value.ToString("#0.0") + "/" + p_MaxValue.ToString("#0.0");
                });
            }
            catch
            { }
        }

        /// <summary>
        /// 背景色
        /// </summary>
        public Color MonitorBlackColor { get { return m_BlackColor; } set { m_BlackColor = value; } }
        /// <summary>
        /// 使用色彩
        /// </summary>
        public Color MonitorUserColor { get { return m_UserColor; } set { m_UserColor = value; } }

        private Image LoadViewImage( decimal p_Value )
        {
            Bitmap _ViewImage = new Bitmap( ViewPicture.Width, ViewPicture.Height );
            Graphics _ViewGraphics = Graphics.FromImage( _ViewImage );
            _ViewGraphics.Clear( m_BlackColor );
            decimal _Value = ( decimal )ViewPicture.Height / 100 * p_Value;
            Rectangle _Rectangle = new Rectangle( 0, ViewPicture.Height - ( int )_Value, ViewPicture.Width, ( int )_Value );
            _ViewGraphics.FillRectangle( new SolidBrush( m_UserColor ), _Rectangle );
            _ViewGraphics.Dispose();
            return _ViewImage;
        }

        /// <summary>
        /// 获取线图形
        /// </summary>
        /// <param name="p_Value"></param>
        /// <returns></returns>
        private Image LoadLineImage( decimal p_Value )
        {
            Bitmap _LineImage = new Bitmap( LinePicture.Width, LinePicture.Height );
            Graphics _LineGraphics = Graphics.FromImage( _LineImage );

            if ( LinePicture.Image != null ) _LineGraphics.DrawImage( LinePicture.Image, -5, 0 );
            Pen _LinePen = new Pen( new SolidBrush( m_UserColor ), 1 );
            decimal _HeightValue = LinePicture.Height - ( ( decimal )LinePicture.Height / 100 * p_Value );
            _LineGraphics.DrawLine( _LinePen, new Point( _LineImage.Width - 5, ( int )_OldHeight ), new Point( _LineImage.Width - 1, ( int )_HeightValue ) );

            _OldHeight = _HeightValue;
            _LineGraphics.Dispose();
            return _LineImage;
        }

        /// <summary>
        /// 获取背景图形
        /// </summary>
        private void SetLineBlackImage()
        {
            int _Height = ( LinePicture.Height ) / 10;
            int _Width = ( LinePicture.Width ) / 10;
            Bitmap _BlackImage = new Bitmap( LinePicture.Width, LinePicture.Height );
            Graphics _Graphics = Graphics.FromImage( _BlackImage );
            _Graphics.Clear( m_BlackColor );
            Pen _LinePen = new Pen( new SolidBrush( Color.FromArgb( 0, 128, 0 ) ), 1 );
            for ( int i = 0; i != 10; i++ )
            {
                _Graphics.DrawLine( _LinePen, new Point( 0, i * _Height ), new Point( LinePicture.Width, i * _Height ) );
                _Graphics.DrawLine( _LinePen, new Point( i * _Width, 0 ), new Point( i * _Width, LinePicture.Height ) );
            }

            _Graphics.Dispose();
            LinePicture.BackgroundImage = _BlackImage;
        }

        private void MonitorControl_ClientSizeChanged( object sender, EventArgs e )
        {
            LinePicture.Image = null;
        }

        private void LineLabel_Click( object sender, EventArgs e )
        {

        }

    }
}
