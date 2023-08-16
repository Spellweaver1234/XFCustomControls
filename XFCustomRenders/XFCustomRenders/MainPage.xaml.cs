using System;

using Xamarin.Forms;

using XFCustomRenders.Controls;

namespace XFCustomRenders
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        int clicks = 0;
        private void ChangeText(object sender, EventArgs e)
        {
            CustomHeaderView hView = sender as CustomHeaderView;
            clicks += 1;
            hView.Text = $"{clicks} clicks";
        }
    }
}
