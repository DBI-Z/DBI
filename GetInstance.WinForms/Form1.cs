using System.Diagnostics;

namespace GetInstance.WinForms
{
	public partial class Form1 : Form
	{ 
		public Form1()
		{
			InitializeComponent();
		}
		void Log(string text)
		{
			Debug.WriteLine(text);
			txtLog.Text += text + "\r\n";
		}

		private void Form1_Activated(object sender, EventArgs e)
		{
			Log("Welcome to EasyCall Report's Live CDR Update");
		}
	}
}