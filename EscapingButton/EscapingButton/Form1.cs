namespace EscapingButton
{
    public partial class EscapingButtonForm : Form
    {
        private readonly Random _random = new();

        public EscapingButtonForm()
        {
            InitializeComponent();
        }

        private void EscapingButton_MouseHover(object sender, EventArgs e)
        {
            ChangePosition();
        }

        private void EscapingButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ChangePosition()
        {
            int newX = _random.Next() % FormWidth;
            int newY = _random.Next() % FormHeight;

            if (FormWidth - newX < ButtonWidth)
            {
                newX -= ButtonWidth;
            }
            if (FormHeight - newY < ButtonHeight)
            {
                newY -= ButtonHeight;
            }

            EscapingButton.Location = new Point(newX, newY);
        }
    }
}
