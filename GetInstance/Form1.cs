namespace GetInstance
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		public TextBox LogDisplayTextbox => txtLog;
		public IDisplayer Displayer { private get; set; }

		private void Form1_Shown(object sender, EventArgs e)
		{
			Displayer.WriteLine("Welcome to EasyCall Report's Live CDR Update");
		}
	}
}
