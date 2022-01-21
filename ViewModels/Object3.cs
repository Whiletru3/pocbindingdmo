namespace pocbindingdmo.ViewModels
{
    public class Object3 : ViewModelBase
    {
        private string _strObject3;
        public string StrObject3 { 
            get =>  _strObject3;

            set
            {
                SetAndRaise(ref _strObject3, value) ;
            } 
        }
        

        public Object3()
        {
            _strObject3 = "test3";
        }

        public Object3(string test)
        {
            _strObject3 = test;
        }
    }
}