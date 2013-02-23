namespace Ciastko
{
    partial class MainForm
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
            this.explorerTree = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // explorerTree
            // 
            this.explorerTree.Location = new System.Drawing.Point(13, 13);
            this.explorerTree.Name = "explorerTree";
            this.explorerTree.Size = new System.Drawing.Size(213, 512);
            this.explorerTree.TabIndex = 0;
            this.explorerTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.explorerTree_AfterSelect);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1043, 537);
            this.Controls.Add(this.explorerTree);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView explorerTree;

    }
}

