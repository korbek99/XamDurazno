﻿using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace IDD
{
    public partial class WebViewPage : ContentPage
    {
        public WebViewPage()
        {
            InitializeComponent();
        }

        public WebViewPage(string url)
        {
            InitializeComponent();
            wv.Source = url;
        }
    }
}
