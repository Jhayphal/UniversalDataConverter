using System.Collections.ObjectModel;

namespace UniversalDataConvertor
{
    class Mode
    {
        public string Name { get; set; }

        public string FileDataSourceName { get; set; }

        public ObservableCollection<FieldInfo> Fields { get; set; }
    }
}