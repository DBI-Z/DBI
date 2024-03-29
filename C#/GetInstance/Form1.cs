﻿namespace GetInstance
{
	public partial class Form1 : Form
	{
		string[] cmdLineArgs;
		bool fromCmdLineArgs;

		public Form1(string[] args)
		{
			InitializeComponent();
			displayer = new TextBoxDisplayer(txtLog);
			this.cmdLineArgs = args;
		}

		public TextBox LogDisplayTextbox => txtLog;
		IDisplayer displayer;

		private void Form1_Shown(object sender, EventArgs e)
		{
			displayer.WriteLine("Welcome to EasyCall Report's Live CDR Update");

			ArgumentProcessor processor = new(cmdLineArgs, displayer);
			GetInstanceRequest request = processor.GetRequest();
			if (request == null)
			{
				Fill(ArgumentProcessor.TestRequest);
			}
			else
			{
				Fill(request);
				GetCdr(request);
			}
		}

		void Fill(GetInstanceRequest request)
		{
			txtUsername.Text = request.Username;
			txtPassword.Text = request.Password;
			txtRssd.Text = request.IdRssd;
			txtPeriods.Text = request.NumberOfPriorPeriods.ToString();
			txtDate.Text = request.ReportingPeriodEndDate.ToString(GetInstanceRequest.DateFormat);
		}

		GetInstanceRequest GetRequestFromUI()
		{
			List<string> nonEmptyArgs = new();
			string[] uiArgs = new string[]
			{
				txtUsername.Text,
				txtPassword.Text,
				"call",
				txtRssd.Text,
				txtPeriods.Text,
				txtDate.Text,
			};

			foreach (string arg in uiArgs)
				if (!string.IsNullOrWhiteSpace(arg))
					nonEmptyArgs.Add(arg);

			return new ArgumentProcessor(nonEmptyArgs.ToArray(), displayer).GetRequest();
		}

		async Task GetCdr(GetInstanceRequest request)
		{
			displayer.WriteLine("Updating history with prior quarter data from the CDR...");
			displayer.WriteLine("1. Logging on to CDR: ");
			await new InstanceGetter(new Prior2Downloader(displayer), new CsvWriter(displayer), new Extractor(displayer), displayer).Get(request);
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			lblFormat.Text = GetInstanceRequest.DateFormat;
		}

		private void btnGetCdr_Click(object sender, EventArgs e)
		{
			displayer.WriteLine(string.Empty);
			displayer.WriteLine("-----------------");
			GetInstanceRequest uiRequest = GetRequestFromUI();
			if (uiRequest != null)
				_ = GetCdr(uiRequest);
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}
