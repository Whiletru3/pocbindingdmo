using Avalonia.Animation;

namespace pocbindingdmo.ViewModels
{
    public class Object1 : ViewModelBase
    {
        private string _strObject1;
        public string StrObject1 { 
            get =>  _strObject1;

            set
            {
                SetAndRaise(ref _strObject1, value) ;
            } 
        }
        

        public Object1()
        {
            StrObject1 = "test1";
        }

        public Object1(string test)
        {
            StrObject1 = test;
        }
    }
}