namespace OKNet.App.ViewModel.Jira
{
    public class JiraComponentViewModel : ViewModelBase
    {
        private int _id;
        private string _name;

        public int Id
        {
            get => _id;
            set => SetValue(ref _id, value);
        }

        public string Name
        {
            get => _name;
            set => SetValue(ref _name, value);
        }

        public override bool Equals(object obj)
        {
            var model = obj as JiraComponentViewModel;
            return model != null &&
                   _id == model._id &&
                   _name == model._name;
        }
    }
}