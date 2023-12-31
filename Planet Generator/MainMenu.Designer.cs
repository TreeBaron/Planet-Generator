﻿namespace Planet_Generator
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainMenu));
            this.GenerateTextureButton = new System.Windows.Forms.Button();
            this.SaveButton = new System.Windows.Forms.Button();
            this.PictureBox = new System.Windows.Forms.PictureBox();
            this.ResolutionTextBox = new System.Windows.Forms.TextBox();
            this.ResolutionLabel = new System.Windows.Forms.Label();
            this.PlanetNameLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.AtmosphereTextBox = new System.Windows.Forms.TextBox();
            this.Upscale = new System.Windows.Forms.CheckBox();
            this.SettingsComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.CreditsButton = new System.Windows.Forms.Button();
            this.ImageLoadButton = new System.Windows.Forms.Button();
            this.BulkGenerateButton = new System.Windows.Forms.Button();
            this.AmountLabel = new System.Windows.Forms.Label();
            this.AmountTextBox = new System.Windows.Forms.TextBox();
            this.BulkImageGenButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // GenerateTextureButton
            // 
            this.GenerateTextureButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.GenerateTextureButton.Location = new System.Drawing.Point(6, 645);
            this.GenerateTextureButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.GenerateTextureButton.Name = "GenerateTextureButton";
            this.GenerateTextureButton.Size = new System.Drawing.Size(150, 33);
            this.GenerateTextureButton.TabIndex = 0;
            this.GenerateTextureButton.Text = "Generate Texture";
            this.GenerateTextureButton.UseVisualStyleBackColor = true;
            this.GenerateTextureButton.Click += new System.EventHandler(this.GenerateTextureButton_Click);
            // 
            // SaveButton
            // 
            this.SaveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveButton.Location = new System.Drawing.Point(256, 645);
            this.SaveButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(150, 33);
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
            this.PictureBox.BackgroundImage = global::Planet_Generator.Properties.Resources.FirstPlanet;
            this.PictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.PictureBox.InitialImage = global::Planet_Generator.Properties.Resources.FirstPlanet;
            this.PictureBox.Location = new System.Drawing.Point(3, 11);
            this.PictureBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.PictureBox.Name = "PictureBox";
            this.PictureBox.Size = new System.Drawing.Size(410, 448);
            this.PictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PictureBox.TabIndex = 2;
            this.PictureBox.TabStop = false;
            // 
            // ResolutionTextBox
            // 
            this.ResolutionTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ResolutionTextBox.Location = new System.Drawing.Point(72, 618);
            this.ResolutionTextBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ResolutionTextBox.Name = "ResolutionTextBox";
            this.ResolutionTextBox.Size = new System.Drawing.Size(52, 20);
            this.ResolutionTextBox.TabIndex = 3;
            this.ResolutionTextBox.Text = "400";
            // 
            // ResolutionLabel
            // 
            this.ResolutionLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ResolutionLabel.AutoSize = true;
            this.ResolutionLabel.Location = new System.Drawing.Point(6, 618);
            this.ResolutionLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.ResolutionLabel.Name = "ResolutionLabel";
            this.ResolutionLabel.Size = new System.Drawing.Size(57, 13);
            this.ResolutionLabel.TabIndex = 4;
            this.ResolutionLabel.Text = "Resolution";
            // 
            // PlanetNameLabel
            // 
            this.PlanetNameLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.PlanetNameLabel.AutoSize = true;
            this.PlanetNameLabel.Location = new System.Drawing.Point(253, 616);
            this.PlanetNameLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.PlanetNameLabel.Name = "PlanetNameLabel";
            this.PlanetNameLabel.Size = new System.Drawing.Size(68, 13);
            this.PlanetNameLabel.TabIndex = 5;
            this.PlanetNameLabel.Text = "Planet Name";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 591);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Atmosphere";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // AtmosphereTextBox
            // 
            this.AtmosphereTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.AtmosphereTextBox.Location = new System.Drawing.Point(72, 591);
            this.AtmosphereTextBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.AtmosphereTextBox.Name = "AtmosphereTextBox";
            this.AtmosphereTextBox.Size = new System.Drawing.Size(52, 20);
            this.AtmosphereTextBox.TabIndex = 6;
            this.AtmosphereTextBox.Text = "4";
            // 
            // Upscale
            // 
            this.Upscale.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Upscale.AutoSize = true;
            this.Upscale.Location = new System.Drawing.Point(255, 585);
            this.Upscale.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Upscale.Name = "Upscale";
            this.Upscale.Size = new System.Drawing.Size(131, 17);
            this.Upscale.TabIndex = 8;
            this.Upscale.Text = "Dynamic Upscale Res";
            this.Upscale.UseVisualStyleBackColor = true;
            // 
            // SettingsComboBox
            // 
            this.SettingsComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.SettingsComboBox.FormattingEnabled = true;
            this.SettingsComboBox.Location = new System.Drawing.Point(8, 556);
            this.SettingsComboBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.SettingsComboBox.Name = "SettingsComboBox";
            this.SettingsComboBox.Size = new System.Drawing.Size(116, 21);
            this.SettingsComboBox.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 536);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Planet Settings";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // CreditsButton
            // 
            this.CreditsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CreditsButton.Location = new System.Drawing.Point(253, 556);
            this.CreditsButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.CreditsButton.Name = "CreditsButton";
            this.CreditsButton.Size = new System.Drawing.Size(150, 25);
            this.CreditsButton.TabIndex = 11;
            this.CreditsButton.Text = "Credits";
            this.CreditsButton.UseVisualStyleBackColor = true;
            this.CreditsButton.Click += new System.EventHandler(this.CreditsButton_Click);
            // 
            // ImageLoadButton
            // 
            this.ImageLoadButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ImageLoadButton.Location = new System.Drawing.Point(8, 504);
            this.ImageLoadButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ImageLoadButton.Name = "ImageLoadButton";
            this.ImageLoadButton.Size = new System.Drawing.Size(160, 22);
            this.ImageLoadButton.TabIndex = 12;
            this.ImageLoadButton.Text = "Create Settings From Image";
            this.ImageLoadButton.UseVisualStyleBackColor = true;
            this.ImageLoadButton.Click += new System.EventHandler(this.ImageLoadButton_Click);
            // 
            // BulkGenerateButton
            // 
            this.BulkGenerateButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BulkGenerateButton.Location = new System.Drawing.Point(253, 525);
            this.BulkGenerateButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.BulkGenerateButton.Name = "BulkGenerateButton";
            this.BulkGenerateButton.Size = new System.Drawing.Size(150, 27);
            this.BulkGenerateButton.TabIndex = 13;
            this.BulkGenerateButton.Text = "Bulk Generate";
            this.BulkGenerateButton.UseVisualStyleBackColor = true;
            this.BulkGenerateButton.Click += new System.EventHandler(this.BulkGenerateButton_Click);
            // 
            // AmountLabel
            // 
            this.AmountLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.AmountLabel.AutoSize = true;
            this.AmountLabel.Location = new System.Drawing.Point(304, 466);
            this.AmountLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.AmountLabel.Name = "AmountLabel";
            this.AmountLabel.Size = new System.Drawing.Size(43, 13);
            this.AmountLabel.TabIndex = 15;
            this.AmountLabel.Text = "Amount";
            this.AmountLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // AmountTextBox
            // 
            this.AmountTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.AmountTextBox.Location = new System.Drawing.Point(351, 463);
            this.AmountTextBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.AmountTextBox.Name = "AmountTextBox";
            this.AmountTextBox.Size = new System.Drawing.Size(52, 20);
            this.AmountTextBox.TabIndex = 14;
            this.AmountTextBox.Text = "4";
            // 
            // BulkImageGenButton
            // 
            this.BulkImageGenButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BulkImageGenButton.Location = new System.Drawing.Point(253, 494);
            this.BulkImageGenButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.BulkImageGenButton.Name = "BulkImageGenButton";
            this.BulkImageGenButton.Size = new System.Drawing.Size(150, 27);
            this.BulkImageGenButton.TabIndex = 16;
            this.BulkImageGenButton.Text = "Bulk Images Gen";
            this.BulkImageGenButton.UseVisualStyleBackColor = true;
            this.BulkImageGenButton.Click += new System.EventHandler(this.BulkImageGenButton_Click);
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(417, 685);
            this.Controls.Add(this.BulkImageGenButton);
            this.Controls.Add(this.AmountLabel);
            this.Controls.Add(this.AmountTextBox);
            this.Controls.Add(this.BulkGenerateButton);
            this.Controls.Add(this.ImageLoadButton);
            this.Controls.Add(this.CreditsButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.SettingsComboBox);
            this.Controls.Add(this.Upscale);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.AtmosphereTextBox);
            this.Controls.Add(this.PlanetNameLabel);
            this.Controls.Add(this.ResolutionLabel);
            this.Controls.Add(this.ResolutionTextBox);
            this.Controls.Add(this.PictureBox);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.GenerateTextureButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "MainMenu";
            this.Text = "Planet Generator - By John Dodd";
            this.Load += new System.EventHandler(this.MainMenu_Load);
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
        private System.Windows.Forms.ComboBox SettingsComboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button CreditsButton;
        private System.Windows.Forms.Button ImageLoadButton;
        private System.Windows.Forms.Button BulkGenerateButton;
        private System.Windows.Forms.Label AmountLabel;
        private System.Windows.Forms.TextBox AmountTextBox;
        private System.Windows.Forms.Button BulkImageGenButton;
    }
}

