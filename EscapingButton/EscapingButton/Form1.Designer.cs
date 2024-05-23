namespace EscapingButton
{
    partial class EscapingButtonForm
    {
        private const int FormWidth = 800;
        private const int FormHeight = 500;

        private const int ButtonWidth = 150;
        private const int ButtonHeight = 75;

        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            EscapingButton = new Button();
            SuspendLayout();
            // 
            // EscapingButton
            // 
            EscapingButton.AutoSize = true;
            EscapingButton.Location = new Point(305, 181);
            EscapingButton.Name = "EscapingButton";
            EscapingButton.Size = new Size(133, 77);
            EscapingButton.TabIndex = 0;
            EscapingButton.Text = "Click me!";
            EscapingButton.UseVisualStyleBackColor = true;
            EscapingButton.MouseHover += EscapingButton_MouseHover;
            // 
            // EscapingButtonForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(784, 461);
            Controls.Add(EscapingButton);
            Name = "EscapingButtonForm";
            Text = "EscapingButtonForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button EscapingButton;
    }
}
