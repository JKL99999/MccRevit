using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Mcc.Revit.Master.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mcc.Revit.Master.ViewModels
{
    public class DockablePaneViewModel:ViewModelBase
    {
		private string text;

		public string Text
		{
			get { return text; }
			set { text = value; }
		}

		public RelayCommand CreateCommand {
			get => new RelayCommand(() =>
			{
				if (text != null)
				{
					SysCache.Instance.Text = text;
					SysCache.Instance.CreateText.Raise();
				}
			});
		}


    }
}
