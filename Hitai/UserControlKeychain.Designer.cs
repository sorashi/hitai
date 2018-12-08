namespace Hitai
{
    partial class UserControlKeychain
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.listView = new System.Windows.Forms.ListView();
            this.columnHeaderPrivate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderLCreationTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderExpires = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // listView
            // 
            this.listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderPrivate,
            this.columnHeaderId,
            this.columnHeaderName,
            this.columnHeaderLCreationTime,
            this.columnHeaderExpires});
            this.listView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView.FullRowSelect = true;
            this.listView.Location = new System.Drawing.Point(0, 0);
            this.listView.Name = "listView";
            this.listView.Size = new System.Drawing.Size(587, 496);
            this.listView.TabIndex = 0;
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeaderPrivate
            // 
            this.columnHeaderPrivate.Text = "Soukromý";
            this.columnHeaderPrivate.Width = 50;
            // 
            // columnHeaderId
            // 
            this.columnHeaderId.Text = "ID";
            this.columnHeaderId.Width = 100;
            // 
            // columnHeaderName
            // 
            this.columnHeaderName.Text = "Název";
            this.columnHeaderName.Width = 208;
            // 
            // columnHeaderLCreationTime
            // 
            this.columnHeaderLCreationTime.Text = "Vytvořeno";
            this.columnHeaderLCreationTime.Width = 80;
            // 
            // columnHeaderExpires
            // 
            this.columnHeaderExpires.Text = "Vyprší";
            this.columnHeaderExpires.Width = 62;
            // 
            // UserControlKeychain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.listView);
            this.Name = "UserControlKeychain";
            this.Size = new System.Drawing.Size(587, 496);
            this.Load += new System.EventHandler(this.UserControlKeychain_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listView;
        private System.Windows.Forms.ColumnHeader columnHeaderPrivate;
        private System.Windows.Forms.ColumnHeader columnHeaderName;
        private System.Windows.Forms.ColumnHeader columnHeaderLCreationTime;
        private System.Windows.Forms.ColumnHeader columnHeaderExpires;
        private System.Windows.Forms.ColumnHeader columnHeaderId;
    }
}
