using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EinKomic.ViewModel
{
    class MainViewModel : ViewModelBase
    {
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
                MainFormMessage = "SinglePageCut:" + _singlePageCut.ToString();
                OnPropertyChanged("SinglePageCut");
            }
        }

    }
}
