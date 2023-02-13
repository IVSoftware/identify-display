Your question is about screen coordinates on a multi-screen display with emphasis on:
> [...] what if i decide to switch the programm to my left monitor? 

Have you considered using the [Screen](https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.screen) class to get the display metrics? Using Jeremy's [link](https://stackoverflow.com/a/1434577/5438626) for `GetWindowRect()` you could have your app poll for program position changes at intervals, determine which screen each app is on, and adjust your coordinates accordingly.

***
**Screen wrapper for ComboBox**

[![screenshot][1]][1]

    class ScreenItem
    {
        public ScreenItem(Screen screen) => Screen = screen;
        public Screen Screen { get; }
        string _displayName = null;
        public string DisplayName
        {
            get => _displayName?? Screen.DeviceName.Substring(Screen.DeviceName.LastIndexOf('\\') + 1);
            set => _displayName = value;
        }
        public override string ToString()
        {
            List<string> builder= new List<string>();
            builder.Add($"{nameof(Screen.Primary)} : {Screen.Primary}");
            builder.Add($"{nameof(Screen.Bounds)} : {Screen.Bounds}");
            builder.Add($"{nameof(Screen.WorkingArea)} : {Screen.WorkingArea}");
            return 
                string.Join(Environment.NewLine, builder.ToArray()
                .Select(_ => $"\t{_}"))
                + Environment.NewLine;
        }
    }

***
**Enumerate**

    public MainForm()
    {
        InitializeComponent();
        ScreenItem primary = null;
        foreach (var screenItem in Screen.AllScreens.Select(_ => new ScreenItem(_)))
        {
            if(screenItem.Screen.Primary)
            {
                primary = screenItem;
            }
            richTextBox.SelectionColor = Color.Navy;
            richTextBox.AppendText($"{screenItem.DisplayName}{Environment.NewLine}");
            richTextBox.SelectionColor = ForeColor;
            richTextBox.AppendText($"{screenItem}{Environment.NewLine}");
        }
    }

  [1]: https://i.stack.imgur.com/aD8Dl.png