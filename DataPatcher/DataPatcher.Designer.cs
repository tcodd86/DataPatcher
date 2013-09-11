namespace DataPatcher
{
    partial class DataPatcher
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
            this.AddDataFileToPatch = new System.Windows.Forms.Button();
            this.addDataFile = new System.Windows.Forms.OpenFileDialog();
            this.PatchDataFiles = new System.Windows.Forms.Button();
            this.saveFile = new System.Windows.Forms.SaveFileDialog();
            this.SuspendLayout();
            // 
            // AddDataFileToPatch
            // 
            this.AddDataFileToPatch.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AddDataFileToPatch.Location = new System.Drawing.Point(12, 12);
            this.AddDataFileToPatch.Name = "AddDataFileToPatch";
            this.AddDataFileToPatch.Size = new System.Drawing.Size(260, 36);
            this.AddDataFileToPatch.TabIndex = 0;
            this.AddDataFileToPatch.Text = "Add Data File To Patch";
            this.AddDataFileToPatch.UseVisualStyleBackColor = true;
            this.AddDataFileToPatch.Click += new System.EventHandler(this.button1_Click);
            // 
            // addDataFile
            // 
            this.addDataFile.FileName = "00p.txt";
            // 
            // PatchDataFiles
            // 
            this.PatchDataFiles.Enabled = false;
            this.PatchDataFiles.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PatchDataFiles.Location = new System.Drawing.Point(12, 67);
            this.PatchDataFiles.Name = "PatchDataFiles";
            this.PatchDataFiles.Size = new System.Drawing.Size(260, 36);
            this.PatchDataFiles.TabIndex = 1;
            this.PatchDataFiles.Text = "Patch Data Files";
            this.PatchDataFiles.UseVisualStyleBackColor = true;
            this.PatchDataFiles.Click += new System.EventHandler(this.button2_Click);
            // 
            // saveFile
            // 
            this.saveFile.DefaultExt = "txt";
            // 
            // DataPatcher
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 115);
            this.Controls.Add(this.PatchDataFiles);
            this.Controls.Add(this.AddDataFileToPatch);
            this.Name = "DataPatcher";
            this.Text = "Data Patcher";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button AddDataFileToPatch;
        private System.Windows.Forms.OpenFileDialog addDataFile;
        private System.Windows.Forms.Button PatchDataFiles;
        private System.Windows.Forms.SaveFileDialog saveFile;
    }
}

