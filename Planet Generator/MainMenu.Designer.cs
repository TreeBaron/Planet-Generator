namespace Planet_Generator
{
    partial class MainMenu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.GenerateTextureButton = new System.Windows.Forms.Button();
            this.SaveButton = new System.Windows.Forms.Button();
            this.PictureBox = new System.Windows.Forms.PictureBox();
            this.ResolutionTextBox = new System.Windows.Forms.TextBox();
            this.ResolutionLabel = new System.Windows.Forms.Label();
            this.PlanetNameLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.AtmosphereTextBox = new System.Windows.Forms.TextBox();
            this.Upscale = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // GenerateTextureButton
            // 
            this.GenerateTextureButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.GenerateTextureButton.Location = new System.Drawing.Point(12, 998);
            this.GenerateTextureButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.GenerateTextureButton.Name = "GenerateTextureButton";
            this.GenerateTextureButton.Size = new System.Drawing.Size(300, 63);
            this.GenerateTextureButton.TabIndex = 0;
            this.GenerateTextureButton.Text = "Generate Texture";
            this.GenerateTextureButton.UseVisualStyleBackColor = true;
            this.GenerateTextureButton.Click += new System.EventHandler(this.GenerateTextureButton_Click);
            // 
            // SaveButton
            // 
            this.SaveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveButton.Location = new System.Drawing.Point(512, 998);
            this.SaveButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(300, 63);
            this.SaveButton.TabIndex = 1;
            this.SaveButton.Text = "Save As...";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // PictureBox
            // 
            this.PictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.PictureBox.Location = new System.Drawing.Point(12, 12);
            this.PictureBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.PictureBox.Name = "PictureBox";
            this.PictureBox.Size = new System.Drawing.Size(800, 731);
            this.PictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PictureBox.TabIndex = 2;
            this.PictureBox.TabStop = false;
            // 
            // ResolutionTextBox
            // 
            this.ResolutionTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ResolutionTextBox.Location = new System.Drawing.Point(144, 944);
            this.ResolutionTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ResolutionTextBox.Name = "ResolutionTextBox";
            this.ResolutionTextBox.Size = new System.Drawing.Size(100, 31);
            this.ResolutionTextBox.TabIndex = 3;
            this.ResolutionTextBox.Text = "400";
            // 
            // ResolutionLabel
            // 
            this.ResolutionLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ResolutionLabel.AutoSize = true;
            this.ResolutionLabel.Location = new System.Drawing.Point(12, 944);
            this.ResolutionLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.ResolutionLabel.Name = "ResolutionLabel";
            this.ResolutionLabel.Size = new System.Drawing.Size(114, 25);
            this.ResolutionLabel.TabIndex = 4;
            this.ResolutionLabel.Text = "Resolution";
            // 
            // PlanetNameLabel
            // 
            this.PlanetNameLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.PlanetNameLabel.AutoSize = true;
            this.PlanetNameLabel.Location = new System.Drawing.Point(506, 940);
            this.PlanetNameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.PlanetNameLabel.Name = "PlanetNameLabel";
            this.PlanetNameLabel.Size = new System.Drawing.Size(135, 25);
            this.PlanetNameLabel.TabIndex = 5;
            this.PlanetNameLabel.Text = "Planet Name";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 892);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 25);
            this.label1.TabIndex = 7;
            this.label1.Text = "Atmosphere";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // AtmosphereTextBox
            // 
            this.AtmosphereTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.AtmosphereTextBox.Location = new System.Drawing.Point(144, 892);
            this.AtmosphereTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.AtmosphereTextBox.Name = "AtmosphereTextBox";
            this.AtmosphereTextBox.Size = new System.Drawing.Size(100, 31);
            this.AtmosphereTextBox.TabIndex = 6;
            this.AtmosphereTextBox.Text = "4";
            // 
            // Upscale
            // 
            this.Upscale.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Upscale.AutoSize = true;
            this.Upscale.Location = new System.Drawing.Point(511, 887);
            this.Upscale.Name = "Upscale";
            this.Upscale.Size = new System.Drawing.Size(255, 29);
            this.Upscale.TabIndex = 8;
            this.Upscale.Text = "Dynamic Upscale Res";
            this.Upscale.UseVisualStyleBackColor = true;
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(834, 1062);
            this.Controls.Add(this.Upscale);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.AtmosphereTextBox);
            this.Controls.Add(this.PlanetNameLabel);
            this.Controls.Add(this.ResolutionLabel);
            this.Controls.Add(this.ResolutionTextBox);
            this.Controls.Add(this.PictureBox);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.GenerateTextureButton);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "MainMenu";
            this.Text = "Planet Generator - By John Dodd";
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button GenerateTextureButton;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.PictureBox PictureBox;
        private System.Windows.Forms.TextBox ResolutionTextBox;
        private System.Windows.Forms.Label ResolutionLabel;
        private System.Windows.Forms.Label PlanetNameLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox AtmosphereTextBox;
        private System.Windows.Forms.CheckBox Upscale;
    }
}

