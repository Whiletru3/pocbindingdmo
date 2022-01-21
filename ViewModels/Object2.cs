namespace pocbindingdmo.ViewModels
{
    public class Object2 : ViewModelBase
    {
        private string _strObject2;
        public string StrObject2 { 
            get =>  _strObject2;

            set
            {
                SetAndRaise(ref _strObject2, value) ;
            } 
        }

        public Object2()
        {
            StrObject2 = "test2";
        }
        public Object2(string test)
        {
            StrObject2 = test;
        }
    }
}