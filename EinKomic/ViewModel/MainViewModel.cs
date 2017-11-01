using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EinKomic.Model;

namespace EinKomic.ViewModel
{
    class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            this.SetInputPathView = new CommandBase(SetInputPathViewModel);
            this.SetOutputPathView = new CommandBase(SetOutputPathViewModel);
        }
        public CommandBase SetInputPathView { get; set; }
        private void SetInputPathViewModel(object obj)
        {
            String path;
            if (UtilHelper.GetFolder(out path))
                this.InputPath = path;
        }

        public CommandBase SetOutputPathView { get; set; }
        private void SetOutputPathViewModel(object obj)
        {
            String path;
            if (UtilHelper.GetFolder(out path))
                this.OutputPath = path;
        }

        private string _inputPath;
        public string InputPath
        {
            get
            {
                return _inputPath;
            }
            set
            {
                _inputPath = value;
                OnPropertyChanged("InputPath");
            }
        }

        private string _mainFormMessage;
        public string MainFormMessage
        {
            get
            {
                return _mainFormMessage; 
            }
            set
            {
                _mainFormMessage = value;
                OnPropertyChanged("MainFormMessage");
            }
        }

        private string _outputPath;
        public string OutputPath
        {
            get
            {
                return _outputPath;
            }
            set
            {
                _outputPath = value;
                OnPropertyChanged("OutputPath");
            }
        }

        private int _flipDirection;
        public int FlipDirection
        {
            get
            {
                return _flipDirection;
            }
            set
            {
                _flipDirection = value;
                MainFormMessage = "FlipDirection:" + _flipDirection.ToString();
                OnPropertyChanged("FlipDirection");
            }
        }

        private bool _singlePageCut;
        public bool SinglePageCut
        {
            get
            {
                return _singlePageCut;
            }
            set
            {
                _singlePageCut = value;
                
                OnPropertyChanged("SinglePageCut");
            }
        }

    }
}
