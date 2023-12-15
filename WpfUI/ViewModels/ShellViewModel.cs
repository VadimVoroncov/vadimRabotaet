using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfUI.ViewModels
{
    public class ShellViewModel : Screen
    {
        private string _title = " dKLASDFJL sf";
        public string Title
        { 
            get { return _title; }
            set { _title = value; }
        }
    }
}
