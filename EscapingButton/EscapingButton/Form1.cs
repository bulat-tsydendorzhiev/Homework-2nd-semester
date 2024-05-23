// Copyright (c) bulat-tsydendorzhiev, LLC. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace EscapingButton
{
    public partial class EscapingButtonForm : Form
    {
        private readonly Random random = new ();

        public EscapingButtonForm()
        {
            InitializeComponent();
        }

        private void EscapingButton_MouseMove(object sender, EventArgs e)
        {
            ChangePosition();
        }

        private void EscapingButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ChangePosition()
        {
            int newX = random.Next() % (Width - EscapingButton.Width);
            int newY = random.Next() % (Height - EscapingButton.Height);

            EscapingButton.Location = new Point(newX, newY);
        }
    }
}
