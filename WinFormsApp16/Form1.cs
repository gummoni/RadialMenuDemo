namespace WinFormsApp16
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            radialMenuControl1.StartAnimation(AnimationState.Enter);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            radialMenuControl1.StartAnimation(AnimationState.Leave);
        }
    }
}