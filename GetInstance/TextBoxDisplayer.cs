namespace GetInstance
{
	internal class TextBoxDisplayer : IDisplayer
	{
		public TextBox textbox;

		public TextBoxDisplayer(TextBox textbox)
		{
			this.textbox = textbox;
		} 

		public void Write(string text)
		{
			if (!textbox.Disposing && !textbox.IsDisposed)
				textbox.AppendText(text);
		}

		public void WriteLine(string text)
		{
			if (!textbox.Disposing && !textbox.IsDisposed)
				textbox.AppendText(text + "\r\n");
		}
	}
}
